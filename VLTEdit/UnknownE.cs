using System;
using System.IO;

namespace VLTEdit
{
	public class UnknownE : IBinReadWrite
	{
		private VLTOtherValue ce1;
		private int i1;
		private long l1;

		public VLTOtherValue d()
		{
			return this.ce1;
		}

		public void a( VLTOtherValue A_0 )
		{
			this.ce1 = A_0;
		}

		public int b()
		{
			return this.i1;
		}

		public void a( int A_0 )
		{
			this.i1 = A_0;
		}

		public int a()
		{
			return this.i1 - 8;
		}

		public void b( int A_0 )
		{
			this.i1 = A_0 + 8;
		}

		public bool c()
		{
			return this.i1 >= 8;
		}

		public void a( Stream A_0 )
		{
			A_0.Seek( this.l1 + 8L, 0 );
		}

		public void b( Stream A_0 )
		{
			A_0.Seek( this.l1 + (long)this.i1, 0 );
		}

		public void read( BinaryReader A_0 )
		{
			this.l1 = A_0.BaseStream.Position;
			this.ce1 = (VLTOtherValue)A_0.ReadInt32();
			this.i1 = A_0.ReadInt32();
		}

		public void write( BinaryWriter A_0 )
		{
			A_0.Write( (int)this.ce1 );
			A_0.Write( this.i1 );
		}
	}
}
