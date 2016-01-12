using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class EADouble : VLTBaseType
	{
		[DataValue( "Value" )]
		public double value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadDouble();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.value );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
