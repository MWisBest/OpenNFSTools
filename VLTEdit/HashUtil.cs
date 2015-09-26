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
			b = a << 8 ^ ( b - c - a );
			c = b >> 13 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 16 ^ ( b - c - a );
			c = b >> 5 ^ ( c - a - b );
			a = c >> 3 ^ ( a - b - c );
			b = a << 10 ^ ( b - c - a );
			c = b >> 15 ^ ( c - a - b );
		}

		public static uint getHash32( string toHash, uint magic = 0xABCDEF00u )
		{
			int num = 0;
			int i = toHash.Length;
			uint a = 0x9E3779B9u, b = 0x9E3779B9u, c = magic;
			while( i >= 12 )
			{
				a += (uint)( toHash[num] + ( (uint)toHash[1 + num] << 8 ) + ( (uint)toHash[2 + num] << 16 ) + ( (uint)toHash[3 + num] << 24 ) );
				b += (uint)( toHash[4 + num] + ( (uint)toHash[5 + num] << 8 ) + ( (uint)toHash[6 + num] << 16 ) + ( (uint)toHash[7 + num] << 24 ) );
				c += (uint)( toHash[8 + num] + ( (uint)toHash[9 + num] << 8 ) + ( (uint)toHash[10 + num] << 16 ) + ( (uint)toHash[11 + num] << 24 ) );
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
					goto TEN;
				case 10: TEN:
					c += (uint)toHash[9 + num] << 16;
					goto NINE;
				case 9:  NINE:
					c += (uint)toHash[8 + num] << 8;
					goto EIGHT;
				case 8:  EIGHT:
					b += (uint)toHash[7 + num] << 24;
					goto SEVEN;
				case 7:  SEVEN:
					b += (uint)toHash[6 + num] << 16;
					goto SIX;
				case 6:  SIX:
					b += (uint)toHash[5 + num] << 8;
					goto FIVE;
				case 5:  FIVE:
					b += (uint)toHash[4 + num];
					goto FOUR;
				case 4:  FOUR:
					a += (uint)toHash[3 + num] << 24;
					goto THREE;
				case 3:  THREE:
					a += (uint)toHash[2 + num] << 16;
					goto TWO;
				case 2:  TWO:
					a += (uint)toHash[1 + num] << 8;
					goto ONE;
				case 1:  ONE:
					a += (uint)toHash[num];
					break;
				default:
					break;
			}
			mix32( ref a, ref b, ref c );
			return c;
		}

		private static void mix64( ref ulong a, ref ulong b, ref ulong c )
		{
			a = c >> 43 ^ ( a - b - c );
			b = a << 9 ^ ( b - c - a );
			c = b >> 8 ^ ( c - a - b );
			a = c >> 38 ^ ( a - b - c );
			b = a << 23 ^ ( b - c - a );
			c = b >> 5 ^ ( c - a - b );
			a = c >> 35 ^ ( a - b - c );
			b = a << 49 ^ ( b - c - a );
			c = b >> 11 ^ ( c - a - b );
			a = c >> 12 ^ ( a - b - c );
			b = a << 18 ^ ( b - c - a );
			c = b >> 22 ^ ( c - a - b );
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
					goto TWENTYTWO;
				case 22: TWENTYTWO:
					c += (ulong)toHash[21] << 48;
					goto TWENTYONE;
				case 21: TWENTYONE:
					c += (ulong)toHash[20] << 40;
					goto TWENTY;
				case 20: TWENTY:
					c += (ulong)toHash[19] << 32;
					goto NINETEEN;
				case 19: NINETEEN:
					c += (ulong)toHash[18] << 24;
					goto EIGHTEEN;
				case 18: EIGHTEEN:
					c += (ulong)toHash[17] << 16;
					goto SEVENTEEN;
				case 17: SEVENTEEN:
					c += (ulong)toHash[16] << 8;
					goto SIXTEEN;
				case 16: SIXTEEN:
					b += (ulong)toHash[15] << 56;
					goto FIFTEEN;
				case 15: FIFTEEN:
					b += (ulong)toHash[14] << 48;
					goto FOURTEEN;
				case 14: FOURTEEN:
					b += (ulong)toHash[13] << 40;
					goto THIRTEEN;
				case 13: THIRTEEN:
					b += (ulong)toHash[12] << 32;
					goto TWELVE;
				case 12: TWELVE:
					b += (ulong)toHash[11] << 24;
					goto ELEVEN;
				case 11: ELEVEN:
					b += (ulong)toHash[10] << 16;
					goto TEN;
				case 10: TEN:
					b += (ulong)toHash[9] << 8;
					goto NINE;
				case 9:  NINE:
					b += (ulong)toHash[8];
					goto EIGHT;
				case 8:  EIGHT:
					a += (ulong)toHash[7] << 56;
					goto SEVEN;
				case 7:  SEVEN:
					a += (ulong)toHash[6] << 48;
					goto SIX;
				case 6:  SIX:
					a += (ulong)toHash[5] << 40;
					goto FIVE;
				case 5:  FIVE:
					a += (ulong)toHash[4] << 32;
					goto FOUR;
				case 4:  FOUR:
					a += (ulong)toHash[3] << 24;
					goto THREE;
				case 3:  THREE:
					a += (ulong)toHash[2] << 16;
					goto TWO;
				case 2:  TWO:
					a += (ulong)toHash[1] << 8;
					goto ONE;
				case 1:  ONE:
					a += (ulong)toHash[0];
					break;
				default:
					break;
			}
			mix64( ref a, ref b, ref c );
			return c;
		}
	}
}
