using System;
using System.IO;

namespace VLTEdit
{
	public class UnknownAS : IBinReadWrite
	{
		private uint ui1;
		private VLTCommonValue b21;
		private int i1; //NFS:C --> Gone!
		private int i2;
		private int i3;
		private UnknownDI di1;

		public void print()
		{
			Console.WriteLine( "AS: ui1: " + ui1 );
			Console.WriteLine( "AS: b21: " + b21 );
			if( !BuildConfig.CARBON )
			{
				Console.WriteLine( "AS: i1: " + i1 );
			}
			Console.WriteLine( "AS: i2: " + i2 );
			Console.WriteLine( "AS: i3: " + i3 );
		}

		public uint d()
		{
			return this.ui1;
		}

		public void a( uint A_0 )
		{
			this.ui1 = A_0;
		}

		public VLTCommonValue b()
		{
			return this.b21;
		}

		public void a( VLTCommonValue A_0 )
		{
			this.b21 = A_0;
		}

		public int e()
		{
			return this.i2;
		}

		public void a( int A_0 )
		{
			this.i2 = A_0;
		}

		public int c()
		{
			return this.i3;
		}

		public void b( int A_0 )
		{
			this.i3 = A_0;
		}

		public UnknownDI a()
		{
			return this.di1;
		}

		public void a( UnknownDI A_0 )
		{
			this.di1 = A_0;
		}

		public void b( BinaryReader A_0 ) // TODO: NFS:C
		{
			// NFS:C  880012749 (BAD!) --> -1911476553 (BAD!!)
			// NFS:MW many many times: 72, 464, 496, 536, 576, 616, 656, 696, 736, ... 162488, 162544, 162600
			//Console.WriteLine( "AS: i3 = " + this.i3.ToString() ); // Problem must be before this;
			A_0.BaseStream.Seek( (long)this.i3, 0 ); // NFS:C EndOfStream Exception! FIXED?!
			this.di1 = UnknownDI.a( this.b() );
			this.di1.setLong( (long)this.i3 );
			this.di1.a( this );
			this.di1.read( A_0 );
		}

		public void a( BinaryWriter A_0 )
		{
			A_0.BaseStream.Seek( (long)this.i3, 0 );
			this.di1.write( A_0 );
		}

		public void read( BinaryReader A_0 )
		{
			this.ui1 = A_0.ReadUInt32();
			this.b21 = (VLTCommonValue)A_0.ReadUInt32();
			if( !BuildConfig.CARBON )
			{
				this.i1 = A_0.ReadInt32(); // Removed in NFS:C
			}
			else
			{
				this.i1 = 0; // Set to something at least though.
			}
			this.i2 = A_0.ReadInt32();
			this.i3 = A_0.ReadInt32();
		}

		public void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ui1 );
			A_0.Write( (uint)this.b21 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( this.i1 ); // Removed in NFS:C
			}
			A_0.Write( this.i2 );
			A_0.Write( this.i3 );
		}
	}
}
