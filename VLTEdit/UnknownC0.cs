using System.IO;

namespace VLTEdit
{
	public abstract class UnknownC0 : IBinReadWrite
	{
		public UnknownE e1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );
	}
}
