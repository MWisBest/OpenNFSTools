using System;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownC : UnknownDI
	{
		public class aclz : IBinReadWrite
		{
			private uint ui1;
			private int i1;
			private short sh1;
			private short sh2;

			public uint c()
			{
				return this.ui1;
			}

			public int e()
			{
				return this.i1;
			}

			public short b()
			{
				return this.sh1;
			}

			public short d()
			{
				return this.sh2;
			}

			public bool a()
			{
				return ( this.sh2 & 32 ) != 0;
			}

			public void read( BinaryReader A_0 )
			{
				this.ui1 = A_0.ReadUInt32();
				this.i1 = (int)A_0.BaseStream.Position;
				if( !BuildConfig.CARBON )
				{
					A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
				}
				this.sh1 = A_0.ReadInt16();
				this.sh2 = A_0.ReadInt16();
			}

			public void write( BinaryWriter A_0 )
			{
				A_0.Write( this.ui1 );
				if( !BuildConfig.CARBON )
				{
					A_0.Write( VLTConstants.MW_DEADBEEF );
				}
				A_0.Write( this.sh1 );
				A_0.Write( this.sh2 );
			}
		}

		private uint hash;
		private uint ui2;
		private uint ui3;
		private int i1; // NFS:C --> Removed?
		private int i2;
		private int i3;
		private int i4;
		private int i5;
		private uint[] uia1;
		private UnknownC.aclz[] caa1;

		public uint ui()
		{
			return this.hash;
		}

		public uint f()
		{
			return this.ui2;
		}

		public uint b()
		{
			return this.ui3;
		}
		public int d()
		{
			return this.i4;
		}

		public int e()
		{
			return this.i1;
		}

		public int a()
		{
			return this.i5;
		}

		public UnknownC.aclz a( int A_0 )
		{
			return this.caa1[A_0];
		}

		public override void read( BinaryReader A_0 )
		{
			this.hash = A_0.ReadUInt32(); //hash in NFS:MW at least...
			this.ui2 = A_0.ReadUInt32();
			this.ui3 = A_0.ReadUInt32();
			this.i1 = A_0.ReadInt32();
			this.i2 = A_0.ReadInt32(); // Always 0 in NFS:MW
			this.i3 = A_0.ReadInt32();
			if( BuildConfig.CARBON )
			{
				//this.i4 = A_0.ReadInt16();
				//A_0.ReadInt16();
				this.i4 = A_0.ReadInt32(); // Int16 in NFS:C? Fits into Int16 in NFS:MW... :/
			}
			else
			{
				this.i4 = A_0.ReadInt32(); // Int16 in NFS:C? Fits into Int16 in NFS:MW
			}
			this.i5 = (int)A_0.BaseStream.Position;
			if( !BuildConfig.CARBON )
			{
				A_0.ReadInt32(); // VLTConstants.MW_DEADBEEF
			}
			this.uia1 = new uint[this.i4]; // NFS:C Overflow!
			for( int i = 0; i < this.i4; i++ )
			{
				this.uia1[i] = A_0.ReadUInt32();
			}
			this.caa1 = new UnknownC.aclz[this.i1];
			for( int j = 0; j < this.i1; j++ )
			{
				this.caa1[j] = new UnknownC.aclz();
				this.caa1[j].read( A_0 );
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.hash );
			A_0.Write( this.ui2 );
			A_0.Write( this.ui3 );
			A_0.Write( this.i1 );
			A_0.Write( this.i2 );
			A_0.Write( this.i3 );
			A_0.Write( this.i4 );
			if( !BuildConfig.CARBON )
			{
				A_0.Write( VLTConstants.MW_DEADBEEF );
			}
			for( int i = 0; i < this.i4; i++ )
			{
				A_0.Write( this.uia1[i] );
			}
			for( int j = 0; j < this.i1; j++ )
			{
				this.caa1[j].write( A_0 );
			}
		}
	}
}
