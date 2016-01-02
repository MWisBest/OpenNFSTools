using System.IO;

namespace VLTEdit.Table
{
	public class ClassRecord : BaseRecord
	{
		public uint hash;
		private int i1;
		public int i2;
		public int position;
		private uint spacer;
		private int i4;
		private int i5;
		public int i6;
		private int i7;

		public override void read( BinaryReader br )
		{
			this.hash = br.ReadUInt32();
			this.i1 = br.ReadInt32();
			this.i2 = br.ReadInt32();

			// position is casted here because the main thing that references this needs it as an int anyway.
			this.position = (int)br.BaseStream.Position;

			this.spacer = br.ReadUInt32(); // VLTConstants.MW_DEADBEEF or VLTConstants.CARBON_SPACER

			this.i4 = br.ReadInt32();
			this.i5 = br.ReadInt32();
			this.i6 = br.ReadInt32();
			this.i7 = br.ReadInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.hash );
			bw.Write( this.i1 );
			bw.Write( this.i2 );
			bw.Write( this.spacer );
			bw.Write( this.i4 );
			bw.Write( this.i5 );
			bw.Write( this.i6 );
			bw.Write( this.i7 );
		}
	}
}
