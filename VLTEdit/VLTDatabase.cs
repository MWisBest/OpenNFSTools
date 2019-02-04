using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace NFSTools.VLTEdit
{

	public class VLTTypeResolver
	{
		private Hashtable _typeTable;
		public static VLTTypeResolver Resolver = new VLTTypeResolver();
		public VLTTypeResolver()
		{
			this._typeTable = new Hashtable();
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Double" ), typeof( VLTDataItems.Double ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Float" ), typeof( VLTDataItems.Float ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt64" ), typeof( VLTDataItems.UInt64 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt32" ), typeof( VLTDataItems.UInt32 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt16" ), typeof( VLTDataItems.UInt16 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt8" ), typeof( VLTDataItems.UInt8 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int64" ), typeof( VLTDataItems.Int64 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int32" ), typeof( VLTDataItems.Int32 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int16" ), typeof( VLTDataItems.Int16 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int8" ), typeof( VLTDataItems.Int8 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Bool" ), typeof( VLTDataItems.Bool ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Text" ), typeof( VLTDataItems.Text ) );

			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Matrix" ), typeof( VLTDataItems.Matrix ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector4" ), typeof( VLTDataItems.Vector4 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector3" ), typeof( VLTDataItems.Vector3 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector2" ), typeof( VLTDataItems.Vector2 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::StringKey" ), typeof( VLTDataItems.StringKey ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::RefSpec" ), typeof( VLTDataItems.RefSpec ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Blob" ), typeof( VLTDataItems.Blob ) );

			this._typeTable.Add( VLTHasher.Hash( "UpgradeSpecs" ), typeof( VLTDataItems.UpgradeSpecs ) );
			this._typeTable.Add( VLTHasher.Hash( "JunkmanMod" ), typeof( VLTDataItems.JunkmanMod ) );
			this._typeTable.Add( VLTHasher.Hash( "AxlePair" ), typeof( VLTDataItems.AxlePair ) );
			this._typeTable.Add( VLTHasher.Hash( "CarBodyMotion" ), typeof( VLTDataItems.CarBodyMotion ) );
			this._typeTable.Add( VLTHasher.Hash( "GCollectionKey" ), typeof( VLTDataItems.GCollectionKey ) );


		}
		public Type Resolve( uint hash )
		{
			if( this._typeTable.ContainsKey( hash ) )
			{
				return this._typeTable[hash] as Type;
			}

			return null;
		}
	}

	public class DataValueAttribute : Attribute
	{
		private string _name;
		private bool _hex;

		public DataValueAttribute( string name )
		{
			this._name = name;
			this._hex = false;
		}

		public bool Hex
		{
			get
			{
				return this._hex;
			}
			set
			{
				this._hex = value;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
		}
	}

	public class VLTHasher
	{

		public static ulong Hash64( string k )
		{
			return Hash64( k, 0x11223344ABCDEF00 );
		}

		public static ulong Hash64( string k, ulong init )
		{
			int koffs = 0;
			int len = k.Length;
			ulong a = 0x9e3779b97f4a7c13;
			ulong b = a;
			ulong c = init;

			byte[] charArr = Encoding.ASCII.GetBytes( k );
			while( len >= 24 )
			{
				a += BitConverter.ToUInt64( charArr, koffs );
				b += BitConverter.ToUInt64( charArr, koffs + 8 );
				c += BitConverter.ToUInt64( charArr, koffs + 16 );

				a -= b;
				a -= c;
				a ^= ( c >> 43 );
				b -= c;
				b -= a;
				b ^= ( a << 9 );
				c -= a;
				c -= b;
				c ^= ( b >> 8 );
				a -= b;
				a -= c;
				a ^= ( c >> 38 );
				b -= c;
				b -= a;
				b ^= ( a << 23 );
				c -= a;
				c -= b;
				c ^= ( b >> 5 );
				a -= b;
				a -= c;
				a ^= ( c >> 35 );
				b -= c;
				b -= a;
				b ^= ( a << 49 );
				c -= a;
				c -= b;
				c ^= ( b >> 11 );
				a -= b;
				a -= c;
				a ^= ( c >> 12 );
				b -= c;
				b -= a;
				b ^= ( a << 18 );
				c -= a;
				c -= b;
				c ^= ( b >> 22 );

				len -= 24;
				koffs += 24;
			}

			c += (ulong)k.Length;

			switch( len )
			{
				case 23:
					c += ( (ulong)k[22] << 56 );
					goto case 22;
				case 22:
					c += ( (ulong)k[21] << 48 );
					goto case 21;
				case 21:
					c += ( (ulong)k[20] << 40 );
					goto case 20;
				case 20:
					c += ( (ulong)k[19] << 32 );
					goto case 19;
				case 19:
					c += ( (ulong)k[18] << 24 );
					goto case 18;
				case 18:
					c += ( (ulong)k[17] << 16 );
					goto case 17;
				case 17:
					c += ( (ulong)k[16] << 8 );
					goto case 16;
				/* the first byte of c is reserved for the length */
				case 16:
					b += ( (ulong)k[15] << 56 );
					goto case 15;
				case 15:
					b += ( (ulong)k[14] << 48 );
					goto case 14;
				case 14:
					b += ( (ulong)k[13] << 40 );
					goto case 13;
				case 13:
					b += ( (ulong)k[12] << 32 );
					goto case 12;
				case 12:
					b += ( (ulong)k[11] << 24 );
					goto case 11;
				case 11:
					b += ( (ulong)k[10] << 16 );
					goto case 10;
				case 10:
					b += ( (ulong)k[9] << 8 );
					goto case 9;
				case 9:
					b += ( (ulong)k[8] );
					goto case 8;
				case 8:
					a += ( (ulong)k[7] << 56 );
					goto case 7;
				case 7:
					a += ( (ulong)k[6] << 48 );
					goto case 6;
				case 6:
					a += ( (ulong)k[5] << 40 );
					goto case 5;
				case 5:
					a += ( (ulong)k[4] << 32 );
					goto case 4;
				case 4:
					a += ( (ulong)k[3] << 24 );
					goto case 3;
				case 3:
					a += ( (ulong)k[2] << 16 );
					goto case 2;
				case 2:
					a += ( (ulong)k[1] << 8 );
					goto case 1;
				case 1:
					a += ( (ulong)k[0] );
					break;
			}

			a -= b;
			a -= c;
			a ^= ( c >> 43 );
			b -= c;
			b -= a;
			b ^= ( a << 9 );
			c -= a;
			c -= b;
			c ^= ( b >> 8 );
			a -= b;
			a -= c;
			a ^= ( c >> 38 );
			b -= c;
			b -= a;
			b ^= ( a << 23 );
			c -= a;
			c -= b;
			c ^= ( b >> 5 );
			a -= b;
			a -= c;
			a ^= ( c >> 35 );
			b -= c;
			b -= a;
			b ^= ( a << 49 );
			c -= a;
			c -= b;
			c ^= ( b >> 11 );
			a -= b;
			a -= c;
			a ^= ( c >> 12 );
			b -= c;
			b -= a;
			b ^= ( a << 18 );
			c -= a;
			c -= b;
			c ^= ( b >> 22 );

			return c;
		}

		public static uint Hash( string k )
		{
			return Hash( k, 0xABCDEF00 );
		}

		public static uint Hash( string k, uint init )
		{
			int koffs = 0;
			int len = k.Length;
			uint a = 0x9e3779b9;
			uint b = a;
			uint c = init;

			while( len >= 12 )
			{
				a += (uint)k[0 + koffs] + ( (uint)k[1 + koffs] << 8 ) + ( (uint)k[2 + koffs] << 16 ) + ( (uint)k[3 + koffs] << 24 );
				b += (uint)k[4 + koffs] + ( (uint)k[5 + koffs] << 8 ) + ( (uint)k[6 + koffs] << 16 ) + ( (uint)k[7 + koffs] << 24 );
				c += (uint)k[8 + koffs] + ( (uint)k[9 + koffs] << 8 ) + ( (uint)k[10 + koffs] << 16 ) + ( (uint)k[11 + koffs] << 24 );

				a -= b;
				a -= c;
				a ^= ( c >> 13 );
				b -= c;
				b -= a;
				b ^= ( a << 8 );
				c -= a;
				c -= b;
				c ^= ( b >> 13 );
				a -= b;
				a -= c;
				a ^= ( c >> 12 );
				b -= c;
				b -= a;
				b ^= ( a << 16 );
				c -= a;
				c -= b;
				c ^= ( b >> 5 );
				a -= b;
				a -= c;
				a ^= ( c >> 3 );
				b -= c;
				b -= a;
				b ^= ( a << 10 );
				c -= a;
				c -= b;
				c ^= ( b >> 15 );

				koffs += 12;
				len -= 12;
			}

			c += (uint)k.Length;

			switch( len )
			{
				case 11:
					c += (uint)k[10 + koffs] << 24;
					goto case 10;
				case 10:
					c += (uint)k[9 + koffs] << 16;
					goto case 9;
				case 9:
					c += (uint)k[8 + koffs] << 8;
					goto case 8;
				case 8:
					b += (uint)k[7 + koffs] << 24;
					goto case 7;
				case 7:
					b += (uint)k[6 + koffs] << 16;
					goto case 6;
				case 6:
					b += (uint)k[5 + koffs] << 8;
					goto case 5;
				case 5:
					b += (uint)k[4 + koffs];
					goto case 4;
				case 4:
					a += (uint)k[3 + koffs] << 24;
					goto case 3;
				case 3:
					a += (uint)k[2 + koffs] << 16;
					goto case 2;
				case 2:
					a += (uint)k[1 + koffs] << 8;
					goto case 1;
				case 1:
					a += (uint)k[0 + koffs];
					break;
			}

			a -= b;
			a -= c;
			a ^= ( c >> 13 );
			b -= c;
			b -= a;
			b ^= ( a << 8 );
			c -= a;
			c -= b;
			c ^= ( b >> 13 );
			a -= b;
			a -= c;
			a ^= ( c >> 12 );
			b -= c;
			b -= a;
			b ^= ( a << 16 );
			c -= a;
			c -= b;
			c ^= ( b >> 5 );
			a -= b;
			a -= c;
			a ^= ( c >> 3 );
			b -= c;
			b -= a;
			b ^= ( a << 10 );
			c -= a;
			c -= b;
			c ^= ( b >> 15 );

			return c;
		}
	}


	public abstract class VLTDataItem : IFileAccess
	{
		private uint _offset;
		private bool _inline;
		private uint _type;
		private uint _name;
		private int _arrayIndex;
		private VLTDataRow _row;

		public VLTDataRow DataRow
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		public int ArrayIndex
		{
			get
			{
				return this._arrayIndex;
			}
			set
			{
				this._arrayIndex = value;
			}
		}

		public uint TypeHash
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public uint NameHash
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public uint Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		public bool InlineData
		{
			get
			{
				return this._inline;
			}
			set
			{
				this._inline = value;
			}
		}

		public static VLTDataItem Instantiate( Type type )
		{
			ConstructorInfo mi = type.GetConstructor( Type.EmptyTypes );
			return mi.Invoke( null ) as VLTDataItem;
		}

		public abstract void Read( BinaryReader br );
		public abstract void Write( BinaryWriter bw );

		public virtual void LoadExtra()
		{

		}

		public override string ToString()
		{
			return "Cannot dump this type.";
		}

	}

	public class VLTDataItemArray : VLTDataItem, IEnumerable
	{
		private short _maxCount, _validCount, _dataSize;
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
			this._maxCount = br.ReadInt16();
			this._validCount = br.ReadInt16();
			this._dataSize = br.ReadInt16();
			br.ReadInt16();

			ConstructorInfo mi = this._itemType.GetConstructor( Type.EmptyTypes );

			for( int i = 0; i < this._maxCount; i++ )
			{
				if( this._align > 0 )
				{
					if( br.BaseStream.Position % this._align != 0 )
					{
						br.BaseStream.Position += this._align - ( br.BaseStream.Position % this._align );
					}
				}

				VLTDataItem item = mi.Invoke( null ) as VLTDataItem;

				if( item is VLTDataItems.Unknown )
				{
					( item as VLTDataItems.Unknown ).SetLength( this._dataSize );
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
			get
			{
				return this._maxCount;
			}
		}

		public short ValidCount
		{
			get
			{
				return this._validCount;
			}
		}

		public short DataSize
		{
			get
			{
				return this._dataSize;
			}
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this._maxCount );
			bw.Write( this._validCount );
			bw.Write( this._dataSize );
			bw.Write( (short)0 );
			for( int i = 0; i < this._maxCount; i++ )
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

	public class VLTDataRow : IEnumerable
	{
		private VLTFile _vltFile;
		private VLTDataCollectionLoad _collLoad;
		private VLTDataItem[] _dataItem;
		private bool[] _dataLoaded;
		private VLTClass _vltClass;
		public VLTClass VLTClass
		{
			get
			{
				return this._vltClass;
			}
			set
			{
				this._vltClass = value;
			}
		}
		public VLTFile VLTFile
		{
			get
			{
				return this._vltFile;
			}
			set
			{
				this._vltFile = value;
			}
		}
		public VLTDataCollectionLoad CollectionLoad
		{
			get
			{
				return this._collLoad;
			}
			set
			{
				this._collLoad = value;
			}
		}
		public VLTDataRow( int count )
		{
			this._dataItem = new VLTDataItem[count];
			this._dataLoaded = new bool[count];
		}
		public VLTDataItem this[int index]
		{
			get
			{
				return this._dataItem[index];
			}
			set
			{
				this._dataLoaded[index] = true;
				this._dataItem[index] = value;
			}
		}

		public bool IsDataLoaded( int index )
		{
			return this._dataLoaded[index];
		}

		public IEnumerator GetEnumerator()
		{
			return this._dataItem.GetEnumerator();
		}
	}

	public class VLTClass : IComparable, IEnumerable
	{
		public class ClassData : IEnumerable
		{
			private VLTClass _vltClass;
			private ArrayList _collection;
			public ClassData( VLTClass vltClass )
			{
				this._vltClass = vltClass;
				this._collection = new ArrayList( vltClass.ClassLoad.CollectionCount );
			}
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}
			public VLTDataRow this[uint hash]
			{
				get
				{
					foreach( VLTDataRow row in this._collection )
					{
						if( row.CollectionLoad.NameHash == hash )
						{
							return row;
						}
					}

					return null;
				}
			}
			public VLTDataRow this[int index]
			{
				get
				{
					return this._collection[index] as VLTDataRow;
				}
			}

			public void Add( VLTDataCollectionLoad collLoad, VLTFile vltFile )
			{
				BinaryReader br;

				BinaryReader brR = new BinaryReader( vltFile.RawStream );
				BinaryReader brV = new BinaryReader( vltFile.VltStream );

				VLTDataRow dataRow = new VLTDataRow( this._vltClass.FieldCount );
				VLTPointers pointers = vltFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;

				int offset = 0;

#if CARBON
				if( pointers[collLoad.Pointer] == null )
				{
					/*
					Debug.WriteLine(string.Format("Skipped Exp.. class='{0}', name='{1}', num1={2}, exp_ptr={3:x}, ptr_ptr={4:x}", 
						HashResolver.Resolve(collLoad.ClassNameHash), HashResolver.Resolve(collLoad.NameHash), 
						collLoad.Num1, collLoad.Address, collLoad.Pointer));
						*/
				}
				else
				{
					offset = pointers[collLoad.Pointer].OffsetDest;
				}
#else
				offset = pointers[collLoad.Pointer].OffsetDest;
#endif

				dataRow.VLTFile = vltFile;
				dataRow.VLTClass = this._vltClass;
				dataRow.CollectionLoad = collLoad;

				for( int i = 0; i < this._vltClass.FieldCount; i++ )
				{
#if CARBON
					bool runtimeLink = false;
#endif
					ClassField field = this._vltClass[i];

					if( field.IsOptional )
					{
						br = null;
						for( int j = 0; j < collLoad.CountOptional; j++ )
						{
							if( collLoad[j].NameHash == field.NameHash )
							{
								if( collLoad[j].IsDataEmbedded )
								{
									br = brV;
									br.BaseStream.Seek( collLoad[j].Pointer, SeekOrigin.Begin );
								}
								else
								{
									VLTPointerBlock block = pointers[collLoad[j].Pointer];
#if CARBON
									if( block != null )
#endif
									{
										int localOffset = block.OffsetDest;
										br = brR;
										br.BaseStream.Seek( localOffset, SeekOrigin.Begin );
									}
#if CARBON
									else
									{
										/*
										System.Diagnostics.Debug.WriteLine(
											string.Format("Runtime Linkage '{0}', name '{1}', field '{2}', req ptr '{3:x}'",
												HashResolver.Resolve(_vltClass.ClassHash), HashResolver.Resolve(collLoad.NameHash),
												HashResolver.Resolve(field.NameHash), collLoad[j].Pointer));
										*/
										runtimeLink = true;
										br = brR;
									}
#endif
								}

							}
						}
						if( br == null )
						{
							// data is not defined
							continue;
						}
					}
					else
					{
						br = brR;
						br.BaseStream.Seek( offset + field.Offset, SeekOrigin.Begin );
					}

					/*
					#if !CARBON
										if (!field.IsArray && field.Alignment > 0)
										{
											int align = field.Alignment;
											if (br.BaseStream.Position % align != 0)
												br.BaseStream.Position += align - (br.BaseStream.Position % align);
										}
					#endif
					*/

					Type type = VLTTypeResolver.Resolver.Resolve( field.TypeHash );

					if( type == null )
					{
						type = typeof( VLTDataItems.Unknown );
					}

					VLTDataItem dataItem;
					if( field.IsArray )
					{
						dataItem = VLTDataItemArray.Instantiate( field, type );
					}
					else
					{
						dataItem = VLTDataItem.Instantiate( type );
						if( dataItem is VLTDataItems.Unknown )
						{
							( dataItem as VLTDataItems.Unknown ).SetLength( field.Length );
						}
					}

					dataItem.Offset = 0;
					dataItem.InlineData = ( br == brV );
					dataItem.TypeHash = field.TypeHash;
					dataItem.NameHash = field.NameHash;
					dataItem.DataRow = dataRow;

					if( offset != 0 && !runtimeLink )
					{
						dataItem.Offset = (uint)br.BaseStream.Position;
						dataItem.Read( br );
					}

					dataRow[i] = dataItem;
				}

				this._collection.Add( dataRow );
			}
			#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			#endregion
		}

		public class ClassField : IFileAccess
		{
			public uint NameHash;
			public uint TypeHash;
			public ushort Offset;
			public ushort Length;
			public short Count;
			public byte Flags;
			public byte AlignShift;

			public bool IsArray
			{
				get
				{
					return ( this.Flags & 0x1 ) != 0;
				}
			}

			public bool IsOptional
			{
				get
				{
					return ( this.Flags & 0x2 ) == 0;
				}
			}

			public int Alignment
			{
				get
				{
					return 1 << this.AlignShift;
				}
			}

			#region IFileAccess Members

			public void Read( BinaryReader br )
			{
				this.NameHash = br.ReadUInt32();
				this.TypeHash = br.ReadUInt32();
				this.Offset = br.ReadUInt16();
				this.Length = br.ReadUInt16();
				this.Count = br.ReadInt16();
				this.Flags = br.ReadByte();
				this.AlignShift = br.ReadByte();
			}

			public void Write( BinaryWriter bw )
			{
				bw.Write( this.NameHash );
				bw.Write( this.TypeHash );
				bw.Write( this.Offset );
				bw.Write( this.Length );
				bw.Write( this.Count );
				bw.Write( this.Flags );
				bw.Write( this.AlignShift );
			}

			#endregion
		}

		private VLTFile _vltFile;
		private uint _classHash;
		private VLTDataClassLoad _classLoad;
		private ClassField[] _classFields;
		private ClassData _data;
		private VLTDatabase _database;
		private VLTPointers _pointers;

		public VLTFile VLTFile
		{
			get
			{
				return this._vltFile;
			}
		}

		public VLTDatabase VLTDatabase
		{
			get
			{
				return this._database;
			}
			set
			{
				this._database = value;
			}
		}

		public VLTPointers VLTPointers
		{
			get
			{
				return this._pointers;
			}
		}
		public VLTDataClassLoad ClassLoad
		{
			get
			{
				return this._classLoad;
			}
		}

		public uint ClassHash
		{
			get
			{
				return this._classHash;
			}
		}

		public int FieldCount
		{
			get
			{
				return this._classLoad.TotalFieldsCount;
			}
		}

		public ClassField this[int index]
		{
			get
			{
				return this._classFields[index] as ClassField;
			}
		}

		public ClassData Data
		{
			get
			{
				return this._data;
			}
		}

		public void LoadClass( VLTDataClassLoad classLoad, VLTFile vltFile )
		{
			this._vltFile = vltFile;
			this._classLoad = classLoad;
			this._classHash = classLoad.NameHash;

			this._pointers = vltFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
			int offset = this._pointers[classLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek( offset, SeekOrigin.Begin );
			BinaryReader br = new BinaryReader( vltFile.RawStream );

			this._classFields = new ClassField[this._classLoad.TotalFieldsCount];
			for( int i = 0; i < this._classLoad.TotalFieldsCount; i++ )
			{
				ClassField field = new ClassField();
				field.Read( br );

				// HACK: for hash dumping later on
				HashResolver.Resolve( field.NameHash );

				this._classFields[i] = field;
			}

			this._data = new ClassData( this );


		}

		public int GetFieldIndex( uint hash )
		{
			for( int i = 0; i < this._classFields.Length; i++ )
			{
				if( this._classFields[i].NameHash == hash )
				{
					return i;
				}
			}
			return -1;
		}

		#region IComparable Members

		public int CompareTo( object obj )
		{
			return this._classHash.CompareTo( ( obj as VLTClass )._classHash );
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this._classFields.GetEnumerator();
		}

		#endregion
	}

	public class VLTDatabase : IEnumerable
	{
		public class VLTType
		{
			public uint Hash;
			public string TypeName;
			public int Length;
		}

		private Hashtable _types;
		private Hashtable _classes;

		public VLTType GetType( uint hash )
		{
			return this._types[hash] as VLTType;
		}

		public VLTClass[] GetClasses()
		{
			VLTClass[] classes = new VLTClass[this._classes.Values.Count];
			this._classes.Values.CopyTo( classes, 0 );
			return classes;
		}

		public VLTClass this[uint hash]
		{
			get
			{
				return this._classes[hash] as VLTClass;
			}
		}

		public void LoadDatabase( VLTDataDatabaseLoad dbLoad, VLTFile vltFile )
		{
			VLTPointers vltPointers = vltFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
			int offset = vltPointers[dbLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek( offset, SeekOrigin.Begin );
			BinaryReader br = new BinaryReader( vltFile.RawStream );

			this._types = new Hashtable( dbLoad.Count );

			for( int i = 0; i < dbLoad.Count; i++ )
			{
				VLTType type = new VLTType();
				type.TypeName = NullTerminatedString.Read( br );
				type.Length = dbLoad[i];
				type.Hash = VLTHasher.Hash( type.TypeName );
				this._types.Add( type.Hash, type );
				HashResolver.AddAuto( type.TypeName );
			}

			this._classes = new Hashtable();
		}

		public void LoadClass( VLTDataClassLoad classLoad, VLTFile vltFile )
		{
			VLTClass vltClass = new VLTClass();
			vltClass.LoadClass( classLoad, vltFile );
			vltClass.VLTDatabase = this;
			this._classes.Add( classLoad.NameHash, vltClass );
		}
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this._classes.Values.GetEnumerator();
		}

		#endregion
	}
}
