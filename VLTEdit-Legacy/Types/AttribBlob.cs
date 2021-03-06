using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class AttribBlob : VLTBaseType
	{
		[DataValue( "Length", Hex = true )]
		public uint length;
		[DataValue( "Offset", Hex = true )]
		public uint offset;

		public override void read( BinaryReader br )
		{
			this.length = br.ReadUInt32();
			this.offset = br.ReadUInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.length );
			bw.Write( this.offset );
		}
	}
}
