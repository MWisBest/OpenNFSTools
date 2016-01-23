using System;
using System.Collections.Generic;
using System.IO;

namespace NFSTools.LibNFS.Compression
{
	public static class CompressUtils
	{
		public static byte[] decompress_auto( byte[] input, out CompressType usedType )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( nameof( input ) );
			}

			byte[] output = null;
			usedType = CompressType.NULL;

			try
			{
				output = RAWW.decompress( input );
				usedType = CompressType.RAWW;
			}
			catch( InvalidDataException )
			{
				try
				{
					output = JDLZ.decompress( input );
					usedType = CompressType.JDLZ;
				}
				catch( InvalidDataException )
				{
					// Try more types as added...
				}
			}

			return output;
		}

		public static byte[] compress_auto( byte[] input, out CompressType usedType, CompressType allowedTypes = CompressType.ALL )
		{
			// Sanity checking...
			if( input == null )
			{
				throw new ArgumentNullException( nameof( input ) );
			}
			if( allowedTypes == CompressType.NULL )
			{
				throw new ArgumentException( "Allowed compression type cannot be NULL!", nameof( allowedTypes ) );
			}

			Dictionary<CompressType, byte[]> mapping = new Dictionary<CompressType, byte[]>();
			byte[] output = null;
			usedType = CompressType.NULL;

			if( ( allowedTypes & CompressType.RAWW ) == CompressType.RAWW )
			{
				mapping.Add( CompressType.RAWW, RAWW.compress( input ) );
			}
			if( ( allowedTypes & CompressType.JDLZ ) == CompressType.JDLZ )
			{
				mapping.Add( CompressType.JDLZ, JDLZ.compress( input ) );
			}

			int minLength = int.MaxValue;

			foreach( KeyValuePair<CompressType, byte[]> entry in mapping )
			{
				// NOTE: if the length is EQUAL TO the shortest one, we decide based on the complexity of that type's decompression.
				if( ( entry.Value.Length < minLength ) || ( entry.Value.Length == minLength && entry.Key < usedType ) )
				{
					minLength = entry.Value.Length;
					output = entry.Value;
					usedType = entry.Key;
				}
			}

			return output;
		}
	}
}
