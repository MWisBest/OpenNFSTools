using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class EAInt64 : VLTBaseType
	{
		[DataValue( "Value" )]
		public long value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadInt64();
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
