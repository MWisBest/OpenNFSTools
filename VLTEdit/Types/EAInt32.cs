using System.IO;

namespace VLTEdit
{
	public class EAInt32 : EABaseType
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
