using System.IO;

namespace NFSLib.Compression
{
	public static class JDLZ
	{
		public static byte[] decompress( byte[] input )
		{
			int flags1 = 1, flags2 = 1;
			int i, t, length;
			int inPos = 16, outPos = 0;

			if( input[0] != 'J' && input[1] != 'D' && input[2] != 'L' && input[3] != 'Z' && input[4] != 2 )
			{
				throw new InvalidDataException( "Input not JDLZ!" );
			}

			byte[] output = new byte[( input[11] << 24 ) + ( input[10] << 16 ) + ( input[9] << 8 ) + input[8]];

			while( ( inPos < input.Length ) && ( outPos < output.Length ) )
			{
				if( flags1 == 1 )
				{
					flags1 = input[inPos] | 0x100;
					++inPos;
				}
				if( flags2 == 1 )
				{
					flags2 = input[inPos] | 0x100;
					++inPos;
				}

				if( ( flags1 & 1 ) > 0 )
				{
					if( ( flags2 & 1 ) > 0 )
					{
						length = ( input[inPos + 1] | ( ( input[inPos] & 0xF0 ) << 4 ) ) + 3;
						t = ( input[inPos] & 0xF ) + 1;
					}
					else
					{
						t = ( input[inPos + 1] | ( ( input[inPos] & 0xE0 ) << 3 ) ) + 17;
						length = ( input[inPos] & 0x1F ) + 3;
					}

					inPos += 2;

					for( i = 0; i < length; ++i )
					{
						output[outPos + i] = output[outPos + i - t];
					}

					outPos += i;
					flags2 >>= 1;
				}
				else
				{
					if( outPos < output.Length )
					{
						output[outPos] = input[inPos];
						++outPos;
						++inPos;
					}
				}
				flags1 >>= 1;
			}
			return output;
		}

		// TODO
		// arushan's jdlzdll.dll (shipped with 'bintex') has a very different looking JDLZ compressor.
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
			ushort v23 = 0xFF00, v24 = 0xFF00;
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
				// The NFS version is much longer than arushan's, but the flow is much easier to follow;
				// I presume the goto's in arushan's are compiler optimizations.
				// Basically there's two choices:
				// 1. Figure out what the hell this thing is doing.
				// 2. Find a way to rewrite arushan's without the goto's, due to them not being allowed in some languages (such as C#).
				// I suspect #2 would be easier, it's likely doable by breaking the code into functions using ref's.
				//v9 = *((_DWORD *)v5 + (-0x1A1 * (*(_BYTE *)v8 ^ (unsigned __int16)(0x10 * (*(_BYTE *)(v8 + 1) ^ (unsigned __int16)(0x10 * *(_BYTE *)(v8 + 2))))) & 0x1FFF));
			}


			return output;
		}
	}
}
