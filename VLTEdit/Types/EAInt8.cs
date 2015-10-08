using System.IO;

namespace VLTEdit.Types
{
	public class EAInt8 : EABaseType
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
