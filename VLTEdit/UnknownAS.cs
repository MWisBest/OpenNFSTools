using System.IO;
using VLTEdit.Table;

namespace VLTEdit
{
	public class UnknownAS : IBinReadWrite // NOTE: This seems like actual table entires read...
	{
		public uint ui1;
		public EntryType b21;
		private int i1; //NFS:C --> Gone!
		private int i2;
		private int i3;
		public BaseEntry di1;

		public void b( BinaryReader A_0 ) // TODO: NFS:C
		{
			// NFS:C  880012749 (BAD!) --> -1911476553 (BAD!!)
			// NFS:MW many many times: 72, 464, 496, 536, 576, 616, 656, 696, 736, ... 162488, 162544, 162600
			//Console.WriteLine( "AS: i3 = " + this.i3.ToString() ); // Problem must be before this;
			A_0.BaseStream.Seek( this.i3, SeekOrigin.Begin ); // NFS:C EndOfStream Exception! FIXED?!
			this.di1 = BaseEntry.a( this.b21 );
			this.di1.as1 = this;
			this.di1.read( A_0 );
		}

		public void read( BinaryReader br )
		{
			this.ui1 = br.ReadUInt32();
			this.b21 = (EntryType)br.ReadUInt32();
			this.i1 = br.ReadInt32();
			if( this.i1 != 0 )
			{
				this.i2 = this.i1;
			}
			else
			{
				this.i2 = br.ReadInt32();
			}
			this.i3 = br.ReadInt32();
		}

		public void write( BinaryWriter bw )
		{
			bw.Write( this.ui1 );
			bw.Write( (uint)this.b21 );
			if( this.i1 == 0 )
			{
				bw.Write( this.i1 ); // Removed in NFS:C
			}
			bw.Write( this.i2 );
			bw.Write( this.i3 );
		}
	}
}
