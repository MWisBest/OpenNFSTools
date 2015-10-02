using System.IO;

namespace VLTEdit
{
	public class UnknownE : IBinReadWrite
	{
		public VLTOtherValue ce1;
		private int i1;
		private long position;

		public int a()
		{
			return this.i1 - 8;
		}

		public bool c()
		{
			return this.i1 >= 8;
		}

		public void a( Stream A_0 )
		{
			A_0.Seek( this.position + 8L, SeekOrigin.Begin );
		}

		public void b( Stream A_0 )
		{
			A_0.Seek( this.position + this.i1, SeekOrigin.Begin );
		}

		public void read( BinaryReader br )
		{
			this.position = br.BaseStream.Position;
			this.ce1 = (VLTOtherValue)br.ReadInt32();
			this.i1 = br.ReadInt32();
		}

		public void write( BinaryWriter bw )
		{
			bw.Write( (uint)this.ce1 );
			bw.Write( this.i1 );
		}
	}
}
