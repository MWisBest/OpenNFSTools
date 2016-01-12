using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class EABool : VLTBaseType
	{
		[DataValue( "Value" )]
		public bool value;

		public override void read( BinaryReader br )
		{
			// TODO: Do we need to read an entire int here?
			this.value = br.ReadBoolean();
		}

		public override void write( BinaryWriter bw )
		{
			// As well as write one here?
			bw.Write( this.value );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
