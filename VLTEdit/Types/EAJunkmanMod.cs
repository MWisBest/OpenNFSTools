using System.IO;

namespace VLTEdit
{
	public class EAJunkmanMod : EABaseType // OBF: at.cs
	{
		private uint junkui1;
		private uint junkui2;
		[DataValue( "Class" )]
		public string jmclass;
		[DataValue( "Collection" )]
		public string jmcollection;
		[DataValue( "Factor" )]
		public float factor;

		public override void read( BinaryReader A_0 )
		{
			this.junkui1 = A_0.ReadUInt32();
			this.junkui2 = A_0.ReadUInt32();
			this.factor = A_0.ReadSingle();
			this.jmclass = HashTracker.getValueForHash( this.junkui1 );
			this.jmcollection = HashTracker.getValueForHash( this.junkui2 );
		}

		public override void write( BinaryWriter A_0 )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.jmclass ) );
			//A_0.Write( HashUtil.getHash32( this.jmcollection ) );
			A_0.Write( this.junkui1 );
			A_0.Write( this.junkui2 );

			// TODO: Why on earth was this writing an extra uint? Shouldn't it be reading it up above then??
			//A_0.Write( 0 );

			A_0.Write( this.factor );
		}
	}
}
