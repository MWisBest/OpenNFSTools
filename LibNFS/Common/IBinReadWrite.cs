using System.IO;

namespace NFSTools.LibNFS.Common
{
	public interface IBinReadWrite
	{
		void read( BinaryReader br );

		void write( BinaryWriter bw );
	}
}
