using System.IO;
using NFSTools.LibNFS.Common;

namespace NFSTools.VLTEditLegacy
{
	public abstract class UnknownC0 : IBinReadWrite
	{
		public UnknownE e1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );
	}
}
