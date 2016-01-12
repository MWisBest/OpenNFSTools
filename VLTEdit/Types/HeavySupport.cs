using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class HeavySupport : VLTBaseType
	{
		[DataValue( "HeavyType? (UInt32)" )]
		public uint value1;
		[DataValue( "??? #2 (UInt32)", Hex = true )]
		public uint value2;
		[DataValue( "??? #3 (Float)" )]
		public float value3;
		[DataValue( "??? #4 (UInt32)" )]
		public uint value4;

		public override void read( BinaryReader br )
		{
			this.value1 = br.ReadUInt32();
			this.value2 = br.ReadUInt32();
			this.value3 = br.ReadSingle();
			this.value4 = br.ReadUInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.value1 );
			bw.Write( this.value2 );
			bw.Write( this.value3 );
			bw.Write( this.value4 );
		}
	}
}
