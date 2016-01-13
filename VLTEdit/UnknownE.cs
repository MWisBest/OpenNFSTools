using System.IO;
using NFSTools.LibNFS.Common;

namespace NFSTools.VLTEdit
{
	public class UnknownE : IBinReadWrite
	{
		public VLTOtherValue ce1;
		private int blockLen; // from VLTOtherValue to next one?
		private long position;

		public int dataSize()
		{
			return this.blockLen - 8;
		}

		public bool isBlank()
		{
			return this.blockLen < 8; // only time this is true is at the end of a file as far as I know.
		}

		public void seekToDataStart( Stream A_0 )
		{
			A_0.Seek( this.position + 8L, SeekOrigin.Begin );
		}

		public void seekToNextBlock( Stream A_0 )
		{
			A_0.Seek( this.position + this.blockLen, SeekOrigin.Begin );
		}

		public void read( BinaryReader br )
		{
			this.position = br.BaseStream.Position;
			this.ce1 = (VLTOtherValue)br.ReadInt32();
			this.blockLen = br.ReadInt32();
			return;
		}

		public void write( BinaryWriter bw )
		{
			bw.Write( (uint)this.ce1 );
			bw.Write( this.blockLen );
		}
	}
}
