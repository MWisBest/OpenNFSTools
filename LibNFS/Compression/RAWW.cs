using System;
using System.IO;

namespace NFSTools.LibNFS.Compression
{
	/// <summary>
	/// RAWW compression is... well, not actually compression.
	/// It isn't really seen in the games, but its purpose is/was to store
	/// data that *increases* in size when it is attempted to be compressed.
	///
	/// Like other formats used, it has a little 16 byte header, followed by the actual data.
	/// 0x0 - 0x3 = 'RAWW'
	/// 0x4       = 0x01
	/// 0x5       = 0x10
	/// 0x6 - 0x7 = 0x0000
	/// 0x8 - 0xB = (uncompressed data length; little-endian)
	/// 0xC - 0xF = ((un)compressed data length, plus header; little-endian)
	/// </summary>
	public static class RAWW
	{
		public static byte[] decompress( byte[] input )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( "input" );
			}
			else if( input.Length < 16 || input[0] != 'R' || input[1] != 'A' || input[2] != 'W' || input[3] != 'W' || input[4] != 0x01 )
			{
				throw new InvalidDataException( "Input header is not RAWW!" );
			}

			// TODO: Should we go with the stated length, or with input.Length - 16?
			byte[] output = new byte[BitConverter.ToInt32( input, 8 )];

			Array.ConstrainedCopy( input, 16, output, 0, output.Length );

			return output;
		}

		public static byte[] compress( byte[] input )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( "input" );
			}

			byte[] output = new byte[input.Length + 16];
			int outPos = 0;

			// Create header
			output[outPos++] = 0x52; // 'R'
			output[outPos++] = 0x41; // 'A'
			output[outPos++] = 0x57; // 'W'
			output[outPos++] = 0x57; // 'W'
			output[outPos++] = 0x01;
			output[outPos++] = 0x10;
			output[outPos++] = 0x00;
			output[outPos++] = 0x00;
			output[outPos++] = (byte)input.Length;
			output[outPos++] = (byte)( input.Length >> 8 );
			output[outPos++] = (byte)( input.Length >> 16 );
			output[outPos++] = (byte)( input.Length >> 24 );
			output[outPos++] = (byte)output.Length;
			output[outPos++] = (byte)( output.Length >> 8 );
			output[outPos++] = (byte)( output.Length >> 16 );
			output[outPos++] = (byte)( output.Length >> 24 );

			Array.ConstrainedCopy( input, 0, output, outPos, input.Length );

			return output;
		}

	}
}
