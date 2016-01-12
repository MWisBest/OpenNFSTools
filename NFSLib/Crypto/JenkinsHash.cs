using System;
using System.Text;

namespace NFSTools.NFSLib.Crypto
{
	/// <summary>
	/// This is an implementation of Bob Jenkin's original hash function.
	/// Its intended use is for table lookups, and NFS games using VLT databases do exactly that.
	/// It's very fast, and produces 32-bit (or optionally 64-bit) hashes.
	///
	/// THIS IS NOT A CRYPTOGRAPHIC HASH. Collisions are simple to make on purpose.
	/// When used *as intended*, /accidental/ collisions are fairly rare.
	///
	/// More details can be found on his website: http://burtleburtle.net/bob/hash/evahash.html
	/// </summary>
	public static class JenkinsHash
	{
		/// <summary>
		/// 32-bit Jenkins hash implementation.
		/// </summary>
		/// <param name="data">bytes to hash</param>
		/// <param name="magic">variable internal state init (default 0xABCDEF00u for NFS)</param>
		/// <returns>32-bit Jenkins hash</returns>
		public static uint getHash32( byte[] data, uint magic = 0xABCDEF00u )
		{
			int num = 0, i = data.Length;
			uint a = 0x9E3779B9u, b = 0x9E3779B9u, c = magic;

			while( i >= 12 )
			{
				a += BitConverter.ToUInt32( data, num );
				b += BitConverter.ToUInt32( data, num + 4 );
				c += BitConverter.ToUInt32( data, num + 8 );
				mix32( ref a, ref b, ref c );
				num += 12;
				i -= 12;
			}

			c += (uint)data.Length;

			switch( i )
			{
				// NOTE: C# doesn't allow fallthroughs, so as a workaround we use goto's instead.
				case 11:
					c += (uint)data[10 + num] << 24;
					goto case 10;
				case 10:
					c += (uint)data[9 + num] << 16;
					goto case 9;
				case 9:
					c += (uint)data[8 + num] << 8;
					goto case 8;
				case 8:
					b += (uint)data[7 + num] << 24;
					goto case 7;
				case 7:
					b += (uint)data[6 + num] << 16;
					goto case 6;
				case 6:
					b += (uint)data[5 + num] << 8;
					goto case 5;
				case 5:
					b += data[4 + num];
					goto case 4;
				case 4:
					a += (uint)data[3 + num] << 24;
					goto case 3;
				case 3:
					a += (uint)data[2 + num] << 16;
					goto case 2;
				case 2:
					a += (uint)data[1 + num] << 8;
					goto case 1;
				case 1:
					a += data[num];
					break;
				default:
					break;
			}

			// micro-optimization: avoids ref overhead and skips 1 assignment by having a separate function for this.
			return mix32_final( a, b, c );
		}

		/// <summary>
		/// 64-bit Jenkins hash implementation.
		/// </summary>
		/// <param name="data">bytes to hash</param>
		/// <param name="magic">variable internal state init (default 0x11223344ABCDEF00uL for NFS)</param>
		/// <returns>64-bit Jenkins hash</returns>
		public static ulong getHash64( byte[] data, ulong magic = 0x11223344ABCDEF00uL )
		{
			int num = 0, i = data.Length;
			ulong a = 0x9E3779B97F4A7C13uL, b = 0x9E3779B97F4A7C13uL, c = magic;

			while( i >= 24 )
			{
				a += BitConverter.ToUInt64( data, num );
				b += BitConverter.ToUInt64( data, num + 8 );
				c += BitConverter.ToUInt64( data, num + 16 );
				mix64( ref a, ref b, ref c );
				num += 24;
				i -= 24;
			}

			c += (ulong)data.Length;

			switch( i )
			{
				// NOTE: C# doesn't allow fallthroughs, so as a workaround we use goto's instead.
				case 23:
					c += (ulong)data[22] << 56;
					goto case 22;
				case 22:
					c += (ulong)data[21] << 48;
					goto case 21;
				case 21:
					c += (ulong)data[20] << 40;
					goto case 20;
				case 20:
					c += (ulong)data[19] << 32;
					goto case 19;
				case 19:
					c += (ulong)data[18] << 24;
					goto case 18;
				case 18:
					c += (ulong)data[17] << 16;
					goto case 17;
				case 17:
					c += (ulong)data[16] << 8;
					goto case 16;
				case 16:
					b += (ulong)data[15] << 56;
					goto case 15;
				case 15:
					b += (ulong)data[14] << 48;
					goto case 14;
				case 14:
					b += (ulong)data[13] << 40;
					goto case 13;
				case 13:
					b += (ulong)data[12] << 32;
					goto case 12;
				case 12:
					b += (ulong)data[11] << 24;
					goto case 11;
				case 11:
					b += (ulong)data[10] << 16;
					goto case 10;
				case 10:
					b += (ulong)data[9] << 8;
					goto case 9;
				case 9:
					b += data[8];
					goto case 8;
				case 8:
					a += (ulong)data[7] << 56;
					goto case 7;
				case 7:
					a += (ulong)data[6] << 48;
					goto case 6;
				case 6:
					a += (ulong)data[5] << 40;
					goto case 5;
				case 5:
					a += (ulong)data[4] << 32;
					goto case 4;
				case 4:
					a += (ulong)data[3] << 24;
					goto case 3;
				case 3:
					a += (ulong)data[2] << 16;
					goto case 2;
				case 2:
					a += (ulong)data[1] << 8;
					goto case 1;
				case 1:
					a += data[0];
					break;
				default:
					break;
			}

			// micro-optimization: avoids ref overhead and skips 1 assignment by having a separate function for this.
			return mix64_final( a, b, c );
		}

		/// <summary>
		/// Helper function for hashing strings (using 32-bit Jenkins hash implementation).
		/// </summary>
		/// <param name="data">string to hash</param>
		/// <param name="magic">variable internal state init (default 0xABCDEF00u for NFS)</param>
		/// <returns>32-bit Jenkins hash</returns>
		public static uint getHash32( string data, uint magic = 0xABCDEF00u )
		{
			return JenkinsHash.getHash32( Encoding.ASCII.GetBytes( data ), magic );
		}

		/// <summary>
		/// Helper function for hashing strings (using 64-bit Jenkins hash implementation).
		/// </summary>
		/// <param name="data">string to hash</param>
		/// <param name="magic">variable internal state init (default 0x11223344ABCDEF00uL for NFS)</param>
		/// <returns>64-bit Jenkins hash</returns>
		public static ulong getHash64( string data, ulong magic = 0x11223344ABCDEF00uL )
		{
			return JenkinsHash.getHash64( Encoding.ASCII.GetBytes( data ), magic );
		}

		private static void mix32( ref uint a, ref uint b, ref uint c )
		{
			a = c >> 13 ^ ( a - b - c );
			b = a <<  8 ^ ( b - c - a );
			c = b >> 13 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 16 ^ ( b - c - a );
			c = b >>  5 ^ ( c - a - b );
			a = c >>  3 ^ ( a - b - c );
			b = a << 10 ^ ( b - c - a );
			c = b >> 15 ^ ( c - a - b );
		}

		private static void mix64( ref ulong a, ref ulong b, ref ulong c )
		{
			a = c >> 43 ^ ( a - b - c );
			b = a <<  9 ^ ( b - c - a );
			c = b >>  8 ^ ( c - a - b );
			a = c >> 38 ^ ( a - b - c );
			b = a << 23 ^ ( b - c - a );
			c = b >>  5 ^ ( c - a - b );
			a = c >> 35 ^ ( a - b - c );
			b = a << 49 ^ ( b - c - a );
			c = b >> 11 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 18 ^ ( b - c - a );
			c = b >> 22 ^ ( c - a - b );
		}

		private static uint mix32_final( uint a, uint b, uint c )
		{
			a = c >> 13 ^ ( a - b - c );
			b = a <<  8 ^ ( b - c - a );
			c = b >> 13 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 16 ^ ( b - c - a );
			c = b >>  5 ^ ( c - a - b );
			a = c >>  3 ^ ( a - b - c );
			b = a << 10 ^ ( b - c - a );
			return b >> 15 ^ ( c - a - b );
		}

		private static ulong mix64_final( ulong a, ulong b, ulong c )
		{
			a = c >> 43 ^ ( a - b - c );
			b = a <<  9 ^ ( b - c - a );
			c = b >>  8 ^ ( c - a - b );
			a = c >> 38 ^ ( a - b - c );
			b = a << 23 ^ ( b - c - a );
			c = b >>  5 ^ ( c - a - b );
			a = c >> 35 ^ ( a - b - c );
			b = a << 49 ^ ( b - c - a );
			c = b >> 11 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 18 ^ ( b - c - a );
			return b >> 22 ^ ( c - a - b );
		}
	}
}
