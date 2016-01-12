using System.IO;

namespace NFSTools.VLTEdit
{
	public class UnknownB8 : IBinReadWrite
	{
		public int i1;
		public short s1;
		public short s2;
		public int i2;

		public void read( BinaryReader br )
		{
			this.i1 = br.ReadInt32();
			this.s1 = br.ReadInt16();
			this.s2 = br.ReadInt16();
			this.i2 = br.ReadInt32();
		}

		public void write( BinaryWriter bw )
		{
			bw.Write( this.i1 );
			bw.Write( this.s1 );
			bw.Write( this.s2 );
			bw.Write( this.i2 );
		}
	}
}
