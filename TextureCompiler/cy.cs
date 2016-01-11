using System;
using System.IO;

public class cy : d1
{
	private byte[] byteArrayA; // obf: "a"

	private long longB; // obf: "b"

	public cy() : base( a9.m )
	{
	}

	public void a( byte[] A_0 )
	{
		this.jB.b = (uint)A_0.Length;
		this.byteArrayA = A_0;
	}

	public byte[] a( long A_0, uint A_1 )
	{
		long num = A_0 - this.longB;
		byte[] array = new byte[A_1];
		Array.Copy( this.byteArrayA, num, array, 0L, (long)( (ulong)A_1 ) );
		return array;
	}

	public void a( long A_0, uint A_1, byte[] A_2 )
	{
		long num = A_0 - this.longB;
		if( (long)A_2.Length == (long)( (ulong)A_1 ) )
		{
			Array.Copy( A_2, 0L, this.byteArrayA, num, (long)( (ulong)A_1 ) );
			return;
		}
		long num2 = num + (long)A_2.Length + ( (long)this.byteArrayA.Length - num - (long)( (ulong)A_1 ) );
		byte[] array = new byte[num2];
		Array.Copy( this.byteArrayA, 0L, array, 0L, num );
		Array.Copy( A_2, 0L, array, num, (long)A_2.Length );
		Array.Copy( this.byteArrayA, num + (long)( (ulong)A_1 ), array, num + (long)A_2.Length, (long)this.byteArrayA.Length - num - (long)( (ulong)A_1 ) );
		this.byteArrayA = array;
		GC.Collect();
	}

	public override void a( BinaryReader A_0 )
	{
		this.longB = A_0.BaseStream.Position;
		if( this.jB.b > 0u )
		{
			this.byteArrayA = A_0.ReadBytes( (int)this.jB.b );
			return;
		}
		this.byteArrayA = null;
	}

	protected override void a( BinaryWriter A_0 )
	{
		this.longB = A_0.BaseStream.Position;
		if( this.jB.b > 0u )
		{
			A_0.Write( this.byteArrayA );
		}
	}
}
