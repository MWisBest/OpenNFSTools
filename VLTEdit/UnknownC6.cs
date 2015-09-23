using System;
using System.IO;

namespace VLTEdit
{
	public class UnknownC6 : UnknownDI
	{
		private uint ui1;
		private int i1;
		private int i2;
		private int i3;
		private int i4;
		private int i5;
		private int i6;
		private int i7;

		public uint b()
		{
			return this.ui1;
		}

		public int e()
		{
			return this.i1;
		}

		public void b( int A_0 )
		{
			this.i1 = A_0;
		}

		public int f()
		{
			return this.i4;
		}

		public void c( int A_0 )
		{
			this.i4 = A_0;
		}

		public int c()
		{
			return this.i2;
		}

		public void a( int A_0 )
		{
			this.i2 = A_0;
		}
		public int d()
		{
			return this.i6;
		}

		public void d( int A_0 )
		{
			this.i6 = A_0;
		}

		public int a()
		{
			return this.i3;
		}

		public override void read( BinaryReader A_0 )
		{
			this.ui1 = A_0.ReadUInt32();
			this.i1 = A_0.ReadInt32();
			this.i2 = A_0.ReadInt32();
			this.i3 = (int)A_0.BaseStream.Position;
			if( !BuildConfig.CARBON )
			{
				A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.i4 = A_0.ReadInt32();
			this.i5 = A_0.ReadInt32();
			this.i6 = A_0.ReadInt32();
			this.i7 = A_0.ReadInt32();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ui1 );
			A_0.Write( this.i1 );
			A_0.Write( this.i2 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( VLTConstants.MW_DEADBEEF );
			}
			A_0.Write( this.i4 );
			A_0.Write( this.i5 );
			A_0.Write( this.i6 );
			A_0.Write( this.i7 );
		}
	}
}
