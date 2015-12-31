using System.IO;

namespace VLTEdit.Types
{
	public class AttribRefSpec : VLTBaseType // OBF: dm.cs
	{
		private uint refui1;
		private uint refui2;
		[DataValue( "Class" )]
		public string refclass;
		[DataValue( "Collection" )]
		public string refcollection;

		public override void read( BinaryReader br )
		{
			this.refui1 = br.ReadUInt32();
			this.refui2 = br.ReadUInt32();
			br.ReadInt32();
			this.refclass = HashTracker.getValueForHash( this.refui1 );
			this.refcollection = HashTracker.getValueForHash( this.refui2 );
		}

		public override void write( BinaryWriter bw )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.refclass ) );
			//A_0.Write( HashUtil.getHash32( this.refcollection ) );
			bw.Write( this.refui1 );
			bw.Write( this.refui2 );

			bw.Write( 0 );
		}

		public override string ToString()
		{
			return this.refclass + " -> " + this.refcollection;
		}
	}
}
