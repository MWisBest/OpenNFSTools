using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NFSTools.VLTEdit.Types
{
	public class VLTArrayType : VLTBaseType, IEnumerable<VLTBaseType>, ITrimmable // OBF: m.cs
	{
		private short curEntries;
		private short maxEntries;
		private short length;
		public List<VLTBaseType> genlist;
		private int earrayi1;
		private uint earrayui1;
		private Type typ1;

		public VLTArrayType( VLTClass.aclz1 A_0, Type A_1 )
		{
			this.earrayi1 = A_0.a();
			this.earrayui1 = A_0.ui2;
			this.typ1 = A_1;
			this.genlist = new List<VLTBaseType>();
		}

		public override void read( BinaryReader br )
		{
			this.curEntries = br.ReadInt16();
			this.maxEntries = br.ReadInt16();
			this.length = br.ReadInt16();
			br.ReadInt16();
			ConstructorInfo constructor = this.typ1.GetConstructor( Type.EmptyTypes );
			for( int i = 0; i < this.curEntries; ++i )
			{
				if( this.earrayi1 > 0 && br.BaseStream.Position % this.earrayi1 != 0L )
				{
					Stream expr_69 = br.BaseStream;
					expr_69.Position = expr_69.Position + ( this.earrayi1 - br.BaseStream.Position % this.earrayi1 );
				}
				VLTBaseType bb = constructor.Invoke( null ) as VLTBaseType;
				if( bb is VLTRawType )
				{
					( bb as VLTRawType ).len = this.length;
				}
				bb.ui1 = (uint)br.BaseStream.Position;
				bb.isVltOffset = false;
				bb.ui2 = this.earrayui1; // TODO: What are we REALLY setting this to? this.ui1 is our hash... or is it not supposed to be?
				bb.dr1 = base.dr1;
				bb.i1 = i;
				bb.read( br );
				this.genlist.Add( bb );
			}

			this.Trim();
		}

		public short getCurrentEntryCount()
		{
			return this.curEntries;
		}

		public short getMaxEntryCount()
		{
			return this.maxEntries;
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.curEntries );
			bw.Write( this.maxEntries );
			bw.Write( this.length );
			bw.Write( (short)0 );
			for( int i = 0; i < this.curEntries; ++i )
			{
				if( this.earrayi1 > 0 && bw.BaseStream.Position % this.earrayi1 != 0L )
				{
					Stream expr_55 = bw.BaseStream;
					expr_55.Position = expr_55.Position + ( this.earrayi1 - bw.BaseStream.Position % this.earrayi1 );
				}
				this.genlist[i].write( bw );
			}
		}

		public IEnumerator<VLTBaseType> GetEnumerator()
		{
			return this.genlist.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Trim()
		{
			if( BuildConfig.TRIMMING_ENABLED )
			{
				this.genlist.TrimExcess();
			}
		}
	}
}
