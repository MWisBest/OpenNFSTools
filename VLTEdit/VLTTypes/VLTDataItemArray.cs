using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class VLTDataItemArray : VLTDataItem, IEnumerable
	{
		private ArrayList _items;
		private int _align;
		private uint _type;
		private Type _itemType;

		public static VLTDataItemArray Instantiate( VLTClass.ClassField field, Type type )
		{
			VLTDataItemArray array = new VLTDataItemArray();
			array._align = field.Alignment;
			array._type = field.TypeHash;
			array._itemType = type;
			array._items = new ArrayList();
			return array;
		}

		public override void Read( BinaryReader br )
		{
			this.MaxCount = br.ReadInt16();
			this.ValidCount = br.ReadInt16();
			this.DataSize = br.ReadInt16();
			br.ReadInt16();

			ConstructorInfo mi = this._itemType.GetConstructor( Type.EmptyTypes );

			for( int i = 0; i < this.MaxCount; i++ )
			{
				if( this._align > 0 )
				{
					if( br.BaseStream.Position % this._align != 0 )
					{
						br.BaseStream.Position += this._align - ( br.BaseStream.Position % this._align );
					}
				}

				VLTDataItem item = mi.Invoke( null ) as VLTDataItem;

				if( item is VLTTypes.UnknownType )
				{
					( item as VLTTypes.UnknownType ).SetLength( this.DataSize );
				}

				item.Offset = (uint)br.BaseStream.Position;
				item.InlineData = false;
				item.TypeHash = this._type;
				item.DataRow = this.DataRow;
				item.ArrayIndex = i;

				item.Read( br );

				this._items.Add( item );
			}
		}

		public VLTDataItem this[int index]
		{
			get
			{
				return this._items[index] as VLTDataItem;
			}
			set
			{
				this._items[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		public short MaxCount
		{
			get; private set;
		}

		public short ValidCount
		{
			get; private set;
		}

		public short DataSize
		{
			get; private set;
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.MaxCount );
			bw.Write( this.ValidCount );
			bw.Write( this.DataSize );
			bw.Write( (short)0 );
			for( int i = 0; i < this.MaxCount; i++ )
			{
				if( this._align > 0 )
				{
					if( bw.BaseStream.Position % this._align != 0 )
					{
						bw.BaseStream.Position += this._align - ( bw.BaseStream.Position % this._align );
					}
				}
				( this._items[i] as VLTDataItem ).Write( bw );
			}
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		#endregion
	}
}
