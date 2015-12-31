using System.IO;

namespace VLTEdit.Types
{
	public class EAInt32 : VLTBaseType
	{
		[DataValue( "Value" )]
		public int value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadInt32();
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
