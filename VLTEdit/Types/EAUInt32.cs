using System.IO;

namespace VLTEdit
{
	public class EAUInt32 : EABaseType
	{
		[DataValue( "Value" )]
		public uint value;

		public override void read( BinaryReader A_0 )
		{
			this.value = A_0.ReadUInt32();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.value );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
