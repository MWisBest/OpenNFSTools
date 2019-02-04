using System.IO;

namespace NFSTools.VLTEditLegacy
{
	public class UnknownBA : UnknownC0
	{
		// MW: Unused
		/*public const int i1 = 0;
		public const int i2 = 1;*/
		private int i3;
		public uint[] uia1;
		public string[] sa1;

		public override void read( BinaryReader br )
		{
			this.i3 = br.ReadInt32();
			this.uia1 = new uint[this.i3];
			this.sa1 = new string[this.i3];
			int[] array = new int[this.i3];

			for( int i = 0; i < this.i3; ++i )
			{
				this.uia1[i] = br.ReadUInt32();
			}

			for( int j = 0; j < this.i3; ++j )
			{
				array[j] = br.ReadInt32();
			}

			long position = br.BaseStream.Position;

			for( int k = 0; k < this.i3; ++k )
			{
				br.BaseStream.Seek( position + array[k], SeekOrigin.Begin );
				this.sa1[k] = UnknownAP.a( br );
			}
		}

		public override void write( BinaryWriter bw )
		{
			int num = 0;
			bw.Write( this.i3 );

			for( int i = 0; i < this.i3; ++i )
			{
				bw.Write( this.uia1[i] );
			}

			for( int j = 0; j < this.i3; ++j )
			{
				bw.Write( num );
				num += this.sa1[j].Length + 1;
			}

			for( int k = 0; k < this.i3; ++k )
			{
				UnknownAP.a( bw, this.sa1[k] );
			}
		}
	}
}
