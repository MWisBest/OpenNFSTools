using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class EAInt8 : VLTBaseType
	{
		[DataValue( "Value" )]
		public sbyte value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadSByte();
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
