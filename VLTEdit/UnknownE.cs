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

		public void read( BinaryReader A_0 )
		{
			this.position = A_0.BaseStream.Position;
			this.ce1 = (VLTOtherValue)A_0.ReadInt32();
			this.i1 = A_0.ReadInt32();
		}

		public void write( BinaryWriter A_0 )
		{
			A_0.Write( (uint)this.ce1 );
			A_0.Write( this.i1 );
		}
	}
}
