﻿using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class AttribStringKeyCarbon : VLTBaseType
	{
		[DataValue( "Hash32", Hex = true )]
		public uint hash32;
		[DataValue( "Offset", Hex = true )]
		public uint offset;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader br )
		{
			this.hash32 = br.ReadUInt32();
			this.offset = br.ReadUInt32();
			if( this.offset == 0u )
			{
				this.value = "(null)";
				return;
			}
			if( this.offset > (ulong)br.BaseStream.Length )
			{
				this.value = "(offset is outta here)";
				return;
			}
			long position = br.BaseStream.Position;
			br.BaseStream.Seek( (long)( (ulong)this.offset ), SeekOrigin.Begin );
			this.value = UnknownAP.a( br );
			br.BaseStream.Seek( position, SeekOrigin.Begin );
		}

		public override void write( BinaryWriter bw )
		{
			if( this.offset == 0u )
			{
				bw.Write( 0u );
			}
			else
			{
				bw.Write( this.hash32 );
			}
			bw.Write( this.offset );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
