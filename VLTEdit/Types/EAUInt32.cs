using System.IO;

namespace VLTEdit.Types
{
	public class EAUInt32 : EABaseType
	{
		[DataValue( "Value" )]
		public uint value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadUInt32();
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
