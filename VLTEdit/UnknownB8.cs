using System.IO;

namespace VLTEdit
{
	public class UnknownB8 : IBinReadWrite
	{
		private int i1;
		private short s1;
		private short s2;
		private int i2;

		public int c()
		{
			return this.i1;
		}

		public void b( int A_0 )
		{
			this.i1 = A_0;
		}

		public short d()
		{
			return this.s1;
		}

		public void a( short A_0 )
		{
			this.s1 = A_0;
		}

		public short a()
		{
			return this.s2;
		}

		public void b( short A_0 )
		{
			this.s2 = A_0;
		}

		public int b()
		{
			return this.i2;
		}

		public void a( int A_0 )
		{
			this.i2 = A_0;
		}

		public void read( BinaryReader A_0 )
		{
			this.i1 = A_0.ReadInt32();
			this.s1 = A_0.ReadInt16();
			this.s2 = A_0.ReadInt16();
			this.i2 = A_0.ReadInt32();
		}

		public void write( BinaryWriter A_0 )
		{
			A_0.Write( this.i1 );
			A_0.Write( this.s1 );
			A_0.Write( this.s2 );
			A_0.Write( this.i2 );
		}
	}
}
