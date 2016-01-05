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
		public static byte[] compress( byte[] input )
		{
			byte[] output = new byte[input.Length];

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

			return output;
		}
	}
}
