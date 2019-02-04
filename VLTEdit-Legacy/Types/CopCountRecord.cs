using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class CopCountRecord : VLTBaseType
	{
		private uint row_hash;

		[DataValue( "??? #1 (UInt32?)", Hex = true )]
		public uint value1;
		[DataValue( "??? #2 (UInt32?)", Hex = true )]
		public uint value2;
		[DataValue( "Row" )]
		public string value3;
		[DataValue( "??? #4 (UInt32?)" )]
		public uint value4;
		[DataValue( "Max Active (UInt32)" )]
		public uint value5;
		[DataValue( "??? #6 (UInt32?)" )]
		public uint value6;

		public override void read( BinaryReader br )
		{
			this.value1 = br.ReadUInt32();
			this.value2 = br.ReadUInt32();
			this.row_hash = br.ReadUInt32();
			this.value3 = HashTracker.getValueForHash( this.row_hash );
			this.value4 = br.ReadUInt32();
			this.value5 = br.ReadUInt32();
			this.value6 = br.ReadUInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.value1 );
			bw.Write( this.value2 );
			bw.Write( this.row_hash );
			bw.Write( this.value4 );
			bw.Write( this.value5 );
			bw.Write( this.value6 );
		}
	}
}
