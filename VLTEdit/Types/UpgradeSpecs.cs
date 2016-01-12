using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class UpgradeSpecs : VLTBaseType
	{
		private uint upgrdui1;
		private uint upgrdui2;
		[DataValue( "Class" )]
		public string usclass;
		[DataValue( "Collection" )]
		public string uscollection;
		[DataValue( "Level" )]
		public uint level;

		public override void read( BinaryReader br )
		{
			this.upgrdui1 = br.ReadUInt32();
			this.upgrdui2 = br.ReadUInt32();
			br.ReadInt32();
			this.level = br.ReadUInt32();
			this.usclass = HashTracker.getValueForHash( this.upgrdui1 );
			this.uscollection = HashTracker.getValueForHash( this.upgrdui2 );
		}

		public override void write( BinaryWriter bw )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.usclass ) );
			//A_0.Write( HashUtil.getHash32( this.uscollection ) );
			bw.Write( this.upgrdui1 );
			bw.Write( this.upgrdui2 );
			bw.Write( 0 );
			bw.Write( this.level );
		}
	}
}
