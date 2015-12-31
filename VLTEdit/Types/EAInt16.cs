using System.IO;

namespace VLTEdit.Types
{
	public class EAInt16 : VLTBaseType
	{
		[DataValue( "Value" )]
		public short value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadInt16();
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
