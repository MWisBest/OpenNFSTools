namespace NFSLib.Crypto
{
	/// <summary>
	/// A 32-bit CRC is used for, at the very least, checksums in some savegames.
	/// This is a table-based implementation, identical to what the games themself use.
	///
	/// It's a little different than the usual 'standard' CRC32 implementations,
	/// but isn't "exotic" or completely unique.
	/// </summary>
	public static class CRC32NFS
	{
		private static uint[] crcTable = new uint[256];

		/// <summary>
		/// This static initializer generates the crc table.
		/// The games have it hardcoded, but obviously they had to be generated somehow! :)
		/// </summary>
		static CRC32NFS()
		{
			uint remainder;

			for( uint dividend = 0; dividend < 256; ++dividend )
			{
				remainder = dividend << 24;

				for( uint bit = 0; bit < 8; ++bit )
				{
					if( ( remainder & 0x80000000u ) > 0 )
					{
						remainder = ( remainder << 1 ) ^ 0x04C11DB7u;
					}
					else
					{
						remainder = ( remainder << 1 );
					}
				}
				crcTable[dividend] = remainder;
			}
		}

		/// <summary>
		/// Gets the 'CRC32-NFS' of a given byte array.
		/// </summary>
		/// <param name="data">bytes to hash</param>
		/// <returns>calculated CRC32-NFS hash</returns>
		public static uint getHash( byte[] data )
		{
			uint crc32 = 0x00000000u;

			if( data.Length >= 4 )
			{
				int index = 0;
				crc32 = ~(uint)( ( data[index + 3] ) | ( data[index + 2] << 8 ) | ( data[index + 1] << 16 ) | ( data[index] << 24 ) );
				index += 4;
				while( index < data.Length )
				{
					crc32 = crcTable[crc32 >> 24] ^ ( ( crc32 << 8 ) | ( data[index++] ) );
				}
				crc32 = ~crc32;
			}

			return crc32;
		}
	}
}
