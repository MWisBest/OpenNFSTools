using NFSTools.LibNFS.Crypto;
using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AttribRefSpec : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue( "Class" )]
		public string Class;
		[DataValue( "Collection" )]
		public string RowName;

		public override void Read( BinaryReader br )
		{
			this.ClassHash = br.ReadUInt32();
			this.RowNameHash = br.ReadUInt32();
			br.ReadInt32();
			this.Class = HashResolver.Resolve( this.ClassHash );
			this.RowName = HashResolver.Resolve( this.RowNameHash );
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( JenkinsHash.getHash32( this.Class ) );
			bw.Write( JenkinsHash.getHash32( this.RowName ) );
			bw.Write( 0 );
		}

		public override string ToString()
		{
			return this.Class + " -> " + this.RowName;
		}
	}
}
