using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class EAArray : EABaseType, IEnumerable // OBF: m.cs
	{
		private short curEntries;
		private short maxEntries;
		private short length;
		private ArrayList al1;
		private int i1;
		private uint ui1;
		private Type typ1;

		public EAArray( VLTClass.aclz1 A_0, Type A_1 )
		{
			this.i1 = A_0.a();
			this.ui1 = A_0.ui2;
			this.typ1 = A_1;
			this.al1 = new ArrayList();
		}

		public override void read( BinaryReader A_0 )
		{
			this.curEntries = A_0.ReadInt16();
			this.maxEntries = A_0.ReadInt16();
			this.length = A_0.ReadInt16();
			A_0.ReadInt16();
			ConstructorInfo constructor = this.typ1.GetConstructor( Type.EmptyTypes );
			for( int i = 0; i < (int)this.curEntries; i++ )
			{
				if( this.i1 > 0 && A_0.BaseStream.Position % (long)this.i1 != 0L )
				{
					Stream expr_69 = A_0.BaseStream;
					expr_69.Position = expr_69.Position + ( (long)this.i1 - A_0.BaseStream.Position % (long)this.i1 );
				}
				EABaseType bb = constructor.Invoke( null ) as EABaseType;
				if( bb is EARawType )
				{
					( bb as EARawType ).len = (int)this.length;
				}
				bb.b( (uint)A_0.BaseStream.Position );
				bb.a( false );
				bb.setUITwo( this.ui1 ); // TODO: What are we REALLY setting this to? this.ui1 is our hash... or is it not supposed to be?
				bb.a( base.m() );
				bb.b( i );
				bb.read( A_0 );
				this.al1.Add( bb );
			}
		}

		public EABaseType a( int A_0 )
		{
			return this.al1[A_0] as EABaseType;
		}

		public void a( int A_0, EABaseType A_1 )
		{
			this.al1[A_0] = A_1;
		}

		public int c()
		{
			return this.al1.Count;
		}

		public short getCurrentEntryCount()
		{
			return this.curEntries;
		}

		public short getMaxEntryCount()
		{
			return this.maxEntries;
		}

		public short b()
		{
			return this.length;
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.curEntries );
			A_0.Write( this.maxEntries );
			A_0.Write( this.length );
			A_0.Write( (short)0 );
			for( int i = 0; i < (int)this.curEntries; i++ )
			{
				if( this.i1 > 0 && A_0.BaseStream.Position % (long)this.i1 != 0L )
				{
					Stream expr_55 = A_0.BaseStream;
					expr_55.Position = expr_55.Position + ( (long)this.i1 - A_0.BaseStream.Position % (long)this.i1 );
				}
				( this.al1[i] as EABaseType ).write( A_0 );
			}
		}

		public IEnumerator GetEnumerator()
		{
			return this.al1.GetEnumerator();
		}
	}
}
