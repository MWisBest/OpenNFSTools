using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

// TODO Collections.Generic IComparable IEnumerable etc
namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class VLTClass : IComparable<VLTClass>, IEnumerable // OBF: dq.cs
	{
		[DefaultMember( "Item" )]
		public class bie : IEnumerable<UnknownDR> // OBF: b
		{
			private VLTClass vltClass;
			private List<UnknownDR> l1;

			public bie( VLTClass vltClass )
			{
				this.vltClass = vltClass;
				this.l1 = new List<UnknownDR>();
			}

			public UnknownDR a( uint A_0 )
			{
				IEnumerator<UnknownDR> enumerator = this.l1.GetEnumerator();
				try
				{
					while( enumerator.MoveNext() )
					{
						UnknownDR dr = enumerator.Current;
						if( dr.c1.hash == A_0 )
						{
							return dr;
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if( disposable != null )
					{
						disposable.Dispose();
					}
				}
				return null;
			}

			public void a( UnknownC A_0, UnknownB0 A_1 )
			{
				BinaryReader binaryReader = new BinaryReader( A_1.ms1 );
				BinaryReader binaryReader2 = new BinaryReader( A_1.ms2 );
				UnknownDR dr = new UnknownDR( this.vltClass.c61.i2 );
				UnknownA8 a = A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8;
				int num = a.genht1[A_0.i5].i2;
				dr.b01 = A_1;
				dr.dq1 = this.vltClass;
				dr.c1 = A_0;
				for( int i = 0; i < this.vltClass.c61.i2; ++i )
				{
					VLTClass.aclz1 a2 = this.vltClass.dqaa1[i];
					BinaryReader binaryReader3;
					if( !a2.c() )
					{
						binaryReader3 = binaryReader;
						binaryReader3.BaseStream.Seek( num + a2.us1, SeekOrigin.Begin );
					}
					else
					{
						binaryReader3 = null;
						for( int j = 0; j < A_0.i1; ++j )
						{
							if( A_0.caa1[j].ui1 == a2.hash )
							{
								if( A_0.caa1[j].a() )
								{
									binaryReader3 = binaryReader2;
									binaryReader3.BaseStream.Seek( A_0.caa1[j].i1, SeekOrigin.Begin );
								}
								else
								{
									UnknownB8 b = a.genht1[A_0.caa1[j].i1];
									binaryReader3 = binaryReader;
									binaryReader3.BaseStream.Seek( b.i2, SeekOrigin.Begin );
								}
							}
						}
						if( binaryReader3 == null )
						{
							continue;
						}
					}
					Type type = TypeMap.instance.getTypeForKey( a2.ui2 );
					if( type == null )
					{
						type = typeof( EARawType );
					}
					EABaseType bb;
					if( a2.isArray() )
					{
						bb = new EAArray( a2, type );
					}
					else
					{
						bb = EABaseType.a( type );
						if( bb is EARawType )
						{
							( bb as EARawType ).len = a2.len;
						}
					}
					bb.b( (uint)binaryReader3.BaseStream.Position );
					bb.a( binaryReader3 == binaryReader2 );
					bb.setUITwo( a2.ui2 );
					bb.c( a2.hash );
					bb.a( dr );
					bb.read( binaryReader3 );
					dr.a( i, bb );
				}
				this.l1.Add( dr );
			}

			public IEnumerator<UnknownDR> GetEnumerator()
			{
				return this.l1.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}

		public class aclz1 : IBinReadWrite // OBF: a
		{
			public uint hash;
			public uint ui2;
			public ushort us1;
			public ushort len;
			public short count;
			public byte b1;
			public byte b2;

			public bool isArray()
			{
				return ( this.b1 & 1 ) != 0;
			}

			public bool c()
			{
				return ( this.b1 & 2 ) == 0;
			}

			public int a()
			{
				return 1 << this.b2;
			}

			public void read( BinaryReader A_0 )
			{
				this.hash = A_0.ReadUInt32();
				this.ui2 = A_0.ReadUInt32();
				this.us1 = A_0.ReadUInt16();
				this.len = A_0.ReadUInt16();
				this.count = A_0.ReadInt16();
				this.b1 = A_0.ReadByte();
				this.b2 = A_0.ReadByte();
				//System.Console.WriteLine( this.ui1.ToString() + ":" + this.ui2.ToString() + ":" + this.us1.ToString() + ":" + this.us2.ToString() + ":" + this.sh1.ToString() + ":" + this.b1.ToString() + ":" + this.b2.ToString() );
			}

			public void write( BinaryWriter A_0 )
			{
				A_0.Write( this.hash );
				A_0.Write( this.ui2 );
				A_0.Write( this.us1 );
				A_0.Write( this.len );
				A_0.Write( this.count );
				A_0.Write( this.b1 );
				A_0.Write( this.b2 );
			}
		}

		public UnknownB0 b01;
		public uint ui1;
		public UnknownC6 c61;
		public VLTClass.aclz1[] dqaa1;
		public VLTClass.bie dqb1;

		public void a( UnknownC6 A_0, UnknownB0 A_1 )
		{
			this.b01 = A_1;
			this.c61 = A_0;
			this.ui1 = A_0.ui1;
			UnknownA8 a81 = ( A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8 );
			int num = a81.genht1[A_0.i3].i2;
			A_1.ms1.Seek( num, SeekOrigin.Begin );
			BinaryReader a_ = new BinaryReader( A_1.ms1 );
			this.dqaa1 = new VLTClass.aclz1[this.c61.i2];
			for( int i = 0; i < this.c61.i2; ++i )
			{
				VLTClass.aclz1 a = new VLTClass.aclz1();
				a.read( a_ );
				HashTracker.getValueForHash( a.hash );
				this.dqaa1[i] = a;
			}
			this.dqb1 = new VLTClass.bie( this );
		}

		public int a( uint A_0 )
		{
			// TODO: Threading?
			for( int i = 0; i < this.dqaa1.Length; ++i )
			{
				if( this.dqaa1[i].hash == A_0 )
				{
					return i;
				}
			}
			return -1;
		}

		public int CompareTo( VLTClass A_0 )
		{
			return this.ui1.CompareTo( A_0.ui1 );
		}

		public IEnumerator GetEnumerator()
		{
			return this.dqaa1.GetEnumerator();
		}
	}
}
