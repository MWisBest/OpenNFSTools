using System.IO;
using System.Reflection;

namespace VLTEdit.Table
{
	[DefaultMember( "Item" )]
	public class RootEntry : BaseEntry
	{
		private int i1;
		private int i2;
		public int i3;
		public int position;
		public int[] ia1;

		public override void read( BinaryReader br )
		{
			// comments on sides referencing NFS:MW db.vlt
			this.i1 = br.ReadInt32(); // offset: 0x48
			this.i2 = br.ReadInt32(); // ...
			this.i3 = br.ReadInt32(); // ...

			// position is casted here because the main thing that references this needs it as an int anyway.
			this.position = (int)br.BaseStream.Position; // we're now at 0x54

			if( !BuildConfig.CARBON )
			{
				br.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
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
			if( !BuildConfig.CARBON )
			{
				bw.Write( VLTConstants.MW_DEADBEEF );
			}
			for( int i = 0; i < this.i3; ++i )
			{
				bw.Write( this.ia1[i] );
			}
		}
	}
}
