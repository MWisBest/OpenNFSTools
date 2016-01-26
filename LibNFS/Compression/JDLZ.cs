using System;
using System.IO;

namespace NFSTools.LibNFS.Compression
{
	/// <summary>
	/// JDLZ compression is used in various BlackBox NFS games, starting somewhere around Underground and continuing to World.
	/// Like other formats used, it has a little 16 byte header, followed by the actual data.
	/// 0x0 - 0x3 = 'JDLZ'
	/// 0x4       = 0x02
	/// 0x5       = 0x10
	/// 0x6 - 0x7 = 0x0000
	/// 0x8 - 0xB = (uncompressed data length; little-endian)
	/// 0xC - 0xF = (compressed data length, including header; little-endian)
	///
	/// JDLZ is very lightweight; it's read totally sequentially and is fairly simple,
	/// however this obviously comes at a tradeoff of how effectively it compresses data.
	/// </summary>
	public static class JDLZ
	{
		public static byte[] decompress( byte[] input )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( nameof( input ) );
			}
			else if( input.Length < 16 || input[0] != 'J' || input[1] != 'D' || input[2] != 'L' || input[3] != 'Z' || input[4] != 0x02 )
			{
				throw new InvalidDataException( "Input header is not JDLZ!" );
			}

			int flags1 = 1, flags2 = 1;
			int t, length;
			int inPos = 16, outPos = 0;

			// TODO: Can we always trust the header's stated length?
			byte[] output = new byte[BitConverter.ToInt32( input, 8 )];

			while( ( inPos < input.Length ) && ( outPos < output.Length ) )
			{
				if( flags1 == 1 )
				{
					flags1 = input[inPos++] | 0x100;
				}
				if( flags2 == 1 )
				{
					flags2 = input[inPos++] | 0x100;
				}

				if( ( flags1 & 1 ) == 1 )
				{
					if( ( flags2 & 1 ) == 1 ) // 3 to 4098(?) iterations, backtracks 1 to 16(?) bytes
					{
						// length max is 4098(?) (0x1002), assuming input[inPos] and input[inPos + 1] are both 0xFF
						length = ( input[inPos + 1] | ( ( input[inPos] & 0xF0 ) << 4 ) ) + 3;
						// t max is 16(?) (0x10), assuming input[inPos] is 0xFF
						t = ( input[inPos] & 0x0F ) + 1;
					}
					else // 3(?) to 34(?) iterations, backtracks 17(?) to 2064(?) bytes
					{
						// t max is 2064(?) (0x810), assuming input[inPos] and input[inPos + 1] are both 0xFF
						t = ( input[inPos + 1] | ( ( input[inPos] & 0xE0 ) << 3 ) ) + 17;
						// length max is 34(?) (0x22), assuming input[inPos] is 0xFF
						length = ( input[inPos] & 0x1F ) + 3;
					}

					inPos += 2;

					for( int i = 0; i < length; ++i )
					{
						output[outPos + i] = output[outPos + i - t];
					}

					outPos += length;
					flags2 >>= 1;
				}
				else
				{
					if( outPos < output.Length )
					{
						output[outPos++] = input[inPos++];
					}
				}
				flags1 >>= 1;
			}
			return output;
		}

		/// <summary>
		/// This lovely JDLZ compressor was written by the user "zombie28" of the encode.ru forums.
		/// Its compression ratios are within a few percent of the NFS games' JDLZ compressor. Awesome!!
		/// </summary>
		/// <param name="input">bytes to compress with JDLZ</param>
		/// <param name="hashSize">speed/ratio tunable. use powers of 2, results vary per file.</param>
		/// <returns>JDLZ-compressed bytes, w/ 16 byte header</returns>
		public static byte[] compress( byte[] input, int hashSize = 0x2000 )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			const int HeaderSize = 16;
			const int MinMatchLength = 3;
			const int MaxSearchDepth = 16;

			int inputBytes = input.Length;
			byte[] output = new byte[inputBytes + ( ( inputBytes + 7 ) / 8 ) + HeaderSize + 1];
			int[] hashPos = new int[hashSize];
			int[] hashChain = new int[inputBytes];

			int outPos = 0;
			int inPos = 0;
			byte flags1bit = 1;
			byte flags2bit = 1;
			byte flags1 = 0;
			byte flags2 = 0;

			output[outPos++] = 0x4A; // 'J'
			output[outPos++] = 0x44; // 'D'
			output[outPos++] = 0x4C; // 'L'
			output[outPos++] = 0x5A; // 'Z'
			output[outPos++] = 0x02;
			output[outPos++] = 0x10;
			output[outPos++] = 0x00;
			output[outPos++] = 0x00;
			output[outPos++] = (byte)inputBytes;
			output[outPos++] = (byte)( inputBytes >> 8 );
			output[outPos++] = (byte)( inputBytes >> 16 );
			output[outPos++] = (byte)( inputBytes >> 24 );
			outPos += 4;

			int flags1Pos = outPos++;
			int flags2Pos = outPos++;

			flags1bit <<= 1;
			output[outPos++] = input[inPos++];
			inputBytes--;

			while( inputBytes > 0 )
			{
				int bestMatchLength = MinMatchLength - 1;
				int bestMatchDist = 0;

				if( inputBytes >= MinMatchLength )
				{
					int hash = ( -0x1A1 * ( input[inPos] ^ ( ( input[inPos + 1] ^ ( input[inPos + 2] << 4 ) ) << 4 ) ) ) & (hashSize - 1);
					int matchPos = hashPos[hash];
					hashPos[hash] = inPos;
					hashChain[inPos] = matchPos;
					int prevMatchPos = inPos;

					for( int i = 0; i < MaxSearchDepth; i++ )
					{
						int matchDist = inPos - matchPos;

						if( matchDist > 2064 || matchPos >= prevMatchPos )
						{
							break;
						}

						int matchLengthLimit = matchDist <= 16 ? 4098 : 34;
						int maxMatchLength = inputBytes;

						if( maxMatchLength > matchLengthLimit )
						{
							maxMatchLength = matchLengthLimit;
						}
						if( bestMatchLength >= maxMatchLength )
						{
							break;
						}

						int matchLength = 0;
						while( ( matchLength < maxMatchLength ) && ( input[inPos + matchLength] == input[matchPos + matchLength] ) )
						{
							matchLength++;
						}

						if( matchLength > bestMatchLength )
						{
							bestMatchLength = matchLength;
							bestMatchDist = matchDist;
						}

						prevMatchPos = matchPos;
						matchPos = hashChain[matchPos];
					}
				}

				if( bestMatchLength >= MinMatchLength )
				{
					flags1 |= flags1bit;
					inPos += bestMatchLength;
					inputBytes -= bestMatchLength;
					bestMatchLength -= MinMatchLength;

					if( bestMatchDist < 17 )
					{
						flags2 |= flags2bit;
						output[outPos++] = (byte)( ( bestMatchDist - 1 ) | ( ( bestMatchLength >> 4 ) & 0xF0 ) );
						output[outPos++] = (byte)bestMatchLength;
					}
					else
					{
						bestMatchDist -= 17;
						output[outPos++] = (byte)( bestMatchLength | ( ( bestMatchDist >> 3 ) & 0xE0 ) );
						output[outPos++] = (byte)bestMatchDist;
					}

					flags2bit <<= 1;
				}
				else
				{
					output[outPos++] = input[inPos++];
					inputBytes--;
				}

				flags1bit <<= 1;

				if( flags1bit == 0 )
				{
					output[flags1Pos] = flags1;
					flags1 = 0;
					flags1Pos = outPos++;
					flags1bit = 1;
				}

				if( flags2bit == 0 )
				{
					output[flags2Pos] = flags2;
					flags2 = 0;
					flags2Pos = outPos++;
					flags2bit = 1;
				}
			}

			if( flags2bit > 1 )
			{
				output[flags2Pos] = flags2;
			}
			else if( flags2Pos == outPos - 1 )
			{
				outPos = flags2Pos;
			}

			if( flags1bit > 1 )
			{
				output[flags1Pos] = flags1;
			}
			else if( flags1Pos == outPos - 1 )
			{
				outPos = flags1Pos;
			}

			output[12] = (byte)outPos;
			output[13] = (byte)( outPos >> 8 );
			output[14] = (byte)( outPos >> 16 );
			output[15] = (byte)( outPos >> 24 );

			Array.Resize( ref output, outPos );
			return output;
		}

		// TODO
		// All the other tools I've found literally copy and pasted the game's assembly code for the JDLZ compressor.
		// What a joke! No wonder they all packed their binaries and didn't release source code... their stuff is garbage!
		private static byte[] compress_nfs( byte[] input )
		{
			const int HASH_LOG = 13;
			const int HASH_SIZE = ( 1 << HASH_LOG ); // 0x2000
			const int HASH_MASK = ( HASH_SIZE - 1 ); // 0x1FFF

			byte[] output = new byte[input.Length + 0x1000];
			int a2 = input.Length; // remaining data?!
			//uint[] v5 = new uint[0x2000];
			byte[] v5 = new byte[HASH_SIZE * sizeof( uint )]; // Hash table. I'm not sure why it's * 4, but that's what they malloc'd. maybe it's uint directly?
			int v6 = input.Length;
			int v10, v27;
			int v11;
			int v16; // some other variable for remaining data?!
			int v19;
			bool v20; // done processing data?!
			int v23 = 0xFF00, v24 = 0xFF00;
			int v25 = 0;
			int v26;
			int v32 = 0;
			int v33 = 0;
			int v34 = input.Length; // i just don't even.

			// Construct header.
			output[0] = 0x4A; // 'J'
			output[1] = 0x44; // 'D'
			output[2] = 0x4C; // 'L'
			output[3] = 0x5A; // 'Z'
			output[4] = 0x02;
			output[5] = 0x10;
			output[6] = 0x00;
			output[7] = 0x00;
			output[8] = (byte)( input.Length & 0xFF );
			output[9] = (byte)( input.Length >> 8 & 0xFF );
			output[10] = (byte)( input.Length >> 16 & 0xFF );
			output[11] = (byte)( input.Length >> 24 & 0xFF );

			// NOTE: 12-15 is output length
			// output array will need to be recreated at its final size.

			while( true )
			{
				v26 = 4098;
				if( v6 < 4098 )
				{
					v26 = v6;
				}

				// oh god this is ugly. Probably the ugliest part of the NFS version's decompile. But we shall not be deterred.
				//v9 = *((_DWORD *)v5 + (-417 * (*(_BYTE *)v8 ^ (unsigned __int16)(16 * (*(_BYTE *)(v8 + 1) ^ (unsigned __int16)(16 * *(_BYTE *)(v8 + 2))))) & 0x1FFF));
				// This is definitely the hash thing.
				// - v5 is a pointer to the hash table, v8 is a pointer to the current spot in the uncompressed data (input)
				// - hash index is calculated and then added to hash table's pointer to get that particular index in the table
				// - Although the index calculation starts off with the "-417", the "& 0x1FFF" at the end chops off any negative issue when all is said and done.
				int inputIndex = 0; // TODO: Keep track of where we are in the input.
				int hashindex;
				hashindex = -417; // NOTE: I think this could be rewritten as "7775" (HASH_SIZE - 417) if necessary
				hashindex *= ( input[inputIndex + 0] ^ ( input[inputIndex + 1] << 4 ^ ( input[inputIndex + 2] << 4 ) ) );
				hashindex &= HASH_MASK; // 0x1FFF

				v10 = 2;
				// ...
				v27 = 2;
				// ...
				while( v10 < 4098 ) // && v9
				{
					v11 = 0;
					if( v26 > 3 )
					{
						// ...
						v10 = v27;
						// ...
					}

					if( v11 < v26 )
					{
						// ...
						v10 = v27;
					}

					if( v11 > v10 && ( v11 <= 34 || /* v25 - *(_DWORD *)v9 < 16 || */ v10 <= 34 )  )
					{
						v10 = v11;
						v27 = v11;
						// ...
					}
					// ...
				}

				if( v10 > 2 )
				{
					v23 >>= 1;
					// ...
					a2 -= v10;

					while( v10 > 0 )
					{
						// ...
						--v10;
						++v25;
					}

					v16 = a2;
				}
				else
				{
					// ...
					v16 = a2 - 1;
					v23 = ( v23 >> 1 ) & 0x7F7F;
					++v25;
					--a2;
				}

				if( v23 < 0x100 )
				{
					// ...
					v23 = 0xFF00;
					// ...
				}
				if( v24 < 0x100 )
				{
					// ...
					v24 = 0xFF00;
					// ...
				}

				// TODO: THIS MAKES NO SENSE!
				++v32;
				if( (v32 & 0x1FFF) != 0 ) // ? !( v32 & 0x1FFF )
				{
					v19 = 10 * v16 / v34;
					if( 10 - v19 != v33 )
					{
						v33 = 10 - v19;
					}
				}

				v20 = v16 < 0;
				// ...

				if( v20 )
				{
					break;
				}

				v6 = a2;
			}
			// ...
			while( ( v23 & 0xFF00 ) > 0 )
			{
				v23 >>= 1;
			}
			// ...
			while( ( v24 & 0xFF00 ) > 0 )
			{
				v24 >>= 1;
			}
			// ...

			return output;
		}
	}
}
