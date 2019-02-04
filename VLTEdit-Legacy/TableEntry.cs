using System.IO;
using NFSTools.LibNFS.Common;
using NFSTools.VLTEditLegacy.Table;

namespace NFSTools.VLTEditLegacy
{
	public class TableEntry : IBinReadWrite // NOTE: This seems like actual table entires read...
	{
		public uint hash;
		public EntryType entryType;
		private int mwEmpty; //NFS:C and up --> Gone!
		private int i2;
		private int addr;
		public BaseRecord di1;

		public void b( BinaryReader A_0 )
		{
			A_0.BaseStream.Seek( this.addr, SeekOrigin.Begin );
			this.di1 = BaseRecord.a( this.entryType );
			this.di1.as1 = this;
			this.di1.read( A_0 );
		}

		public void read( BinaryReader br )
		{
			this.hash = br.ReadUInt32();
			this.entryType = (EntryType)br.ReadUInt32();
			this.mwEmpty = br.ReadInt32();
			if( this.mwEmpty != 0 )
			{
				this.i2 = this.mwEmpty;
			}
			else
			{
				this.i2 = br.ReadInt32();
			}
			this.addr = br.ReadInt32();
		}

		public void write( BinaryWriter bw )
		{
			bw.Write( this.hash );
			bw.Write( (uint)this.entryType );
			if( this.mwEmpty == 0 )
			{
				bw.Write( this.mwEmpty ); // Removed in NFS:C
			}
			bw.Write( this.i2 );
			bw.Write( this.addr );
		}
	}
}
