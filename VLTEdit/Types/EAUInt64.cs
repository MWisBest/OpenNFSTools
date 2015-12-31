using System.IO;

namespace VLTEdit.Types
{
	public class EAUInt64 : VLTBaseType
	{
		[DataValue( "Value" )]
		public ulong value;

		public override void read( BinaryReader br )
		{
			this.value = br.ReadUInt64();
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
