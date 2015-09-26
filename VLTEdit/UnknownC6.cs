using System.IO;

namespace VLTEdit
{
	public class UnknownC6 : UnknownDI
	{
		public uint ui1;
		private int i1;
		public int i2;
		public int i3;
		private int i4;
		private int i5;
		public int i6;
		private int i7;

		public override void read( BinaryReader A_0 )
		{
			this.ui1 = A_0.ReadUInt32();
			this.i1 = A_0.ReadInt32();
			this.i2 = A_0.ReadInt32();
			this.i3 = (int)A_0.BaseStream.Position;
			if( !BuildConfig.CARBON )
			{
				A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.i4 = A_0.ReadInt32();
			this.i5 = A_0.ReadInt32();
			this.i6 = A_0.ReadInt32();
			this.i7 = A_0.ReadInt32();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ui1 );
			A_0.Write( this.i1 );
			A_0.Write( this.i2 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( VLTConstants.MW_DEADBEEF );
			}
			A_0.Write( this.i4 );
			A_0.Write( this.i5 );
			A_0.Write( this.i6 );
			A_0.Write( this.i7 );
		}
	}
}
