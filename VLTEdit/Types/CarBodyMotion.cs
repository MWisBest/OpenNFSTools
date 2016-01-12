using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class CarBodyMotion : VLTBaseType
	{
		[DataValue( "Value1" )]
		public float value1;
		[DataValue( "Value2" )]
		public float value2;
		[DataValue( "Value3" )]
		public float value3;

		public override void read( BinaryReader br )
		{
			this.value1 = br.ReadSingle();
			this.value2 = br.ReadSingle();
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
