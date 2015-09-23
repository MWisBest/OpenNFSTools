using System;
using System.IO;

namespace VLTEdit
{
	public class EAGCollectionKey : EABaseType // OBF: al.cs
	{
		[DataValue( "Hash" )]
		public uint hash;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader A_0 )
		{
			this.hash = A_0.ReadUInt32();
			this.value = HashTracker.getValueForHash( this.hash );
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( HashUtil.getHash32( this.value ) );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
