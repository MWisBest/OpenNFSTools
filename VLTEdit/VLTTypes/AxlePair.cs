using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AxlePair : VLTDataItem
	{
		[DataValue( "Front" )]
		public float Value1;
		[DataValue( "Rear" )]
		public float Value2;

		public override void Read( BinaryReader br )
		{
			this.Value1 = br.ReadSingle();
			this.Value2 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value1 );
			bw.Write( this.Value2 );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.Value1, this.Value2 );
		}
	}
}
