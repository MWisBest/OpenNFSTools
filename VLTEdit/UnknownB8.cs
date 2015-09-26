using System.IO;

namespace VLTEdit
{
	public class UnknownB8 : IBinReadWrite
	{
		public int i1;
		public short s1;
		public short s2;
		public int i2;

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
