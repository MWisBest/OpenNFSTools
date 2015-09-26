using System.IO;

namespace VLTEdit
{
	public interface IBinReadWrite
	{
		void read( BinaryReader br );

		void write( BinaryWriter bw );
	}
}
