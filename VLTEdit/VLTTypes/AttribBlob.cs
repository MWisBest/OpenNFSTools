using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AttribBlob : VLTDataItem
	{
		[DataValue( "Length", Hex = true )]
		public uint DataLength;
		[DataValue( "Offset", Hex = true )]
		public uint DataOffset;

		public override void Read( BinaryReader br )
		{
			this.DataLength = br.ReadUInt32();
			this.DataOffset = br.ReadUInt32();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.DataLength );
			bw.Write( this.DataOffset );
		}
	}
}
