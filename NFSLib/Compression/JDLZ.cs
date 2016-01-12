using System.IO;

namespace NFSTools.NFSLib.Compression
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
			int flags1 = 1, flags2 = 1;
			int t, length;
			int inPos = 16, outPos = 0;

			if( input[0] != 'J' || input[1] != 'D' || input[2] != 'L' || input[3] != 'Z' || input[4] != 0x02 )
			{
				throw new InvalidDataException( "Input not JDLZ!" );
			}

			// TODO: Can we always trust the header's stated length?
			byte[] output = new byte[( input[11] << 24 ) + ( input[10] << 16 ) + ( input[9] << 8 ) + input[8]];

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

				if( ( flags1 & 1 ) > 0 )
				{
					if( ( flags2 & 1 ) > 0 )
					{
						// length max is 4098 (0x1002), assuming input[inPos] and input[inPos + 1] are both 0xFF
						length = ( input[inPos + 1] | ( ( input[inPos] & 0xF0 ) << 4 ) ) + 0x03;
						// t max is 16 (0x10), assuming input[inPos] is 0xFF
						t = ( input[inPos] & 0x0F ) + 0x01;
					}
					else
					{
						// t max is 2064 (0x810), assuming input[inPos] and input[inPos + 1] are both 0xFF
						t = ( input[inPos + 1] | ( ( input[inPos] & 0xE0 ) << 3 ) ) + 0x11;
						// length max is 34 (0x22), assuming input[inPos] is 0xFF
						length = ( input[inPos] & 0x1F ) + 0x03;
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

		// TODO
		// arushan's jdlzdll.dll (shipped with 'bintex') has a very different looking JDLZ compressor.
		// It might be something more rudimentary, which produces output compatible
		// with the decompression algorithm, but doesn't compress quite as well.
		public static byte[] compress_arushan( byte[] input )
		{
			byte[] output = new byte[input.Length];
			int v4 = 0;
			int v6 = 0;
			int v14 = input.Length;
			int v16 = 0;

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

			output[16] = 0x00;
			output[17] = 0x00;

		// NOTE: This label should actually be inside the while loop directly below this,
		// but it also will work here.
		LABEL_15:
			if( v14 < 0 )
			{
				goto OUT;
			}

			while( v6 < 4096 && v4 > 0 ) // && *(_BYTE *)(v4 + v8) == *(_BYTE *)(v8 + v4 - 1)
			{
				++v4;
				v16 = v4;
				//++v13;
				--v14;
				++v6;
				if( v14 < 0 )
				{
					goto OUT;
				}
			}
		// NOTE: This label should actually be inside the if, but it also will work here.
		LABEL_14:
			if( v6 <= 0 )
			{
				// ...
				--v14;
				// ...
				v16 = v4;
				// ...
				goto LABEL_15;
			}
			if( v6 <= 2 )
			{
				if( v6 <= 0 )
				{
					v6 = 0;
					goto LABEL_14;
				}
				while( true )
				{
					// ...
					--v6;
					if( v6 == 0 ) // "!v6"
						break;
					// ...
				}
			}
			else
			{
				// ...
				v4 = v16;
			}
			// ...
			v6 = 0;
			goto LABEL_14;

		OUT:
			// set length here, reduce output size to final size
			return output;
		}

		// TODO
		// All the other tools I've found literally copy and pasted the game's assembly code for the JDLZ compressor.
		// What a joke! No wonder they all packed their binaries and didn't release source code... their stuff is garbage!
		public static byte[] compress_nfs( byte[] input )
		{
			byte[] output = new byte[input.Length];
			int v6 = input.Length;
			int v10, v27;
			int v11;
			int v23 = 0xFF00, v24 = 0xFF00;
			int v26;

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
				v26 = 0x1002;
				if( v6 < 0x1002 )
				{
					v26 = v6;
				}

				// oh god this is ugly. Probably the ugliest part of the NFS version's decompile.
				//v9 = *((_DWORD *)v5 + (-0x1A1 * (*(_BYTE *)v8 ^ (unsigned __int16)(0x10 * (*(_BYTE *)(v8 + 1) ^ (unsigned __int16)(0x10 * *(_BYTE *)(v8 + 2))))) & 0x1FFF));

				v10 = 0x0002;
				// ...
				v27 = 0x0002;
				// ...
				while( v10 < 0x1002 ) // && v9
				{
					v11 = 0;
					if( v26 > 0x0003 )
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
				}
				else
				{
					// ...
					v23 = ( v23 >> 1 ) & 0x7F7F;
					// ...
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
				// ...

				// this break is temporary, just shuts up Visual Studio about "unreachable code" right now.
				break;
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
