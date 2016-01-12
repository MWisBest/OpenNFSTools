using System.IO;

namespace NFSTools.VLTEdit
{
	public interface IBinReadWrite
	{
		void read( BinaryReader br );

		void write( BinaryWriter bw );
	}
}
