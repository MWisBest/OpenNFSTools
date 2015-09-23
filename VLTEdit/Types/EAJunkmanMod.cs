using System;
using System.IO;

namespace VLTEdit
{
	public class EAJunkmanMod : EABaseType // OBF: at.cs
	{
		private uint ui1;
		private uint ui2;
		[DataValue( "Class" )]
		public string jmclass;
		[DataValue( "Collection" )]
		public string jmcollection;
		[DataValue( "Factor" )]
		public float factor;

		public override void read( BinaryReader A_0 )
		{
			this.ui1 = A_0.ReadUInt32();
			this.ui2 = A_0.ReadUInt32();
			this.factor = A_0.ReadSingle();
			this.jmclass = HashTracker.getValueForHash( this.ui1 );
			this.jmcollection = HashTracker.getValueForHash( this.ui2 );
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( HashUtil.getHash32( this.jmclass ) );
			A_0.Write( HashUtil.getHash32( this.jmcollection ) );
			A_0.Write( 0 );
			A_0.Write( this.factor );
		}
	}
}
