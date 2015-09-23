using System;
using System.IO;

namespace VLTEdit
{
	public class EAText : EABaseType
	{
		[DataValue( "Offset", Hex = true )]
		public uint offset;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader A_0 )
		{
			this.offset = A_0.ReadUInt32();
			if( (ulong)this.offset > (ulong)A_0.BaseStream.Length )
			{
				this.offset = base.i();
			}
			if( this.offset == 0u )
			{
				this.value = "(null)";
				return;
			}
			long position = A_0.BaseStream.Position;
			A_0.BaseStream.Seek( (long)( (ulong)this.offset ), 0 );
			this.value = UnknownAP.a( A_0 );
			A_0.BaseStream.Seek( position, 0 );
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.offset );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
