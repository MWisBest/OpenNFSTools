using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EADouble : VLTDataItem
	{
		[DataValue( "Value" )]
		public double Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadDouble();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
}
