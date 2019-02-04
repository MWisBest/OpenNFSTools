using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EABool : VLTDataItem
	{
		[DataValue( "Value" )]
		public bool Value;

		public override void Read( BinaryReader br )
		{
			this.Value = ( br.ReadByte() == 1 );
		}

		public override void Write( BinaryWriter bw )
		{
			if( this.Value )
			{
				bw.Write( (byte)1 );
			}
			else
			{
				bw.Write( (byte)0 );
			}
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
}
