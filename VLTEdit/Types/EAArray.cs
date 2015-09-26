using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class EAArray : EABaseType, IEnumerable<EABaseType> // OBF: m.cs
	{
		private short curEntries;
		private short maxEntries;
		private short length;
		public List<EABaseType> genlist;
		private int earrayi1;
		private uint earrayui1;
		private Type typ1;

		public EAArray( VLTClass.aclz1 A_0, Type A_1 )
		{
			this.earrayi1 = A_0.a();
			this.earrayui1 = A_0.ui2;
			this.typ1 = A_1;
			this.genlist = new List<EABaseType>();
		}

		public override void read( BinaryReader A_0 )
		{
			this.curEntries = A_0.ReadInt16();
			this.maxEntries = A_0.ReadInt16();
			this.length = A_0.ReadInt16();
			A_0.ReadInt16();
			ConstructorInfo constructor = this.typ1.GetConstructor( Type.EmptyTypes );
			for( int i = 0; i < this.curEntries; ++i )
			{
				if( this.earrayi1 > 0 && A_0.BaseStream.Position % this.earrayi1 != 0L )
				{
					Stream expr_69 = A_0.BaseStream;
					expr_69.Position = expr_69.Position + ( this.earrayi1 - A_0.BaseStream.Position % this.earrayi1 );
				}
				EABaseType bb = constructor.Invoke( null ) as EABaseType;
				if( bb is EARawType )
				{
					( bb as EARawType ).len = this.length;
				}
				bb.b( (uint)A_0.BaseStream.Position );
				bb.a( false );
				bb.setUITwo( this.earrayui1 ); // TODO: What are we REALLY setting this to? this.ui1 is our hash... or is it not supposed to be?
				bb.a( base.m() );
				bb.b( i );
				bb.read( A_0 );
				this.genlist.Add( bb );
			}
		}

		public short getCurrentEntryCount()
		{
			return this.curEntries;
		}

		public short getMaxEntryCount()
		{
			return this.maxEntries;
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.curEntries );
			A_0.Write( this.maxEntries );
			A_0.Write( this.length );
			A_0.Write( (short)0 );
			for( int i = 0; i < this.curEntries; ++i )
			{
				if( this.earrayi1 > 0 && A_0.BaseStream.Position % this.earrayi1 != 0L )
				{
					Stream expr_55 = A_0.BaseStream;
					expr_55.Position = expr_55.Position + ( this.earrayi1 - A_0.BaseStream.Position % this.earrayi1 );
				}
				this.genlist[i].write( A_0 );
			}
		}

		public IEnumerator<EABaseType> GetEnumerator()
		{
			return this.genlist.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
