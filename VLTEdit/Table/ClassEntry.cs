using System.IO;

namespace VLTEdit.Table
{
	public class ClassEntry : BaseEntry
	{
		public uint ui1;
		private int i1;
		public int i2;
		public int position;
		private int i4;
		private int i5;
		public int i6;
		private int i7;

		public override void read( BinaryReader br )
		{
			this.ui1 = br.ReadUInt32();
			this.i1 = br.ReadInt32();
			this.i2 = br.ReadInt32();

			// position is casted here because the main thing that references this needs it as an int anyway.
			this.position = (int)br.BaseStream.Position;

			if( !BuildConfig.CARBON )
			{
				br.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.i4 = br.ReadInt32();
			this.i5 = br.ReadInt32();
			this.i6 = br.ReadInt32();
			this.i7 = br.ReadInt32();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.ui1 );
			bw.Write( this.i1 );
			bw.Write( this.i2 );
			if( !BuildConfig.CARBON )
			{
				bw.Write( VLTConstants.MW_DEADBEEF );
			}
			bw.Write( this.i4 );
			bw.Write( this.i5 );
			bw.Write( this.i6 );
			bw.Write( this.i7 );
		}
	}
}
