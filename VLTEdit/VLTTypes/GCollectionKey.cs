using NFSTools.LibNFS.Crypto;
using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class GCollectionKey : VLTDataItem
	{
		[DataValue( "Hash" )]
		public uint Hash;
		[DataValue( "Value" )]
		public string Value;

		public override void Read( BinaryReader br )
		{
			this.Hash = br.ReadUInt32();
			this.Value = HashResolver.Resolve( this.Hash );
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( JenkinsHash.getHash32( this.Value ) );
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}
