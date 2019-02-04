using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class JunkmanMod : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue( "Class" )]
		public string Class;
		[DataValue( "Collection" )]
		public string RowName;
		[DataValue( "Factor" )]
		public float Factor;

		public override void Read( BinaryReader br )
		{
			this.ClassHash = br.ReadUInt32();
			this.RowNameHash = br.ReadUInt32();
			this.Factor = br.ReadSingle();

			this.Class = HashResolver.Resolve( this.ClassHash );
			this.RowName = HashResolver.Resolve( this.RowNameHash );
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( VLTHasher.Hash( this.Class ) );
			bw.Write( VLTHasher.Hash( this.RowName ) );
			bw.Write( 0 );
			bw.Write( this.Factor );
		}
	}
}
