using System.IO;

namespace VLTEdit
{
	public class EAStringKey : EABaseType
	{
		[DataValue( "Hash64", Hex = true )]
		public ulong hash64;
		[DataValue( "Hash32", Hex = true )]
		public uint hash32;
		[DataValue( "Offset", Hex = true )]
		public uint offset;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader A_0 )
		{
			this.hash64 = A_0.ReadUInt64();
			this.hash32 = A_0.ReadUInt32();
			this.offset = A_0.ReadUInt32();
			if( this.offset == 0u )
			{
				this.value = "(null)";
				return;
			}
			if( this.offset > (ulong)A_0.BaseStream.Length )
			{
				this.value = "(offset is outta here)";
				return;
			}
			long position = A_0.BaseStream.Position;
			A_0.BaseStream.Seek( (long)( (ulong)this.offset ), SeekOrigin.Begin );
			this.value = UnknownAP.a( A_0 );
			A_0.BaseStream.Seek( position, SeekOrigin.Begin );
		}

		public override void write( BinaryWriter A_0 )
		{
			if( this.offset == 0u )
			{
				A_0.Write( 0uL );
				A_0.Write( 0u );
			}
			else
			{
				A_0.Write( this.hash64 );
				A_0.Write( this.hash32 );
			}
			A_0.Write( this.offset );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
