using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NFSTools.LibNFS.Common;
using NFSTools.VLTEdit.Table;
using NFSTools.VLTEdit.Types;

// TODO Collections.Generic IComparable IEnumerable etc
namespace NFSTools.VLTEdit
{
	public class VLTClass : IComparable<VLTClass>, IEnumerable // OBF: dq.cs
	{
		public class bie : IEnumerable<UnknownDR>, ITrimmable // OBF: b
		{
			private VLTClass vltClass;
			private List<UnknownDR> drList;
			public static uint numFails = 0;
			public static uint b8Fails = 0;

			public bie( VLTClass vltClass )
			{
				this.vltClass = vltClass;
				this.drList = new List<UnknownDR>();
			}

			public UnknownDR a( uint A_0 )
			{
				foreach( UnknownDR dr in this.drList )
				{
					if( dr.c1.hash == A_0 )
					{
						return dr;
					}
				}
				return null;
			}

			// This is where our problems with Carbon and up appear to stem from.
			public void a( RowRecord A_0, UnknownB0 A_1 )
			{
				BinaryReader binaryReader = new BinaryReader( A_1.ms1 );
				BinaryReader binaryReader2 = new BinaryReader( A_1.ms2 );
				UnknownDR dr = new UnknownDR( this.vltClass.classRecord.i2 );
				UnknownA8 a = A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8;

				int num;
				if( a.genht1.ContainsKey( A_0.position ) )
				{
					num = a.genht1[A_0.position].i2;
				}
				else
				{
					// NOTE: THIS IS A BUG, IT SHOULD NOT HAPPEN.
					//       The ONLY reason I'm checking ContainsKey instead of using exception handling,
					//       is because exception handling causes huge slowdowns in Visual Studio debugging.
					if( BuildConfig.DEBUG )
					{
						++numFails;
						Console.WriteLine( "VLTClass.a(): num fail (num" + numFails + ",b" + b8Fails + ")" );
					}
					return;
				}

				dr.b01 = A_1;
				dr.dq1 = this.vltClass;
				dr.c1 = A_0;
				for( int i = 0; i < this.vltClass.classRecord.i2; ++i )
				{
					VLTClass.aclz1 a2 = this.vltClass.fields[i];
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
							if( A_0.caa1[j].hash == a2.hash )
							{
								if( A_0.caa1[j].a() )
								{
									binaryReader3 = binaryReader2;
									binaryReader3.BaseStream.Seek( A_0.caa1[j].position, SeekOrigin.Begin );
								}
								else
								{
									UnknownB8 b = null;
									if( a.genht1.ContainsKey( A_0.caa1[j].position ) )
									{
										b = a.genht1[A_0.caa1[j].position];
									}
									else
									{
										// NOTE: THIS IS A BUG, IT SHOULD NOT HAPPEN.
										//       The ONLY reason I'm checking ContainsKey instead of using exception handling,
										//       is because exception handling causes huge slowdowns in Visual Studio debugging.
										if( BuildConfig.DEBUG )
										{
											++b8Fails;
											Console.WriteLine( "VLTClass.a(): b   fail (num" + numFails + ",b" + b8Fails + ")" );
										}
										//continue;
									}
									if( b != null )
									{
										binaryReader3 = binaryReader;
										binaryReader3.BaseStream.Seek( b.i2, SeekOrigin.Begin );
									}
									else
									{
										// lolwtf, doing this can actually help Carbon+ rows that don't load! Unreal!
										binaryReader3 = binaryReader2;
										binaryReader3.BaseStream.Seek( A_0.caa1[j].position, SeekOrigin.Begin );
									}
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
						type = typeof( VLTRawType );
					}
					VLTBaseType bb;
					if( a2.isArray() )
					{
						bb = new VLTArrayType( a2, type );
					}
					else
					{
						bb = VLTBaseType.a( type );
						bb.size = a2.len;
						if( bb is VLTRawType )
						{
							( bb as VLTRawType ).len = a2.len;
						}
					}
					bb.ui1 = (uint)binaryReader3.BaseStream.Position;
					bb.isVltOffset = ( binaryReader3 == binaryReader2 );
					bb.ui2 = a2.ui2;
					bb.ui3 = a2.hash;
					bb.dr1 = dr;
					bb.read( binaryReader3 );
					dr.a( i, bb );
				}
				this.drList.Add( dr );
			}

			public void Trim()
			{
				if( BuildConfig.TRIMMING_ENABLED )
				{
					this.drList.TrimExcess();
				}
			}

			public IEnumerator<UnknownDR> GetEnumerator()
			{
				return this.drList.GetEnumerator();
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

			public void read( BinaryReader br )
			{
				this.hash = br.ReadUInt32();
				this.ui2 = br.ReadUInt32();
				this.us1 = br.ReadUInt16();
				this.len = br.ReadUInt16();
				this.count = br.ReadInt16();
				this.b1 = br.ReadByte();
				this.b2 = br.ReadByte();
			}

			public void write( BinaryWriter bw )
			{
				bw.Write( this.hash );
				bw.Write( this.ui2 );
				bw.Write( this.us1 );
				bw.Write( this.len );
				bw.Write( this.count );
				bw.Write( this.b1 );
				bw.Write( this.b2 );
			}
		}

		public UnknownB0 b01;
		public uint classHash;
		public ClassRecord classRecord;
		public VLTClass.aclz1[] fields;
		public VLTClass.bie dqb1;

		public void a( ClassRecord A_0, UnknownB0 A_1 )
		{
			this.b01 = A_1;
			this.classRecord = A_0;
			this.classHash = A_0.hash;
			UnknownA8 a81 = ( A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8 );
			int num = a81.genht1[A_0.position].i2;
			A_1.ms1.Seek( num, SeekOrigin.Begin );
			BinaryReader br = new BinaryReader( A_1.ms1 );
			this.fields = new VLTClass.aclz1[this.classRecord.i2];
			for( int i = 0; i < this.classRecord.i2; ++i )
			{
				VLTClass.aclz1 a = new VLTClass.aclz1();
				a.read( br );
				HashTracker.getValueForHash( a.hash );
				this.fields[i] = a;
			}
			this.dqb1 = new VLTClass.bie( this );
		}

		public int a( uint A_0 )
		{
			// TODO: Threading?
			for( int i = 0; i < this.fields.Length; ++i )
			{
				if( this.fields[i].hash == A_0 )
				{
					return i;
				}
			}
			return -1;
		}

		public int CompareTo( VLTClass A_0 )
		{
			return this.classHash.CompareTo( A_0.classHash );
		}

		public IEnumerator GetEnumerator()
		{
			return this.fields.GetEnumerator();
		}
	}
}
