using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class JunkmanMod : VLTBaseType // OBF: at.cs
	{
		private uint junkui1;
		private uint junkui2;
		[DataValue( "Class" )]
		public string jmclass;
		[DataValue( "Collection" )]
		public string jmcollection;
		[DataValue( "Factor" )]
		public float factor;

		public override void read( BinaryReader br )
		{
			this.junkui1 = br.ReadUInt32();
			this.junkui2 = br.ReadUInt32();
			this.factor = br.ReadSingle();
			this.jmclass = HashTracker.getValueForHash( this.junkui1 );
			this.jmcollection = HashTracker.getValueForHash( this.junkui2 );
		}

		public override void write( BinaryWriter bw )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.jmclass ) );
			//A_0.Write( HashUtil.getHash32( this.jmcollection ) );
			bw.Write( this.junkui1 );
			bw.Write( this.junkui2 );

			// TODO: Why on earth was this writing an extra uint? Shouldn't it be reading it up above then??
			//A_0.Write( 0 );

			bw.Write( this.factor );
		}
	}
}
