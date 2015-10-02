using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownW : UnknownDI
	{
		private int i1;
		private int i2;
		public int i3;
		public int position;
		public int[] ia1;

		public override void read( BinaryReader A_0 )
		{
			this.i1 = A_0.ReadInt32();
			this.i2 = A_0.ReadInt32();
			this.i3 = A_0.ReadInt32();

			// position is casted here because the main thing that references this needs it as an int anyway.
			this.position = (int)A_0.BaseStream.Position;

			if( !BuildConfig.CARBON )
			{
				A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.ia1 = new int[this.i3];
			for( int i = 0; i < this.i3; ++i )
			{
				this.ia1[i] = A_0.ReadInt32();
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.i1 );
			A_0.Write( this.i2 );
			A_0.Write( this.i3 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( VLTConstants.MW_DEADBEEF );
			}
			for( int i = 0; i < this.i3; ++i )
			{
				A_0.Write( this.ia1[i] );
			}
		}
	}
}
