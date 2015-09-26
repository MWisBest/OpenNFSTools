using System.IO;

namespace VLTEdit
{
	public class EARefSpec : EABaseType // OBF: dm.cs
	{
		private uint ui1;
		private uint ui2;
		[DataValue( "Class" )]
		public string refclass;
		[DataValue( "Collection" )]
		public string refcollection;

		public override void read( BinaryReader A_0 )
		{
			this.ui1 = A_0.ReadUInt32();
			this.ui2 = A_0.ReadUInt32();
			A_0.ReadInt32();
			this.refclass = HashTracker.getValueForHash( this.ui1 );
			this.refcollection = HashTracker.getValueForHash( this.ui2 );
		}

		public override void write( BinaryWriter A_0 )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing ui1 and ui2 for now.
			//A_0.Write( HashUtil.getHash32( this.refclass ) );
			//A_0.Write( HashUtil.getHash32( this.refcollection ) );
			A_0.Write( this.ui1 );
			A_0.Write( this.ui2 );

			A_0.Write( 0 );
		}

		public override string ToString()
		{
			return this.refclass + " -> " + this.refcollection;
		}
	}
}
