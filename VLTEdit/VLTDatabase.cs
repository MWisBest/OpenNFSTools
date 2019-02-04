using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace vltedit
{

	public class VLTTypeResolver
	{
		private Hashtable _typeTable;
		public static VLTTypeResolver Resolver = new VLTTypeResolver();
		public VLTTypeResolver()
		{
			_typeTable = new Hashtable();
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Double"), typeof(VLTDataItems.Double));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Float"), typeof(VLTDataItems.Float));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::UInt64"), typeof(VLTDataItems.UInt64));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::UInt32"), typeof(VLTDataItems.UInt32));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::UInt16"), typeof(VLTDataItems.UInt16));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::UInt8"), typeof(VLTDataItems.UInt8));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Int64"), typeof(VLTDataItems.Int64));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Int32"), typeof(VLTDataItems.Int32));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Int16"), typeof(VLTDataItems.Int16));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Int8"), typeof(VLTDataItems.Int8));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Bool"), typeof(VLTDataItems.Bool));
			_typeTable.Add(VLTHasher.Hash("EA::Reflection::Text"), typeof(VLTDataItems.Text));

			_typeTable.Add(VLTHasher.Hash("Attrib::Types::Matrix"), typeof(VLTDataItems.Matrix));
			_typeTable.Add(VLTHasher.Hash("Attrib::Types::Vector4"), typeof(VLTDataItems.Vector4));
			_typeTable.Add(VLTHasher.Hash("Attrib::Types::Vector3"), typeof(VLTDataItems.Vector3));
			_typeTable.Add(VLTHasher.Hash("Attrib::Types::Vector2"), typeof(VLTDataItems.Vector2));
			_typeTable.Add(VLTHasher.Hash("Attrib::StringKey"), typeof(VLTDataItems.StringKey));
			_typeTable.Add(VLTHasher.Hash("Attrib::RefSpec"), typeof(VLTDataItems.RefSpec));
			_typeTable.Add(VLTHasher.Hash("Attrib::Blob"), typeof(VLTDataItems.Blob));
			
			_typeTable.Add(VLTHasher.Hash("UpgradeSpecs"), typeof(VLTDataItems.UpgradeSpecs));
			_typeTable.Add(VLTHasher.Hash("JunkmanMod"), typeof(VLTDataItems.JunkmanMod));
			_typeTable.Add(VLTHasher.Hash("AxlePair"), typeof(VLTDataItems.AxlePair));
			_typeTable.Add(VLTHasher.Hash("CarBodyMotion"), typeof(VLTDataItems.CarBodyMotion));
			_typeTable.Add(VLTHasher.Hash("GCollectionKey"), typeof(VLTDataItems.GCollectionKey));
			

		}
		public Type Resolve(uint hash)
		{
			if (_typeTable.ContainsKey(hash))
				return _typeTable[hash] as Type;
			return null;
		}
	}
	
	public class DataValueAttribute : Attribute
	{
		private string _name;
		private bool _hex;

		public DataValueAttribute(string name)
		{
			_name = name;
			_hex = false;
		}

		public bool Hex
		{
			get { return _hex; }
			set { _hex = value; }
		}

		public string Name
		{
			get { return _name; }
		}
	}

	public class VLTHasher
	{

		public static ulong Hash64(string k)
		{
			return Hash64(k, 0x11223344ABCDEF00);
		}

		public static ulong Hash64(string k, ulong init)
		{
			int koffs = 0;
			int len = k.Length;
			ulong a = 0x9e3779b97f4a7c13;
			ulong b = a;
			ulong c = init;

			byte[] charArr = Encoding.ASCII.GetBytes(k);
			while(len >= 24)
			{
				a += BitConverter.ToUInt64(charArr, koffs);
				b += BitConverter.ToUInt64(charArr, koffs+8);
				c += BitConverter.ToUInt64(charArr, koffs+16);

				a -= b; a -= c; a ^= (c>>43);
				b -= c; b -= a; b ^= (a<<9);
				c -= a; c -= b; c ^= (b>>8);
				a -= b; a -= c; a ^= (c>>38);
				b -= c; b -= a; b ^= (a<<23);
				c -= a; c -= b; c ^= (b>>5); 
				a -= b; a -= c; a ^= (c>>35);
				b -= c; b -= a; b ^= (a<<49);
				c -= a; c -= b; c ^= (b>>11);
				a -= b; a -= c; a ^= (c>>12);
				b -= c; b -= a; b ^= (a<<18);
				c -= a; c -= b; c ^= (b>>22);

				len -= 24;
				koffs += 24;
			}

			c += (ulong)k.Length;

			switch(len)
			{
				case 23: 
					c+=((ulong)k[22]<<56); 
					goto case 22;
				case 22: 
					c+=((ulong)k[21]<<48);
					goto case 21;
				case 21: 
					c+=((ulong)k[20]<<40);
					goto case 20;
				case 20: 
					c+=((ulong)k[19]<<32);
					goto case 19;
				case 19: 
					c+=((ulong)k[18]<<24);
					goto case 18;
				case 18: 
					c+=((ulong)k[17]<<16);
					goto case 17;
				case 17: 
					c+=((ulong)k[16]<<8);
					goto case 16;
					/* the first byte of c is reserved for the length */
				case 16: 
					b+=((ulong)k[15]<<56);
					goto case 15;
				case 15: 
					b+=((ulong)k[14]<<48);
					goto case 14;
				case 14: 
					b+=((ulong)k[13]<<40);
					goto case 13;
				case 13: 
					b+=((ulong)k[12]<<32);
					goto case 12;
				case 12: 
					b+=((ulong)k[11]<<24);
					goto case 11;
				case 11: 
					b+=((ulong)k[10]<<16);
					goto case 10;
				case 10: 
					b+=((ulong)k[ 9]<<8);
					goto case 9;
				case  9: 
					b+=((ulong)k[ 8]);
					goto case 8;
				case  8: 
					a+=((ulong)k[ 7]<<56);
					goto case 7;
				case  7: 
					a+=((ulong)k[ 6]<<48);
					goto case 6;
				case  6: 
					a+=((ulong)k[ 5]<<40);
					goto case 5;
				case  5: 
					a+=((ulong)k[ 4]<<32);
					goto case 4;
				case  4: 
					a+=((ulong)k[ 3]<<24);
					goto case 3;
				case  3: 
					a+=((ulong)k[ 2]<<16);
					goto case 2;
				case  2: 
					a+=((ulong)k[ 1]<<8);
					goto case 1;
				case  1: 
					a+=((ulong)k[ 0]);
					break;
			}

			a -= b; a -= c; a ^= (c>>43);
			b -= c; b -= a; b ^= (a<<9);
			c -= a; c -= b; c ^= (b>>8);
			a -= b; a -= c; a ^= (c>>38);
			b -= c; b -= a; b ^= (a<<23);
			c -= a; c -= b; c ^= (b>>5); 
			a -= b; a -= c; a ^= (c>>35);
			b -= c; b -= a; b ^= (a<<49);
			c -= a; c -= b; c ^= (b>>11);
			a -= b; a -= c; a ^= (c>>12);
			b -= c; b -= a; b ^= (a<<18);
			c -= a; c -= b; c ^= (b>>22);

			return c;
		}

		public static uint Hash(string k)
		{
			return Hash(k, 0xABCDEF00);
		}

		public static uint Hash(string k, uint init)
		{
			int koffs = 0;
			int len = k.Length;
			uint a = 0x9e3779b9;
			uint b = a;
			uint c = init;

			while(len >= 12)
			{
				a += (uint)k[0+koffs] + ((uint)k[1+koffs]<<8) + ((uint)k[2+koffs]<<16) + ((uint)k[3+koffs]<<24);
				b += (uint)k[4+koffs] + ((uint)k[5+koffs]<<8) + ((uint)k[6+koffs]<<16) + ((uint)k[7+koffs]<<24);
				c += (uint)k[8+koffs] + ((uint)k[9+koffs]<<8) + ((uint)k[10+koffs]<<16) + ((uint)k[11+koffs]<<24);

				a -= b; a -= c; a ^= (c>>13);
				b -= c; b -= a; b ^= (a<<8);
				c -= a; c -= b; c ^= (b>>13);
				a -= b; a -= c; a ^= (c>>12); 
				b -= c; b -= a; b ^= (a<<16);
				c -= a; c -= b; c ^= (b>>5);
				a -= b; a -= c; a ^= (c>>3);
				b -= c; b -= a; b ^= (a<<10);
				c -= a; c -= b; c ^= (b>>15);

				koffs += 12;
				len -= 12;
			}

			c += (uint)k.Length;

			switch(len)
			{
				case 11: 
					c += (uint)k[10+koffs]<<24;
					goto case 10;
				case 10: 
					c += (uint)k[9+koffs]<<16;
					goto case 9;
				case 9:
					c += (uint)k[8+koffs]<<8;
					goto case 8;
				case 8 : 
					b += (uint)k[7+koffs]<<24;
					goto case 7;
				case 7 : 
					b += (uint)k[6+koffs]<<16;
					goto case 6;
				case 6 : 
					b += (uint)k[5+koffs]<<8;
					goto case 5;
				case 5 : 
					b += (uint)k[4+koffs];
					goto case 4;
				case 4 : 
					a += (uint)k[3+koffs]<<24;
					goto case 3;
				case 3 : 
					a += (uint)k[2+koffs]<<16;
					goto case 2;
				case 2 : 
					a += (uint)k[1+koffs]<<8;
					goto case 1;
				case 1 : 
					a += (uint)k[0+koffs];
					break;
			}

			a -= b; a -= c; a ^= (c>>13);
			b -= c; b -= a; b ^= (a<<8);
			c -= a; c -= b; c ^= (b>>13);
			a -= b; a -= c; a ^= (c>>12); 
			b -= c; b -= a; b ^= (a<<16);
			c -= a; c -= b; c ^= (b>>5);
			a -= b; a -= c; a ^= (c>>3);
			b -= c; b -= a; b ^= (a<<10);
			c -= a; c -= b; c ^= (b>>15);

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
			get { return _row; }
			set { _row = value; }
		}

		public int ArrayIndex
		{
			get { return _arrayIndex; }
			set { _arrayIndex = value; }
		}
		
		public uint TypeHash
		{
			get { return _type; }
			set { _type = value; }
		}

		public uint NameHash
		{
			get { return _name; }
			set { _name = value; }
		}

		public uint Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		public bool InlineData
		{
			get { return _inline; }
			set { _inline = value; }
		}

		public static VLTDataItem Instantiate(Type type)
		{
			ConstructorInfo mi = type.GetConstructor(Type.EmptyTypes);
			return mi.Invoke(null) as VLTDataItem;
		}

		public abstract void Read(BinaryReader br);
		public abstract void Write(BinaryWriter bw);

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

		public static VLTDataItemArray Instantiate(VLTClass.ClassField field, Type type)
		{
			VLTDataItemArray array = new VLTDataItemArray();
			array._align = field.Alignment;
			array._type = field.TypeHash;
			array._itemType = type;
			array._items = new ArrayList();
			return array;
		}

		public override void Read(BinaryReader br)
		{
			_maxCount = br.ReadInt16();
			_validCount = br.ReadInt16();
			_dataSize = br.ReadInt16();
			br.ReadInt16();

			ConstructorInfo mi = _itemType.GetConstructor(Type.EmptyTypes);
			
			for(int i=0; i<_maxCount; i++)
			{
				if (_align > 0)
				{
					if (br.BaseStream.Position % _align != 0)
						br.BaseStream.Position += _align - (br.BaseStream.Position % _align);
				}

				VLTDataItem item = mi.Invoke(null) as VLTDataItem;

				if (item is VLTDataItems.Unknown)
					(item as VLTDataItems.Unknown).SetLength(_dataSize);

				item.Offset = (uint)br.BaseStream.Position;
				item.InlineData = false;
				item.TypeHash = _type;
				item.DataRow = this.DataRow;
				item.ArrayIndex = i;

				item.Read(br);

				_items.Add(item);
			}
		}
		public VLTDataItem this[int index]
		{
			get { return _items[index] as VLTDataItem; }
			set { _items[index] = value; }
		}

		public int Count
		{
			get { return _items.Count; }
		}
		
		public short MaxCount
		{
			get { return _maxCount; }
		}
		
		public short ValidCount
		{
			get { return _validCount; }
		}
		
		public short DataSize
		{
			get { return _dataSize; }
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(_maxCount);
			bw.Write(_validCount);
			bw.Write(_dataSize);
			bw.Write((short)0);
			for(int i=0; i<_maxCount; i++)
			{
				if (_align > 0)
				{
					if (bw.BaseStream.Position % _align != 0)
						bw.BaseStream.Position += _align - (bw.BaseStream.Position % _align);
				}
				(_items[i] as VLTDataItem).Write(bw);
			}			
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _items.GetEnumerator();
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
			get { return _vltClass; }
			set { _vltClass = value; }
		}
		public VLTFile VLTFile
		{
			get { return _vltFile; }
			set { _vltFile = value; }
		}
		public VLTDataCollectionLoad CollectionLoad
		{
			get { return _collLoad; }
			set { _collLoad = value; }
		}
		public VLTDataRow(int count)
		{
			_dataItem = new VLTDataItem[count];
			_dataLoaded = new bool[count];
		}
		public VLTDataItem this[int index]
		{
			get { return _dataItem[index]; }
			set
			{
				_dataLoaded[index] = true;
				_dataItem[index] = value;
			}
		}

		public bool IsDataLoaded(int index)
		{
			return _dataLoaded[index];
		}

		public IEnumerator GetEnumerator()
		{
			return _dataItem.GetEnumerator();
		}
	}

	public class VLTClass : IComparable, IEnumerable
	{
		public class ClassData : IEnumerable
		{
			private VLTClass _vltClass;
			private ArrayList _collection;
			public ClassData(VLTClass vltClass)
			{
				_vltClass = vltClass;
				_collection = new ArrayList(vltClass.ClassLoad.CollectionCount);
			}
			public int Count
			{
				get { return _collection.Count; }
			}
			public VLTDataRow this[uint hash]
			{
				get
				{
					foreach(VLTDataRow row in _collection)
						if (row.CollectionLoad.NameHash == hash)
							return row;
					return null;
				}
			}
			public VLTDataRow this[int index]
			{
				get { return _collection[index] as VLTDataRow; }
			}
			
			public void Add(VLTDataCollectionLoad collLoad, VLTFile vltFile)
			{
				BinaryReader br;
				
				BinaryReader brR = new BinaryReader(vltFile.RawStream);
				BinaryReader brV = new BinaryReader(vltFile.VltStream);

				VLTDataRow dataRow = new VLTDataRow(_vltClass.FieldCount);
				VLTPointers pointers = vltFile.GetChunk(VLTChunkId.Pointers) as VLTPointers;
				
				int offset = 0;

#if CARBON
				if (pointers[collLoad.Pointer] == null)
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
				dataRow.VLTClass = _vltClass;
				dataRow.CollectionLoad = collLoad;
				
				for(int i=0; i<_vltClass.FieldCount; i++)
				{
#if CARBON
					bool runtimeLink = false;
#endif
					ClassField field = _vltClass[i];

					if (field.IsOptional)
					{
						br = null;
						for (int j=0; j<collLoad.CountOptional; j++)
						{
							if (collLoad[j].NameHash == field.NameHash)
							{
								if (collLoad[j].IsDataEmbedded)
								{
									br = brV;
									br.BaseStream.Seek(collLoad[j].Pointer, SeekOrigin.Begin);
								} 
								else
								{
									VLTPointerBlock block = pointers[collLoad[j].Pointer];
#if CARBON
									if (block != null) 
#endif
									{
										int localOffset = block.OffsetDest;
										br = brR;
										br.BaseStream.Seek(localOffset, SeekOrigin.Begin);
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
						if (br == null)
						{
							// data is not defined
							continue;
						}
					} 
					else
					{
						br = brR;
						br.BaseStream.Seek(offset+field.Offset, SeekOrigin.Begin);
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
					
					Type type = VLTTypeResolver.Resolver.Resolve(field.TypeHash);
					
					if (type == null)
						type = typeof(VLTDataItems.Unknown);

					VLTDataItem dataItem;
					if (field.IsArray)
					{
						dataItem = VLTDataItemArray.Instantiate(field, type);
					}
					else
					{
						dataItem = VLTDataItem.Instantiate(type);
						if (dataItem is VLTDataItems.Unknown)
							(dataItem as VLTDataItems.Unknown).SetLength(field.Length);
						
					}

					dataItem.Offset = 0;
					dataItem.InlineData = (br == brV);
					dataItem.TypeHash = field.TypeHash;
					dataItem.NameHash = field.NameHash;
					dataItem.DataRow = dataRow;

					if (offset != 0 && !runtimeLink)
					{
						dataItem.Offset = (uint)br.BaseStream.Position;
						dataItem.Read(br);
					}

					dataRow[i] = dataItem;
				}

				_collection.Add(dataRow);
			}
			#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				return _collection.GetEnumerator();
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
				get { return (Flags & 0x1) != 0; }
			}
			
			public bool IsOptional
			{
				get { return (Flags & 0x2) == 0; }
			}

			public int Alignment
			{
				get
				{
					return 1 << AlignShift;
				}
			}

			#region IFileAccess Members

			public void Read(BinaryReader br)
			{
				NameHash = br.ReadUInt32();
				TypeHash = br.ReadUInt32();
				Offset = br.ReadUInt16();
				Length = br.ReadUInt16();
				Count = br.ReadInt16();
				Flags = br.ReadByte();
				AlignShift = br.ReadByte();
			}

			public void Write(BinaryWriter bw)
			{
				bw.Write(NameHash);
				bw.Write(TypeHash);
				bw.Write(Offset);
				bw.Write(Length);
				bw.Write(Count);
				bw.Write(Flags);
				bw.Write(AlignShift);
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
			get { return _vltFile; }
		}

		public VLTDatabase VLTDatabase
		{
			get { return _database; }
			set { _database = value; }
		}

		public VLTPointers VLTPointers
		{
			get { return _pointers; }
		}
		public VLTDataClassLoad ClassLoad
		{
			get { return _classLoad; }
		}

		public uint ClassHash
		{
			get { return _classHash; }
		}

		public int FieldCount
		{
			get { return _classLoad.TotalFieldsCount; }
		}

		public ClassField this[int index]
		{
			get { return _classFields[index] as ClassField; }
		}

		public ClassData Data
		{
			get { return _data; }
		}

		public void LoadClass(VLTDataClassLoad classLoad, VLTFile vltFile)
		{
			_vltFile = vltFile;
			_classLoad = classLoad;
			_classHash = classLoad.NameHash;

			_pointers = vltFile.GetChunk(VLTChunkId.Pointers) as VLTPointers;
			int offset = _pointers[classLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek(offset, SeekOrigin.Begin);
			BinaryReader br = new BinaryReader(vltFile.RawStream);

			_classFields = new ClassField[_classLoad.TotalFieldsCount];
			for(int i=0; i<_classLoad.TotalFieldsCount; i++) 
			{
				ClassField field = new ClassField();
				field.Read(br);

				// HACK: for hash dumping later on
				HashResolver.Resolve(field.NameHash);

				_classFields[i] = field;
			}

			_data = new ClassData(this);

			
		}

		public int GetFieldIndex(uint hash)
		{
			for(int i=0; i<_classFields.Length; i++)
			{
				if (_classFields[i].NameHash == hash)
					return i;
			}
			return -1;
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			return _classHash.CompareTo((obj as VLTClass)._classHash);
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _classFields.GetEnumerator();
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

		public VLTType GetType(uint hash)
		{
			return _types[hash] as VLTType;
		}

		public VLTClass[] GetClasses()
		{
			VLTClass[] classes = new VLTClass[_classes.Values.Count];
			_classes.Values.CopyTo(classes, 0);
			return classes;
		}

		public VLTClass this[uint hash]
		{
			get { return _classes[hash] as VLTClass; }
		}

		public void LoadDatabase(VLTDataDatabaseLoad dbLoad, VLTFile vltFile)
		{
			VLTPointers vltPointers = vltFile.GetChunk(VLTChunkId.Pointers) as VLTPointers;
			int offset = vltPointers[dbLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek(offset, SeekOrigin.Begin);
			BinaryReader br = new BinaryReader(vltFile.RawStream);

			_types = new Hashtable(dbLoad.Count);

			for (int i=0; i<dbLoad.Count; i++) {
				VLTType type = new VLTType();
				type.TypeName = NullTerminatedString.Read(br);
				type.Length = dbLoad[i];
				type.Hash = VLTHasher.Hash(type.TypeName);
				_types.Add(type.Hash, type);
                HashResolver.AddAuto(type.TypeName);
			}

			_classes = new Hashtable();
		}

		public void LoadClass(VLTDataClassLoad classLoad, VLTFile vltFile)
		{
			VLTClass vltClass = new VLTClass();
			vltClass.LoadClass(classLoad, vltFile);
			vltClass.VLTDatabase = this;
			_classes.Add(classLoad.NameHash, vltClass);
		}
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _classes.Values.GetEnumerator();
		}

		#endregion
	}
}
