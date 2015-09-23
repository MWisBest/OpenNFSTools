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
		public static uint getHash32( string toHash, uint magic = 2882400000u ) // 0xABCDEF00
		{
			int num = 0;
			int i = toHash.Length;
			uint num2 = 2654435769u; // 0x9E3779B9
			uint num3 = num2;
			uint num4 = magic;
			while( i >= 12 )
			{
				num2 += (uint)( toHash[num] + ( (uint)toHash[1 + num] << 8 ) + ( (uint)toHash[2 + num] << 16 ) + ( (uint)toHash[3 + num] << 24 ) );
				num3 += (uint)( toHash[4 + num] + ( (uint)toHash[5 + num] << 8 ) + ( (uint)toHash[6 + num] << 16 ) + ( (uint)toHash[7 + num] << 24 ) );
				num4 += (uint)( toHash[8 + num] + ( (uint)toHash[9 + num] << 8 ) + ( (uint)toHash[10 + num] << 16 ) + ( (uint)toHash[11 + num] << 24 ) );
				num2 = num4 >> 13 ^ ( num2 - num3 - num4 );
				num3 = num2 << 8 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 13 ^ ( num4 - num2 - num3 );
				num2 = num4 >> 12 ^ ( num2 - num3 - num4 );
				num3 = num2 << 16 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 5 ^ ( num4 - num2 - num3 );
				num2 = num4 >> 3 ^ ( num2 - num3 - num4 );
				num3 = num2 << 10 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 15 ^ ( num4 - num2 - num3 );
				num += 12;
				i -= 12;
			}
			num4 += (uint)toHash.Length;
			switch( i )
			{
				case 11:
					num4 += (uint)( (uint)toHash[10 + num] << 24 );
					goto TEN;
				case 10:
				TEN:
					num4 += (uint)( (uint)toHash[9 + num] << 16 );
					goto NINE;
				case 9:
				NINE:
					num4 += (uint)( (uint)toHash[8 + num] << 8 );
					goto EIGHT;
				case 8:
				EIGHT:
					num3 += (uint)( (uint)toHash[7 + num] << 24 );
					goto SEVEN;
				case 7:
				SEVEN:
					num3 += (uint)( (uint)toHash[6 + num] << 16 );
					goto SIX;
				case 6:
				SIX:
					num3 += (uint)( (uint)toHash[5 + num] << 8 );
					goto FIVE;
				case 5:
				FIVE:
					num3 += (uint)toHash[4 + num];
					goto FOUR;
				case 4:
				FOUR:
					num2 += (uint)( (uint)toHash[3 + num] << 24 );
					goto THREE;
				case 3:
				THREE:
					num2 += (uint)( (uint)toHash[2 + num] << 16 );
					goto TWO;
				case 2:
				TWO:
					num2 += (uint)( (uint)toHash[1 + num] << 8 );
					goto ONE;
				case 1:
				ONE:
					num2 += (uint)toHash[num];
					break;
				default:
					break;
			}
			num2 = num4 >> 13 ^ ( num2 - num3 - num4 );
			num3 = num2 << 8 ^ ( num3 - num4 - num2 );
			num4 = num3 >> 13 ^ ( num4 - num2 - num3 );
			num2 = num4 >> 12 ^ ( num2 - num3 - num4 );
			num3 = num2 << 16 ^ ( num3 - num4 - num2 );
			num4 = num3 >> 5 ^ ( num4 - num2 - num3 );
			num2 = num4 >> 3 ^ ( num2 - num3 - num4 );
			num3 = num2 << 10 ^ ( num3 - num4 - num2 );
			return ( num3 >> 15 ^ ( num4 - num2 - num3 ) );
		}

		public static ulong getHash64( string toHash, ulong magic = 1234605617886129920uL ) // 0x11223344ABCDEF00
		{
			int num = 0;
			int i = toHash.Length;
			ulong num2 = 11400714819323198483uL; // 0x9E3779B97F4A7C13
			ulong num3 = num2;
			ulong num4 = magic;
			byte[] bytes = Encoding.ASCII.GetBytes( toHash );
			while( i >= 24 )
			{
				num2 += BitConverter.ToUInt64( bytes, num );
				num3 += BitConverter.ToUInt64( bytes, num + 8 );
				num4 += BitConverter.ToUInt64( bytes, num + 16 );
				num2 = num4 >> 43 ^ ( num2 - num3 - num4 );
				num3 = num2 << 9 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 8 ^ ( num4 - num2 - num3 );
				num2 = num4 >> 38 ^ ( num2 - num3 - num4 );
				num3 = num2 << 23 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 5 ^ ( num4 - num2 - num3 );
				num2 = num4 >> 35 ^ ( num2 - num3 - num4 );
				num3 = num2 << 49 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 11 ^ ( num4 - num2 - num3 );
				num2 = num4 >> 12 ^ ( num2 - num3 - num4 );
				num3 = num2 << 18 ^ ( num3 - num4 - num2 );
				num4 = num3 >> 22 ^ ( num4 - num2 - num3 );
				i -= 24;
				num += 24;
			}
			num4 += (ulong)( (long)toHash.Length );
			switch( i )
			{
				case 23:
					num4 += (ulong)toHash[22] << 56;
					goto TWENTYTWO;
				case 22:
				TWENTYTWO:
					num4 += (ulong)toHash[21] << 48;
					goto TWENTYONE;
				case 21:
				TWENTYONE:
					num4 += (ulong)toHash[20] << 40;
					goto TWENTY;
				case 20:
				TWENTY:
					num4 += (ulong)toHash[19] << 32;
					goto NINETEEN;
				case 19:
				NINETEEN:
					num4 += (ulong)toHash[18] << 24;
					goto EIGHTEEN;
				case 18:
				EIGHTEEN:
					num4 += (ulong)toHash[17] << 16;
					goto SEVENTEEN;
				case 17:
				SEVENTEEN:
					num4 += (ulong)toHash[16] << 8;
					goto SIXTEEN;
				case 16:
				SIXTEEN:
					num3 += (ulong)toHash[15] << 56;
					goto FIFTEEN;
				case 15:
				FIFTEEN:
					num3 += (ulong)toHash[14] << 48;
					goto FOURTEEN;
				case 14:
				FOURTEEN:
					num3 += (ulong)toHash[13] << 40;
					goto THIRTEEN;
				case 13:
				THIRTEEN:
					num3 += (ulong)toHash[12] << 32;
					goto TWELVE;
				case 12:
				TWELVE:
					num3 += (ulong)toHash[11] << 24;
					goto ELEVEN;
				case 11:
				ELEVEN:
					num3 += (ulong)toHash[10] << 16;
					goto TEN;
				case 10:
				TEN:
					num3 += (ulong)toHash[9] << 8;
					goto NINE;
				case 9:
				NINE:
					num3 += (ulong)toHash[8];
					goto EIGHT;
				case 8:
				EIGHT:
					num2 += (ulong)toHash[7] << 56;
					goto SEVEN;
				case 7:
				SEVEN:
					num2 += (ulong)toHash[6] << 48;
					goto SIX;
				case 6:
				SIX:
					num2 += (ulong)toHash[5] << 40;
					goto FIVE;
				case 5:
				FIVE:
					num2 += (ulong)toHash[4] << 32;
					goto FOUR;
				case 4:
				FOUR:
					num2 += (ulong)toHash[3] << 24;
					goto THREE;
				case 3:
				THREE:
					num2 += (ulong)toHash[2] << 16;
					goto TWO;
				case 2:
				TWO:
					num2 += (ulong)toHash[1] << 8;
					goto ONE;
				case 1:
				ONE:
					num2 += (ulong)toHash[0];
					break;
				default:
					break;
			}
			num2 = num4 >> 43 ^ ( num2 - num3 - num4 );
			num3 = num2 << 9 ^ ( num3 - num4 - num2 );
			num4 = num3 >> 8 ^ ( num4 - num2 - num3 );
			num2 = num4 >> 38 ^ ( num2 - num3 - num4 );
			num3 = num2 << 23 ^ ( num3 - num4 - num2 );
			num4 = num3 >> 5 ^ ( num4 - num2 - num3 );
			num2 = num4 >> 35 ^ ( num2 - num3 - num4 );
			num3 = num2 << 49 ^ ( num3 - num4 - num2 );
			num4 = num3 >> 11 ^ ( num4 - num2 - num3 );
			num2 = num4 >> 12 ^ ( num2 - num3 - num4 );
			num3 = num2 << 18 ^ ( num3 - num4 - num2 );
			return ( num3 >> 22 ^ ( num4 - num2 - num3 ) );
		}
	}
}
