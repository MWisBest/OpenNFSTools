using System.IO;

namespace VLTEdit
{
	public class EAUInt8 : EABaseType
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
