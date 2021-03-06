using System.IO;

namespace NFSTools.VLTEditLegacy.Table
{
	public class RootRecord : BaseRecord
	{
		private int i1;
		private int i2;
		public int i3;
		public int position;
		private uint spacer;
		public int[] ia1;

		public override void read( BinaryReader br )
		{
			// comments on sides referencing NFS:MW db.vlt
			this.i1 = br.ReadInt32(); // offset: 0x48
			this.i2 = br.ReadInt32(); // ...
			this.i3 = br.ReadInt32(); // ...

			// position is casted here because the main thing that references this needs it as an int anyway.
			this.position = (int)br.BaseStream.Position; // we're now at 0x54

			this.spacer = br.ReadUInt32(); // VLTConstants.MW_DEADBEEF or VLTConstants.CARBON_SPACER

			this.ia1 = new int[this.i3];
			for( int i = 0; i < this.i3; ++i )
			{
				this.ia1[i] = br.ReadInt32();
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.i1 );
			bw.Write( this.i2 );
			bw.Write( this.i3 );
			bw.Write( this.spacer );
			for( int i = 0; i < this.i3; ++i )
			{
				bw.Write( this.ia1[i] );
			}
		}
	}
}
