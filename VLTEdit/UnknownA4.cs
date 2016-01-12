using System.IO;

namespace NFSTools.VLTEdit
{
	public class UnknownA4 : UnknownC0
	{
		private byte[] ba1;

		public override void read( BinaryReader br )
		{
			this.ba1 = br.ReadBytes( this.e1.dataSize() );
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.ba1 );
		}
	}
}
