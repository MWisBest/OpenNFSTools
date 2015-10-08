using System.IO;

namespace VLTEdit.Types
{
	public class EATrafficPatternRecord : EABaseType
	{
		private uint class_hash;
		private uint row_hash;

		[DataValue( "Class" )]
		public string clazz;
		[DataValue( "Row" )]
		public string row;
		[DataValue( "AlwaysZero (UInt32)" )]
		public uint value1;
		[DataValue( "Max1PerXCars (Float)" )]
		public float value2;
		[DataValue( "Value3 (UInt32)" )]
		public uint value3;
		[DataValue( "Frequency (UInt32)" )]
		public uint value4;

		public override void read( BinaryReader br )
		{
			this.class_hash = br.ReadUInt32();
			this.clazz = HashTracker.getValueForHash( this.class_hash );
			this.row_hash = br.ReadUInt32();
			this.row = HashTracker.getValueForHash( this.row_hash );
			this.value1 = br.ReadUInt32();
			this.value2 = br.ReadSingle();
			this.value3 = br.ReadUInt32();
			this.value4 = br.ReadUInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.class_hash );
			bw.Write( this.row_hash );
			bw.Write( this.value1 );
			bw.Write( this.value2 );
			bw.Write( this.value3 );
			bw.Write( this.value4 );
		}
	}
}
