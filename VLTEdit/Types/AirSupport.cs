using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class AirSupport : VLTBaseType
	{
		[DataValue( "ChopperType? (UInt32)" )]
		public uint value1;
		[DataValue( "??? (UInt32)" )]
		public uint value2;
		[DataValue( "FuelTime (Float)" )]
		public float value3;

		public override void read( BinaryReader br )
		{
			this.value1 = br.ReadUInt32();
			this.value2 = br.ReadUInt32();
			this.value3 = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.value1 );
			bw.Write( this.value2 );
			bw.Write( this.value3 );
		}
	}
}
