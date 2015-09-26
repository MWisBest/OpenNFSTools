using System.IO;

namespace VLTEdit
{
	public class EABlob : EABaseType
	{
		[DataValue( "Length", Hex = true )]
		public uint length;
		[DataValue( "Offset", Hex = true )]
		public uint offset;

		public override void read( BinaryReader A_0 )
		{
			this.length = A_0.ReadUInt32();
			this.offset = A_0.ReadUInt32();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.length );
			A_0.Write( this.offset );
		}
	}
}
