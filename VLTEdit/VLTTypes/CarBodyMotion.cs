using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class CarBodyMotion : VLTDataItem
	{
		[DataValue( "Value1" )]
		public float Value1;
		[DataValue( "Value2" )]
		public float Value2;
		[DataValue( "Value3" )]
		public float Value3;

		public override void Read( BinaryReader br )
		{
			this.Value1 = br.ReadSingle();
			this.Value2 = br.ReadSingle();
			this.Value3 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value1 );
			bw.Write( this.Value2 );
			bw.Write( this.Value3 );
		}
	}
}
