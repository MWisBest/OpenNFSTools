using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public class bw
{
	public enum enumA // obf: "a"
	{
		b = 4,
		c = 32,
		d = 64
	}

	public enum enumC // obf: "c"
	{
		b = 827611204,
		c = 861165636,
		d = 21,
		e = 41
	}

	public class exceptionB : Exception // obf: "b"
	{
		public exceptionB( string reason ) : base( reason )
		{
		}
	}

	public int intA; // obf: "a"

	public int intB; // obf: "b"

	public int intC; // obf: "c"

	public int intD; // obf: "d"

	public int intE; // obf: "e"

	public int intF; // obf: "f"

	public bw.enumA g; // obf: "g"

	public bw.enumC h; // obf: "h"

	public int intI; // obf: "i"

	public int intJ; // obf: "j"

	public int intK; // obf: "k"

	public int intL; // obf: "l"

	public int intM; // obf: "m"

	public int intN; // obf: "n"

	public int intO; // obf: "o"

	public int intP; // obf: "p"

	public int intQ; // obf: "q"

	public byte[] byteArrayR; // obf: "r"

	public byte[][] byteArrayArrayS; // obf: "s"

	public void a( string A_0 )
	{
		FileStream fileStream = new FileStream( A_0, (FileMode)3, (FileAccess)1 );
		try
		{
			this.a( fileStream );
		}
		catch( Exception ex )
		{
			fileStream.Close();
			throw ex;
		}
	}

	public void a( Stream A_0 )
	{
		BinaryReader binaryReader = new BinaryReader( A_0 );
		if( binaryReader.ReadInt32() != 542327876 )
		{
			throw new bw.exceptionB( "Not a valid DDS file." );
		}
		A_0.Seek( 8L, (SeekOrigin)1 );
		this.intA = binaryReader.ReadInt32();
		this.intB = binaryReader.ReadInt32();
		this.intC = binaryReader.ReadInt32();
		this.intD = binaryReader.ReadInt32();
		this.intE = binaryReader.ReadInt32();
		A_0.Seek( 44L, (SeekOrigin)1 );
		this.intF = binaryReader.ReadInt32();
		this.g = (bw.enumA)binaryReader.ReadInt32();
		this.h = (bw.enumC)binaryReader.ReadInt32();
		this.intI = binaryReader.ReadInt32();
		this.intJ = binaryReader.ReadInt32();
		this.intK = binaryReader.ReadInt32();
		this.intL = binaryReader.ReadInt32();
		this.intM = binaryReader.ReadInt32();
		this.intN = binaryReader.ReadInt32();
		this.intO = binaryReader.ReadInt32();
		this.intP = binaryReader.ReadInt32();
		this.intQ = binaryReader.ReadInt32();
		binaryReader.ReadInt32();
		if( ( this.g & bw.enumA.c ) != (bw.enumA)0 )
		{
			this.byteArrayR = binaryReader.ReadBytes( ( 1 << this.intI ) * 4 );
		}
		if( this.intE == 0 )
		{
			this.intE = 1;
		}
		this.byteArrayArrayS = new byte[this.intE][];
		int num = this.intC;
		for( int i = 0; i < this.intE; i++ )
		{
			this.byteArrayArrayS[i] = binaryReader.ReadBytes( num );
			num /= 2;
		}
	}

	public Image a()
	{
		if( ( this.g & bw.enumA.b ) != (bw.enumA)0 )
		{
			byte[] array = this.byteArrayArrayS[0];
			if( this.h == bw.enumC.b )
			{
				array = this.a( array, this.intB, this.intA );
			}
			else
			{
				if( this.h != bw.enumC.c )
				{
					return null;
				}
				array = this.b( array, this.intB, this.intA );
			}
			Bitmap bitmap = new Bitmap( this.intB, this.intA, (PixelFormat)2498570 );
			Rectangle rectangle = new Rectangle( 0, 0, bitmap.Width, bitmap.Height );
			BitmapData bitmapData = bitmap.LockBits( rectangle, (ImageLockMode)3, bitmap.PixelFormat );
			Marshal.Copy( array, 0, bitmapData.Scan0, this.intB * this.intA * 4 );
			bitmap.UnlockBits( bitmapData );
			return bitmap;
		}
		return null;
	}

	private byte[] b( byte[] A_0, int A_1, int A_2 )
	{
		byte[] array = new byte[A_1 * A_2 * 4];
		int num = A_1 / 4;
		int num2 = A_2 / 4;
		for( int i = 0; i < num2; i++ )
		{
			for( int j = 0; j < num; j++ )
			{
				int num3 = ( i * num + j ) * 16;
				ushort[] array2 = new ushort[]
				{
					BitConverter.ToUInt16(A_0, num3),
					BitConverter.ToUInt16(A_0, num3 + 2),
					BitConverter.ToUInt16(A_0, num3 + 4),
					BitConverter.ToUInt16(A_0, num3 + 6)
				};
				byte[,] array3 = new byte[4, 4];
				for( int k = 0; k < 4; k++ )
				{
					for( int l = 0; l < 4; l++ )
					{
						array3[l, k] = (byte)( ( array2[k] & 15 ) * 16 );
						ushort[] array4;
						IntPtr intPtr;
						( array4 = array2 )[(int)( intPtr = (IntPtr)k )] = (ushort)( array4[(int)intPtr] >> 4 );
					}
				}
				ushort num4 = BitConverter.ToUInt16( A_0, num3 + 8 );
				ushort num5 = BitConverter.ToUInt16( A_0, num3 + 8 + 2 );
				uint num6 = BitConverter.ToUInt32( A_0, num3 + 8 + 4 );
				ushort num7 = (ushort)( 8 * ( num4 & 31 ) ); // NOTE: WAS NOT CASTED BEFORE
				ushort num8 = (ushort)( 4 * ( num4 >> 5 & 63 ) );
				ushort num9 = (ushort)( 8 * ( num4 >> 11 & 31 ) );
				ushort num10 = (ushort)( 8 * ( num5 & 31 ) ); // NOTE: WAS NOT CASTED BEFORE
				ushort num11 = (ushort)( 4 * ( num5 >> 5 & 63 ) );
				ushort num12 = (ushort)( 8 * ( num5 >> 11 & 31 ) );
				for( int m = 0; m < 4; m++ )
				{
					for( int n = 0; n < 4; n++ )
					{
						int num13 = A_1 * ( i * 4 + m ) * 4 + ( j * 4 + n ) * 4;
						uint num14 = num6 & 3u;
						array[num13 + 3] = array3[n, m];
						switch( num14 )
						{
							case 0u:
								array[num13] = (byte)num7;
								array[num13 + 1] = (byte)num8;
								array[num13 + 2] = (byte)num9;
								break;
							case 1u:
								array[num13] = (byte)num10;
								array[num13 + 1] = (byte)num11;
								array[num13 + 2] = (byte)num12;
								break;
							case 2u:
								if( num4 > num5 )
								{
									array[num13] = (byte)( ( 2 * num7 + num10 ) / 3 );
									array[num13 + 1] = (byte)( ( 2 * num8 + num11 ) / 3 );
									array[num13 + 2] = (byte)( ( 2 * num9 + num12 ) / 3 );
								}
								else
								{
									array[num13] = (byte)( ( num7 + num10 ) / 2 );
									array[num13 + 1] = (byte)( ( num8 + num11 ) / 2 );
									array[num13 + 2] = (byte)( ( num9 + num12 ) / 2 );
								}
								break;
							case 3u:
								if( num4 > num5 )
								{
									array[num13] = (byte)( ( num7 + 2 * num10 ) / 3 );
									array[num13 + 1] = (byte)( ( num8 + 2 * num11 ) / 3 );
									array[num13 + 2] = (byte)( ( num9 + 2 * num12 ) / 3 );
								}
								else
								{
									array[num13] = 0;
									array[num13 + 1] = 0;
									array[num13 + 2] = 0;
								}
								break;
						}
						num6 >>= 2;
					}
				}
			}
		}
		return array;
	}

	private byte[] a( byte[] A_0, int A_1, int A_2 )
	{
		byte[] array = new byte[A_1 * A_2 * 4];
		int num = A_1 / 4;
		int num2 = A_2 / 4;
		for( int i = 0; i < num2; i++ )
		{
			for( int j = 0; j < num; j++ )
			{
				int num3 = ( i * num + j ) * 8;
				uint num4 = (uint)BitConverter.ToUInt16( A_0, num3 );
				uint num5 = (uint)BitConverter.ToUInt16( A_0, num3 + 2 );
				uint num6 = BitConverter.ToUInt32( A_0, num3 + 4 );
				ushort num7 = (ushort)( 8u * ( num4 & 31u ) );
				ushort num8 = (ushort)( 4u * ( num4 >> 5 & 63u ) );
				ushort num9 = (ushort)( 8u * ( num4 >> 11 & 31u ) );
				ushort num10 = (ushort)( 8u * ( num5 & 31u ) );
				ushort num11 = (ushort)( 4u * ( num5 >> 5 & 63u ) );
				ushort num12 = (ushort)( 8u * ( num5 >> 11 & 31u ) );
				for( int k = 0; k < 4; k++ )
				{
					for( int l = 0; l < 4; l++ )
					{
						int num13 = A_1 * ( i * 4 + k ) * 4 + ( j * 4 + l ) * 4;
						switch( num6 & 3u )
						{
							case 0u:
								array[num13] = (byte)num7;
								array[num13 + 1] = (byte)num8;
								array[num13 + 2] = (byte)num9;
								array[num13 + 3] = 255;
								break;
							case 1u:
								array[num13] = (byte)num10;
								array[num13 + 1] = (byte)num11;
								array[num13 + 2] = (byte)num12;
								array[num13 + 3] = 255;
								break;
							case 2u:
								array[num13 + 3] = 255;
								if( num4 > num5 )
								{
									array[num13] = (byte)( ( 2 * num7 + num10 ) / 3 );
									array[num13 + 1] = (byte)( ( 2 * num8 + num11 ) / 3 );
									array[num13 + 2] = (byte)( ( 2 * num9 + num12 ) / 3 );
								}
								else
								{
									array[num13] = (byte)( ( num7 + num10 ) / 2 );
									array[num13 + 1] = (byte)( ( num8 + num11 ) / 2 );
									array[num13 + 2] = (byte)( ( num9 + num12 ) / 2 );
								}
								break;
							case 3u:
								if( num4 > num5 )
								{
									array[num13] = (byte)( ( num7 + 2 * num10 ) / 3 );
									array[num13 + 1] = (byte)( ( num8 + 2 * num11 ) / 3 );
									array[num13 + 2] = (byte)( ( num9 + 2 * num12 ) / 3 );
									array[num13 + 3] = 255;
								}
								else
								{
									array[num13] = 0;
									array[num13 + 1] = 0;
									array[num13 + 2] = 0;
									array[num13 + 3] = 0;
								}
								break;
						}
						num6 >>= 2;
					}
				}
			}
		}
		return array;
	}
}
