using System.IO;

namespace VLTEdit.Types
{
	public class EAUInt8 : VLTBaseType
	{
		[DataValue( "Value" )]
		public byte value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadByte();
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
