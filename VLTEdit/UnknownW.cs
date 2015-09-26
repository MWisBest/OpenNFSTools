using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownW : UnknownDI
	{
		private int i1;
		private int i2;
		private int i3;
		private int i4;
		private int[] ia1;

		public int b( int A_0 )
		{
			return this.ia1[A_0];
		}

		public void a( int A_0, int A_1 )
		{
			this.ia1[A_0] = A_1;
		}

		public int b()
		{
			return this.i3;
		}

		public int c()
		{
			return this.i1;
		}

		public void a( int A_0 )
		{
			this.i1 = A_0;
		}

		public int d()
		{
			return this.i2;
		}

		public void c( int A_0 )
		{
			this.i2 = A_0;
		}

		public int a()
		{
			return this.i4;
		}

		public override void read( BinaryReader A_0 )
		{
			this.i1 = A_0.ReadInt32();
			this.i2 = A_0.ReadInt32();
			this.i3 = A_0.ReadInt32();
			this.i4 = (int)A_0.BaseStream.Position;
			if( !BuildConfig.CARBON )
			{
				A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.ia1 = new int[this.i3];
			for( int i = 0; i < this.i3; ++i )
			{
				this.ia1[i] = A_0.ReadInt32();
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.i1 );
			A_0.Write( this.i2 );
			A_0.Write( this.i3 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( VLTConstants.MW_DEADBEEF );
			}
			for( int i = 0; i < this.i3; ++i )
			{
				A_0.Write( this.ia1[i] );
			}
		}
	}
}
