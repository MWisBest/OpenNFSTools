using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class EAFloat : VLTBaseType
	{
		[DataValue( "Value" )]
		public float value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadSingle();
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
