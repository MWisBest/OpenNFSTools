using System.IO;

namespace NFSTools.NFSLib.Common
{
	public interface IBinReadWrite
	{
		void read( BinaryReader br );

		void write( BinaryWriter bw );
	}
}
