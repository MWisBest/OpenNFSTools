using System.IO;

namespace VLTEdit
{
	public class EAUInt64 : EABaseType
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
