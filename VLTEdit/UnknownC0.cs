using System.IO;

namespace VLTEdit
{
	public abstract class UnknownC0 : IBinReadWrite
	{
		public UnknownE e1;

		public void a( UnknownE A_0 )
		{
			this.e1 = A_0;
		}

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );
	}
}
