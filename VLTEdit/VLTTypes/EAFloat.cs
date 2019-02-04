using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EAFloat : VLTDataItem
	{
		[DataValue( "Value" )]
		public float Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadSingle();
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
