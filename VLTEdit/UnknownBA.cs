using System.IO;

namespace VLTEdit
{
	public class UnknownBA : UnknownC0
	{
		// MW: Unused
		/*public const int i1 = 0;
		public const int i2 = 1;*/
		private int i3;
		private uint[] uia1;
		private string[] sa1;

		public uint b( int A_0 )
		{
			return this.uia1[A_0];
		}

		public string a( int A_0 )
		{
			return this.sa1[A_0];
		}

		public override void read( BinaryReader A_0 )
		{
			this.i3 = A_0.ReadInt32();
			this.uia1 = new uint[this.i3];
			this.sa1 = new string[this.i3];
			int[] array = new int[this.i3];

			for( int i = 0; i < this.i3; i++ )
			{
				this.uia1[i] = A_0.ReadUInt32();
			}

			for( int j = 0; j < this.i3; j++ )
			{
				array[j] = A_0.ReadInt32();
			}

			long position = A_0.BaseStream.Position;

			for( int k = 0; k < this.i3; k++ )
			{
				A_0.BaseStream.Seek( position + array[k], SeekOrigin.Begin );
				this.sa1[k] = UnknownAP.a( A_0 );
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			int num = 0;
			A_0.Write( this.i3 );

			for( int i = 0; i < this.i3; i++ )
			{
				A_0.Write( this.uia1[i] );
			}

			for( int j = 0; j < this.i3; j++ )
			{
				A_0.Write( num );
				num += this.sa1[j].Length + 1;
			}

			for( int k = 0; k < this.i3; k++ )
			{
				UnknownAP.a( A_0, this.sa1[k] );
			}
		}
	}
}
