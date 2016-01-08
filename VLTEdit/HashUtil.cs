using System;
using System.Text;

namespace VLTEdit
{
	/**
	 * The names of entries in the VLT db are hashed with a Jenkins(-like) hash function, specifically the 32-bit one here;
	 * the 64-bit one is believed (by MWisBest...) to have been in the game code as well, but was left unused.
	 */
	public class HashUtil // OBF: o.cs
	{
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

		public static uint getHash32( string toHash, uint magic = 0xABCDEF00u )
		{
			int num = 0, i = toHash.Length;
			uint a = 0x9E3779B9u, b = 0x9E3779B9u, c = magic;
			while( i >= 12 )
			{
				a += ( toHash[num] + ( (uint)toHash[1 + num] << 8 ) + ( (uint)toHash[2 + num] << 16 ) + ( (uint)toHash[3 + num] << 24 ) );
				b += ( toHash[4 + num] + ( (uint)toHash[5 + num] << 8 ) + ( (uint)toHash[6 + num] << 16 ) + ( (uint)toHash[7 + num] << 24 ) );
				c += ( toHash[8 + num] + ( (uint)toHash[9 + num] << 8 ) + ( (uint)toHash[10 + num] << 16 ) + ( (uint)toHash[11 + num] << 24 ) );
				mix32( ref a, ref b, ref c );
				num += 12;
				i -= 12;
			}
			c += (uint)toHash.Length;
			switch( i )
			{
				// NOTE: C# doesn't allow fallthroughs, so as a workaround we use goto's instead.
				case 11:
					c += (uint)toHash[10 + num] << 24;
					goto case 10;
				case 10:
					c += (uint)toHash[9 + num] << 16;
					goto case 9;
				case 9:
					c += (uint)toHash[8 + num] << 8;
					goto case 8;
				case 8:
					b += (uint)toHash[7 + num] << 24;
					goto case 7;
				case 7:
					b += (uint)toHash[6 + num] << 16;
					goto case 6;
				case 6:
					b += (uint)toHash[5 + num] << 8;
					goto case 5;
				case 5:
					b += toHash[4 + num];
					goto case 4;
				case 4:
					a += (uint)toHash[3 + num] << 24;
					goto case 3;
				case 3:
					a += (uint)toHash[2 + num] << 16;
					goto case 2;
				case 2:
					a += (uint)toHash[1 + num] << 8;
					goto case 1;
				case 1:
					a += toHash[num];
					break;
				default:
					break;
			}

			// micro-optimization: avoids ref overhead and skips 1 assignment by having a separate function for this.
			return mix32_final( a, b, c );
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

		public static ulong getHash64( string toHash, ulong magic = 0x11223344ABCDEF00uL )
		{
			int num = 0;
			int i = toHash.Length;
			ulong a = 0x9E3779B97F4A7C13uL, b = 0x9E3779B97F4A7C13uL, c = magic;
			byte[] bytes = Encoding.ASCII.GetBytes( toHash );
			while( i >= 24 )
			{
				a += BitConverter.ToUInt64( bytes, num );
				b += BitConverter.ToUInt64( bytes, num + 8 );
				c += BitConverter.ToUInt64( bytes, num + 16 );
				mix64( ref a, ref b, ref c );
				num += 24;
				i -= 24;
			}
			c += (ulong)toHash.Length;
			switch( i )
			{
				// NOTE: C# doesn't allow fallthroughs, so as a workaround we use goto's instead.
				case 23:
					c += (ulong)toHash[22] << 56;
					goto case 22;
				case 22:
					c += (ulong)toHash[21] << 48;
					goto case 21;
				case 21:
					c += (ulong)toHash[20] << 40;
					goto case 20;
				case 20:
					c += (ulong)toHash[19] << 32;
					goto case 19;
				case 19:
					c += (ulong)toHash[18] << 24;
					goto case 18;
				case 18:
					c += (ulong)toHash[17] << 16;
					goto case 17;
				case 17:
					c += (ulong)toHash[16] << 8;
					goto case 16;
				case 16:
					b += (ulong)toHash[15] << 56;
					goto case 15;
				case 15:
					b += (ulong)toHash[14] << 48;
					goto case 14;
				case 14:
					b += (ulong)toHash[13] << 40;
					goto case 13;
				case 13:
					b += (ulong)toHash[12] << 32;
					goto case 12;
				case 12:
					b += (ulong)toHash[11] << 24;
					goto case 11;
				case 11:
					b += (ulong)toHash[10] << 16;
					goto case 10;
				case 10:
					b += (ulong)toHash[9] << 8;
					goto case 9;
				case 9:
					b += toHash[8];
					goto case 8;
				case 8:
					a += (ulong)toHash[7] << 56;
					goto case 7;
				case 7:
					a += (ulong)toHash[6] << 48;
					goto case 6;
				case 6:
					a += (ulong)toHash[5] << 40;
					goto case 5;
				case 5:
					a += (ulong)toHash[4] << 32;
					goto case 4;
				case 4:
					a += (ulong)toHash[3] << 24;
					goto case 3;
				case 3:
					a += (ulong)toHash[2] << 16;
					goto case 2;
				case 2:
					a += (ulong)toHash[1] << 8;
					goto case 1;
				case 1:
					a += toHash[0];
					break;
				default:
					break;
			}

			// micro-optimization: avoids ref overhead and skips 1 assignment by having a separate function for this.
			return mix64_final( a, b, c );
		}
	}
}
