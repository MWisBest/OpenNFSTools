using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using NFSTools.LibNFS.Crypto;
using NFSTools.VLTEdit.VLTTypes;

namespace NFSTools.VLTEdit
{
	public class VLTDataRow : IEnumerable
	{
		private VLTDataItem[] _dataItem;
		private bool[] _dataLoaded;

		public VLTClass VLTClass { get; set; }

		public VLTFile VLTFile { get; set; }

		public VLTDataCollectionLoad CollectionLoad { get; set; }

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
			private List<VLTDataRow> _collection;
			public ClassData( VLTClass vltClass )
			{
				this._vltClass = vltClass;
				this._collection = new List<VLTDataRow>( vltClass.ClassLoad.CollectionCount );
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
					return this._collection[index];
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
						type = typeof( VLTTypes.UnknownType );
					}

					VLTDataItem dataItem;
					if( field.IsArray )
					{
						dataItem = VLTDataItemArray.Instantiate( field, type );
					}
					else
					{
						dataItem = VLTDataItem.Instantiate( type );
						if( dataItem is VLTTypes.UnknownType )
						{
							( dataItem as VLTTypes.UnknownType ).SetLength( field.Length );
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

		private ClassField[] _classFields;

		public VLTFile VLTFile { get; private set; }

		public VLTDatabase VLTDatabase { get; set; }

		public VLTPointers VLTPointers { get; private set; }

		public VLTDataClassLoad ClassLoad { get; private set; }

		public uint ClassHash { get; private set; }

		public int FieldCount
		{
			get
			{
				return this.ClassLoad.TotalFieldsCount;
			}
		}

		public ClassField this[int index]
		{
			get
			{
				return this._classFields[index] as ClassField;
			}
		}

		public ClassData Data { get; private set; }

		public void LoadClass( VLTDataClassLoad classLoad, VLTFile vltFile )
		{
			this.VLTFile = vltFile;
			this.ClassLoad = classLoad;
			this.ClassHash = classLoad.NameHash;

			this.VLTPointers = vltFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
			int offset = this.VLTPointers[classLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek( offset, SeekOrigin.Begin );
			BinaryReader br = new BinaryReader( vltFile.RawStream );

			this._classFields = new ClassField[this.ClassLoad.TotalFieldsCount];
			for( int i = 0; i < this.ClassLoad.TotalFieldsCount; i++ )
			{
				ClassField field = new ClassField();
				field.Read( br );

				// HACK: for hash dumping later on
				HashResolver.Resolve( field.NameHash );

				this._classFields[i] = field;
			}

			this.Data = new ClassData( this );


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
			return this.ClassHash.CompareTo( ( obj as VLTClass ).ClassHash );
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

		private Dictionary<uint, VLTType> _types;
		private Dictionary<uint, VLTClass> _classes;

		public VLTType GetType( uint hash )
		{
			return this._types[hash];
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
				return this._classes[hash];
			}
		}

		public void LoadDatabase( VLTDataDatabaseLoad dbLoad, VLTFile vltFile )
		{
			VLTPointers vltPointers = vltFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
			int offset = vltPointers[dbLoad.Pointer].OffsetDest;
			vltFile.RawStream.Seek( offset, SeekOrigin.Begin );
			BinaryReader br = new BinaryReader( vltFile.RawStream );

			this._types = new Dictionary<uint, VLTType>( dbLoad.Count );

			for( int i = 0; i < dbLoad.Count; i++ )
			{
				VLTType type = new VLTType();
				type.TypeName = NullTerminatedString.Read( br );
				type.Length = dbLoad[i];
				type.Hash = JenkinsHash.getHash32( type.TypeName );
				this._types.Add( type.Hash, type );
				HashResolver.AddAuto( type.TypeName );
			}

			this._classes = new Dictionary<uint, VLTClass>();
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
