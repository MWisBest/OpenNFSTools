using System.IO;

namespace VLTEdit
{
	public class EAUpgradeSpecs : EABaseType
	{
		private uint upgrdui1;
		private uint upgrdui2;
		[DataValue( "Class" )]
		public string usclass;
		[DataValue( "Collection" )]
		public string uscollection;
		[DataValue( "Level" )]
		public uint level;

		public override void read( BinaryReader A_0 )
		{
			this.upgrdui1 = A_0.ReadUInt32();
			this.upgrdui2 = A_0.ReadUInt32();
			A_0.ReadInt32();
			this.level = A_0.ReadUInt32();
			this.usclass = HashTracker.getValueForHash( this.upgrdui1 );
			this.uscollection = HashTracker.getValueForHash( this.upgrdui2 );
		}

		public override void write( BinaryWriter A_0 )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.usclass ) );
			//A_0.Write( HashUtil.getHash32( this.uscollection ) );
			A_0.Write( this.upgrdui1 );
			A_0.Write( this.upgrdui2 );
			A_0.Write( 0 );
			A_0.Write( this.level );
		}
	}
}
