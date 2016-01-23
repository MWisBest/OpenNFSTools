using System.IO;
using NFSTools.LibNFS.Common;

namespace NFSTools.VLTEdit.Table
{
	public class RowRecord : BaseRecord
	{
		public class aclz : IBinReadWrite // Should be OK for Carbon?
		{
			public uint hash;
			public int position;
			private uint spacer;
			private short sh1;
			private byte flag1;
			private byte flag2;

			public bool a()
			{
				return ( this.flag1 & 32 ) != 0 || ( this.flag1 & 64 ) != 0;
			}

			public void read( BinaryReader br )
			{
				this.hash = br.ReadUInt32();

				// position is casted here because the main thing that references this needs it as an int anyway.
				this.position = (int)br.BaseStream.Position;
				this.spacer = br.ReadUInt32();
				this.sh1 = br.ReadInt16();
				this.flag1 = br.ReadByte();
				this.flag2 = br.ReadByte(); // Always 0 in NFS:MW
			}

			public void write( BinaryWriter bw )
			{
				bw.Write( this.hash );
				bw.Write( this.spacer );
				bw.Write( this.sh1 );
				bw.Write( this.flag1 );
				bw.Write( this.flag2 );
			}
		}

		public uint hash;
		public uint ui2;
		public uint ui3; // This is a hash of some sort as well.
		public int i1; // NFS:C --> Removed?
		private int i2;
		private int i3;
		private ushort i4;
		private ushort carbonsomething;
		public int position;
		private uint spacer;
		private uint[] uia1;
		private uint[] extraHashes;
		public RowRecord.aclz[] caa1;

		public override void read( BinaryReader br )
		{
			this.hash = br.ReadUInt32(); //hash in NFS:MW at least...
			this.ui2 = br.ReadUInt32();
			this.ui3 = br.ReadUInt32();
			this.i1 = br.ReadInt32();
			this.i2 = br.ReadInt32(); // Always 0 in NFS:MW
			this.i3 = br.ReadInt32();
			this.i4 = br.ReadUInt16();
			this.carbonsomething = br.ReadUInt16();

			// position is casted here because the only place that references this needs it as an int anyway.
			this.position = (int)br.BaseStream.Position;

			this.spacer = br.ReadUInt32(); // VLTConstants.MW_DEADBEEF or VLTConstants.CARBON_SPACER

			this.uia1 = new uint[this.i4]; // NOTE: Prone to overflow in NFS:C if something is even *slightly* off.
			for( int i = 0; i < this.i4; ++i )
			{
				// parent hashes?
				this.uia1[i] = br.ReadUInt32();
			}

			if( this.carbonsomething > 0 )
			{
				this.extraHashes = new uint[this.carbonsomething];

				// Where the hell are these hashes coming from? What are they for?!
				// TODO: This is DEFINITELY a major issue.

				for( ushort num = 0; num < this.carbonsomething; ++num )
				{
					this.extraHashes[num] = br.ReadUInt32();
				}
			}

			this.caa1 = new RowRecord.aclz[this.i1];
			for( int j = 0; j < this.i1; ++j )
			{
				this.caa1[j] = new RowRecord.aclz();
				this.caa1[j].read( br );
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.hash );
			bw.Write( this.ui2 );
			bw.Write( this.ui3 );
			bw.Write( this.i1 );
			bw.Write( this.i2 );
			bw.Write( this.i3 );
			bw.Write( this.i4 );
			bw.Write( this.carbonsomething );
			bw.Write( this.spacer );
			for( int i = 0; i < this.i4; ++i )
			{
				bw.Write( this.uia1[i] );
			}
			for( int j = 0; j < this.i1; ++j )
			{
				this.caa1[j].write( bw );
			}
		}
	}
}
