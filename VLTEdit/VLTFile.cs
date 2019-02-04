using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace vltedit
{
	public interface IFileAccess
	{
		void Read(BinaryReader br);
		void Write(BinaryWriter bw);
	}

	public interface IAddressable
	{
		long Address { get; set; }
	}

	public class NullTerminatedString
	{
		public static string Read(BinaryReader br)
		{
			StringBuilder sb = new StringBuilder();
			byte b;
			do
			{
				b = br.ReadByte();
				if (b != 0)
				{
					sb.Append((char)b);
				}
			} while (b != 0);
			return sb.ToString();
		}
		public static void Write(BinaryWriter bw, string value)
		{
			byte[] str = Encoding.ASCII.GetBytes(value);
			bw.Write(str);
			bw.Write((byte)0);
		}
	}

	public enum VLTChunkId : int
	{
		Dependency = 0x4465704E,
		StringsRaw = 0x53747245,
		Strings = 0x5374724E,
		Data = 0x4461744E,
		Expression = 0x4578704E,
		Pointers = 0x5074724E,
		Null = 0,
	}

	public class VLTChunk : IFileAccess
	{
		VLTChunkId _chunkId;
		int _length;
		long _offset;

		public VLTChunkId ChunkId
		{
			get { return _chunkId; }
			set { _chunkId = value; }
		}

		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}
		
		public int DataLength
		{
			get { return _length - 0x8; }
			set { _length = value + 0x8; }
		}

		public bool IsValid
		{
			get { return _length >= 0x8; }
		}

		public void GotoStart(Stream stream) 
		{
			stream.Seek(_offset + 0x8, SeekOrigin.Begin);
		}

		public void SkipChunk(Stream stream)
		{
			stream.Seek(_offset + _length, SeekOrigin.Begin);
		}

		#region IFileAccess Members

		public void Read(BinaryReader br)
		{
			_offset = br.BaseStream.Position;
			_chunkId = (VLTChunkId)br.ReadInt32();
			_length = br.ReadInt32();
		}

		public void Write(BinaryWriter bw)
		{
			bw.Write((int)_chunkId);
			bw.Write(_length);
		}

		#endregion
	}

	public abstract class VLTBase : IFileAccess
	{
		protected VLTChunk _chunk;
		public VLTChunk Chunk
		{
			get { return _chunk; }
			set { _chunk = value; }
		}

		public abstract void Read(BinaryReader br);
		public abstract void Write(BinaryWriter bw);
	}

	public class VLTDependency : VLTBase
	{
		public const int VltFile = 0;
		public const int BinFile = 1;

		int _count;
		uint[] _hashes;
		string[] _names;

		public uint GetHash(int index)
		{
			return _hashes[index];
		}

		public string GetName(int index)
		{
			return _names[index];
		}

		public override void Read(BinaryReader br)
		{
			int[] offsets;
			long position;

			_count = br.ReadInt32();
			_hashes = new uint[_count];
			_names = new string[_count];
			offsets = new int[_count];
			for(int i=0; i<_count; i++)
				_hashes[i] = br.ReadUInt32();
			for(int i=0; i<_count; i++)
				offsets[i] = br.ReadInt32();

			position = br.BaseStream.Position;
			for(int i=0; i<_count; i++)
			{
				br.BaseStream.Seek(position+offsets[i], SeekOrigin.Begin);
				_names[i] = NullTerminatedString.Read(br);
			}
		}

		public override void Write(BinaryWriter bw)
		{
			int length = 0;
			bw.Write(_count);
			for(int i=0; i<_count; i++)
				bw.Write(_hashes[i]);
			for(int i=0; i<_count; i++)
			{
				bw.Write(length);
				length += _names[i].Length + 1;
			}
			for(int i=0; i<_count; i++)
				NullTerminatedString.Write(bw, _names[i]);
		}

	}

	public class VLTRaw : VLTBase
	{
		byte[] _data;

		public byte[] Data
		{
			get { return _data; }
			set { _data = value; }
		}

		public Stream GetStream()
		{
			return new MemoryStream(_data);
		}

		public override void Read(BinaryReader br)
		{
			_data = br.ReadBytes(_chunk.DataLength);
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(_data);
		}
	}
	public abstract class VLTDataBlock : IAddressable, IFileAccess
	{
		protected VLTExpressionBlock _expression;
		protected long _address;

		public abstract void Read(BinaryReader br);
		public abstract void Write(BinaryWriter bw);

		public long Address
		{
			get { return _address; }
			set { _address = value; }
		}

		public VLTExpressionBlock Expression
		{
			get { return _expression; }
			set { _expression = value; }
		}

		public static VLTDataBlock CreateOfType(VLTExpressionType type)
		{
			switch (type)
			{
				case VLTExpressionType.DatabaseLoadData:
					return new VLTDataDatabaseLoad();
				case VLTExpressionType.ClassLoadData:
					return new VLTDataClassLoad();
				case VLTExpressionType.CollectionLoadData:
					return new VLTDataCollectionLoad();
				default:
					return null;
			}
		}

		public VLTDataDatabaseLoad AsDatabaseLoad()
		{
			return this as VLTDataDatabaseLoad;
		}
		public VLTDataClassLoad AsClassLoad()
		{
			return this as VLTDataClassLoad;
		}
		public VLTDataCollectionLoad AsCollectionLoad()
		{
			return this as VLTDataCollectionLoad;
		}

	}
	public class VLTDataDatabaseLoad : VLTDataBlock
	{
		int _num1, _num2, _count;
		int _pointer;
		int[] _sizes;

		public int this [int index]
		{
			get { return _sizes[index]; }
			set { _sizes[index] = value; }
		}

		public int Count 
		{
			get { return _count; }
		}

		public int Num1
		{
			get { return _num1; }
			set { _num1 = value; }
		}

		public int Num2
		{
			get { return _num2; }
			set { _num2 = value; }
		}

		/// <summary>
		/// Gets the pointer to a string array of types
		/// </summary>
		public int Pointer
		{
			get { return _pointer; }
		}

		public override void Read(BinaryReader br)
		{
			_num1 = br.ReadInt32();
			_num2 = br.ReadInt32();
			_count = br.ReadInt32();
			_pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			_sizes = new int[_count];
			for(int i=0; i<_count; i++)
			{
				_sizes[i] = br.ReadInt32();
			}
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(_num1);
			bw.Write(_num2);
			bw.Write(_count);
			bw.Write(0xEFFECADD);
			for(int i=0; i<_count; i++)
			{
				bw.Write(_sizes[i]);
			}
		}
	}
	public class VLTDataClassLoad : VLTDataBlock
	{
		uint _nameHash;
		int _collectionCount, _totalFieldsCount, _pointer;
		int _num2, _zero1, _requiredFieldsCount, _zero2;

		public uint NameHash
		{
			get { return _nameHash; }
		}

		public int CollectionCount
		{
			get { return _collectionCount; }
			set { _collectionCount = value; }
		}

		public int Num2
		{
			get { return _num2; }
			set { _num2 = value; }
		}

		public int TotalFieldsCount
		{
			get { return _totalFieldsCount; }
			set { _totalFieldsCount = value; }
		}

		public int RequiredFieldsCount
		{
			get { return _requiredFieldsCount; }
			set { _requiredFieldsCount = value; }
		}

		public int Pointer
		{
			get { return _pointer; }
		}

		public override void Read(BinaryReader br)
		{
			_nameHash = br.ReadUInt32();
			_collectionCount = br.ReadInt32();
			_totalFieldsCount = br.ReadInt32();

			_pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			_num2 = br.ReadInt32();
			_zero1 = br.ReadInt32();
			_requiredFieldsCount = br.ReadInt32();
			_zero2 = br.ReadInt32();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(_nameHash);
			bw.Write(_collectionCount);
			bw.Write(_totalFieldsCount);
			bw.Write(0xEFFECADD);
			bw.Write(_num2);
			bw.Write(_zero1);
			bw.Write(_requiredFieldsCount);
			bw.Write(_zero2);
		}
	}
	public class VLTDataCollectionLoad : VLTDataBlock
	{
		public class OptionalData : IFileAccess
		{
			uint _nameHash;
			int _pointer;
			short _flags1;
			short _flags2;

			public uint NameHash
			{
				get { return _nameHash; }
			}

			public int Pointer
			{
				get { return _pointer; }
			}

			public short Flags1
			{
				get { return _flags1; }
			}

			public short Flags2
			{
				get { return _flags2; }
			}

			public bool IsDataEmbedded
			{
				get { return (_flags2 & 0x20) != 0; }
			}

			#region IFileAccess Members

			public void Read(BinaryReader br)
			{
				_nameHash = br.ReadUInt32();
				_pointer = (int)br.BaseStream.Position;
				br.ReadInt32();
				_flags1 = br.ReadInt16();
				_flags2 = br.ReadInt16();
			}

			public void Write(BinaryWriter bw)
			{
				bw.Write(_nameHash);
				bw.Write(0xEFFECADD);	// beware, sometimes this is a ptr, sometimes this is data
										// recommend writing collload block before writing data
				bw.Write(_flags1);
				bw.Write(_flags2);
			}

			#endregion
		}

		uint _nameHash, _classNameHash, _parentHash;
		int _countOpt1, _num1;
		int _countOpt2, _countTypes, _pointer;
		uint[] _typeHashes;
		OptionalData[] _optionalData;
#if CARBON
		short _countTypesTotal;
#endif

		public uint NameHash
		{
			get { return _nameHash; }
		}

		public uint ClassNameHash
		{
			get { return _classNameHash; }
		}

		public uint ParentHash
		{
			get { return _parentHash; }
		}

		public int Count
		{
			get { return _countTypes; }
		}

		public int CountOptional
		{
			get { return _countOpt1; }
		}

		public int Pointer
		{
			get { return _pointer; }
		}

		/*
		public uint this[int index]
		{
			get { return  _typeHashes[index]; }
		}
		*/

		public OptionalData this[int index]
		{
			get { return _optionalData[index]; }
		}

		public int Num1
		{
			get { return _num1; }
		}

		public override void Read(BinaryReader br)
		{
			_nameHash = br.ReadUInt32();
			_classNameHash = br.ReadUInt32();
			_parentHash = br.ReadUInt32();
			_countOpt1 = br.ReadInt32();
			_num1 = br.ReadInt32();			// not always 0, 0xc in one case
			_countOpt2 = br.ReadInt32();

			Debug.Assert(_countOpt1 == _countOpt2, "CountOpt1 not equal to CountOpt2");

#if CARBON
			_countTypes = br.ReadInt16();
			_countTypesTotal = br.ReadInt16();
#else
			_countTypes = br.ReadInt32();
#endif

			_pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			_typeHashes = new uint[_countTypes];
			for(int i=0; i<_countTypes; i++)
			{
				_typeHashes[i] = br.ReadUInt32();
			}
#if CARBON
			for(int i=_countTypes; i<_countTypesTotal; i++) 
			{
				br.ReadInt32();
			}
#endif
			_optionalData = new OptionalData[_countOpt1];
			for(int i=0; i<_countOpt1; i++)
			{
				_optionalData[i] = new OptionalData();
				_optionalData[i].Read(br);
			}
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(_nameHash);
			bw.Write(_classNameHash);
			bw.Write(_parentHash);
			bw.Write(_countOpt1);
			bw.Write(_num1);
			bw.Write(_countOpt2);
#if CARBON
			bw.Write((short)_countTypes);
			bw.Write(_countTypesTotal);
#else
			bw.Write(_countTypes);
#endif
			bw.Write(0xEFFECADD);
			for(int i=0; i<_countTypes; i++)
			{
				bw.Write(_typeHashes[i]);
			}
#if CARBON
			for(int i=_countTypes; i<_countTypesTotal; i++) {
				bw.Write((int)0);
			}
#endif
			for(int i=0; i<_countOpt1; i++)
			{
				_optionalData[i].Write(bw);
			}		
		}
	}
	public enum VLTExpressionType : uint
	{
		DatabaseLoadData = 0xcbbc628f,
		ClassLoadData = 0x5e970cbc,
		CollectionLoadData = 0x8e112eb7,
	}
	public class VLTExpressionBlock : IFileAccess
	{
		uint _id;
		VLTExpressionType _expressionType;
#if !CARBON
		int _zero;
#endif
		int _length;
		int _offset;
		VLTDataBlock _data;

		public uint Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public VLTExpressionType ExpressionType
		{
			get { return _expressionType; }
			set { _expressionType = value; }
		}

		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}

		public int Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		public VLTDataBlock Data
		{
			get { return _data; }
			set { _data = value; }
		}

		public void ReadData(BinaryReader br)
		{
			br.BaseStream.Seek(_offset, SeekOrigin.Begin);
			_data = VLTDataBlock.CreateOfType(ExpressionType);
			_data.Address = _offset;
			_data.Expression = this;
			_data.Read(br);
		}

		public void WriteData(BinaryWriter bw)
		{
			bw.BaseStream.Seek(_offset, SeekOrigin.Begin);
			_data.Write(bw);
		}

		#region IFileAccess Members

		public void Read(BinaryReader br)
		{
			_id = br.ReadUInt32();
			_expressionType = (VLTExpressionType)br.ReadUInt32();
#if !CARBON
			_zero = br.ReadInt32();
#endif
			_length = br.ReadInt32();
			_offset = br.ReadInt32();

#if CARBON
			if (_expressionType == VLTExpressionType.ClassLoadData)
			{
				Debug.Assert(_length == 0x20, "sizeof(ClassLoadDataExpression) not 0x20");
			}
#endif
		}

		public void Write(BinaryWriter bw)
		{
			bw.Write(_id);
			bw.Write((uint)_expressionType);
#if !CARBON
			bw.Write(_zero);
#endif
			bw.Write(_length);
			bw.Write(_offset);
		}

		#endregion
	}
	public class VLTExpression : VLTBase, IEnumerable
	{
		VLTExpressionBlock[] _blocks;

		public VLTExpressionBlock this[int index]
		{
			get { return _blocks[index]; }
			set { _blocks[index] = value; }
		}

		public int Count
		{
			get { return _blocks.Length; }
		}

		public override void Read(BinaryReader br)
		{
			int count = br.ReadInt32();
			_blocks = new VLTExpressionBlock[count];
			for(int i=0; i<count; i++)
			{
				_blocks[i] = new VLTExpressionBlock();
				_blocks[i].Read(br);
			}
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write((int)_blocks.Length);
            for(int i=0; i<_blocks.Length; i++)
				_blocks[i].Write(bw);
		}
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _blocks.GetEnumerator();
		}

		#endregion
	}
	public class VLTPointerBlock : IFileAccess
	{
		public enum BlockType
		{
			Done = 0,
			RuntimeLink = 1,
			Switch = 2,
			Load = 3,
		}

		int _offsetSource;
		short _blockType, _identifier;
		int _offsetDest;

		public int OffsetSource
		{
			get { return _offsetSource; }
			set { _offsetSource = value; }
		}

		public BlockType Type
		{
			get { return (BlockType)_blockType; }
			set { _blockType = (short)value; }
		}
		
		public short Identifier
		{
			get { return _identifier; }
			set { _identifier = value; }
		}

		public int OffsetDest
		{
			get { return _offsetDest; }
			set { _offsetDest = value; }
		}

		#region IFileAccess Members

		public void Read(BinaryReader br)
		{
			_offsetSource = br.ReadInt32();
			_blockType = br.ReadInt16();
			_identifier = br.ReadInt16();
			_offsetDest = br.ReadInt32();
		}

		public void Write(BinaryWriter bw)
		{
			bw.Write(_offsetSource);
			bw.Write(_blockType);
			bw.Write(_identifier);
			bw.Write(_offsetDest);
		}

		#endregion

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			switch((BlockType)_blockType)
			{
				case BlockType.Done:
					sb.Append("Done");
					break;
				case BlockType.RuntimeLink:
					sb.Append("RunL");
					break;
				case BlockType.Load:
					sb.Append("Load");
					break;
				case BlockType.Switch:
					sb.Append("SwiS");
					break;
			}
			sb.AppendFormat("\tId={0}\tFrom={1:x}\tTo:{2:x}", _identifier, _offsetSource, _offsetDest);
			return sb.ToString();
		}

	}

	public class VLTPointers : VLTBase
	{
		ArrayList _allBlocks;
		Hashtable _vltBlocks;
		ArrayList _rawBlocks;

		public VLTPointerBlock this[int offset]
		{
			get { return _vltBlocks[offset] as VLTPointerBlock; }
			set { _vltBlocks[offset] = value; }
		}

		public void ResolveRawPointers(Stream stream)
		{
			BinaryWriter bw = new BinaryWriter(stream);
			foreach(VLTPointerBlock bk in _rawBlocks)
			{
				bw.BaseStream.Seek(bk.OffsetSource, SeekOrigin.Begin);
				bw.Write(bk.OffsetDest);
			}
		}

		public override void Read(BinaryReader br)
		{
			_allBlocks = new ArrayList();
			_vltBlocks = new Hashtable();
			_rawBlocks = new ArrayList();
			bool loadVlt = false;
			bool loadRaw = false;
			while(true)
			{
				VLTPointerBlock bk = new VLTPointerBlock();
				bk.Read(br);
				if (bk.Type != VLTPointerBlock.BlockType.Load)
				{
					Debug.Write(string.Format("{0:x}\t", br.BaseStream.Position-0xC));
					Debug.WriteLine(bk.ToString());					
				}
				_allBlocks.Add(bk);
				if (bk.Type == VLTPointerBlock.BlockType.Switch && (bk.Identifier == 0 || bk.Identifier == 1))
				{
					if (bk.Identifier == 1)
					{
						loadVlt = false;
						loadRaw = true;
					} 
					else if (bk.Identifier == 0) 
					{
						loadVlt = true;
						loadRaw = false;
					}
				} 
				else if (bk.Type == VLTPointerBlock.BlockType.RuntimeLink)
				{
					// Linked at runtime.
					if (loadVlt)
						_vltBlocks[bk.OffsetSource] = bk;
					if (loadRaw)
						_rawBlocks.Add(bk);
				}
				else if (bk.Type == VLTPointerBlock.BlockType.Load && bk.Identifier == 1)
				{
					if (loadVlt)
						_vltBlocks[bk.OffsetSource] = bk;
					if (loadRaw)
						_rawBlocks.Add(bk);
				} 
				else if (bk.Type == VLTPointerBlock.BlockType.Done)
				{
					break;
				}
				else
				{
					throw new Exception("Unknown ptr type.");
				}
			}
		}

		public override void Write(BinaryWriter bw)
		{
			foreach(VLTPointerBlock bk in _allBlocks)
				bk.Write(bw);
		}
	}

	public class VLTFile
	{
		string _filename;
		string _baseDir;
		ArrayList _chunks;
		MemoryStream _binRaw;
		MemoryStream _binVlt;

		private void WriteChunk(BinaryWriter bw, VLTBase vltbase)
		{
			long position = bw.BaseStream.Position;
			bw.BaseStream.Seek(8, SeekOrigin.Current);
			vltbase.Write(bw);
			long endPosition = bw.BaseStream.Position;
			if ((endPosition % 0x10) != 0)
				endPosition += 0x10 - (endPosition % 0x10);
			vltbase.Chunk.Length = (int)(endPosition - position);
			bw.BaseStream.Seek(position, SeekOrigin.Begin);
			vltbase.Chunk.Write(bw);
            bw.BaseStream.Seek(endPosition, SeekOrigin.Begin);
		}

		private VLTBase ReadChunk(BinaryReader br)
		{

			if (br.BaseStream.Position == br.BaseStream.Length)
				return null;

			VLTChunk chunk = new VLTChunk();
			chunk.Read(br);

			if (chunk.IsValid)
			{
				VLTBase vltbase = null;
				switch(chunk.ChunkId)
				{
					case VLTChunkId.Dependency:
						vltbase = new VLTDependency();
						break;
					case VLTChunkId.Strings:
						vltbase = new VLTRaw();
						break;
					case VLTChunkId.Data:
						vltbase = new VLTRaw();
						break;
					case VLTChunkId.Expression:
						vltbase = new VLTExpression();
						break;
					case VLTChunkId.Pointers:
						vltbase = new VLTPointers();
						break;
					default:
						vltbase = new VLTRaw();
						break;
				}				
				vltbase.Chunk = chunk;
				vltbase.Read(br);
				chunk.SkipChunk(br.BaseStream);
				return vltbase;
			}  
			else
			{
				return null;
			}

		}

		public Stream RawStream
		{
			get { return _binRaw; }
		}

		public Stream VltStream
		{
			get { return _binVlt; }
		}

		public VLTBase GetChunk(VLTChunkId id)
		{
			foreach(VLTBase chunk in _chunks)
				if (chunk.Chunk.ChunkId == id)
					return chunk;
			return null;
		}

		public VLTFile()
		{
		}

		public void Open(string vltFilename)
		{
			FileInfo fi = new FileInfo(vltFilename);
			_baseDir = fi.Directory.FullName;
			_filename = vltFilename;

			FileStream fs = new FileStream(vltFilename, FileMode.Open, FileAccess.Read);
			Open(fs, null);
			fs.Close();
		}

		public void Open(Stream vlt, Stream bin)
		{
			byte[] data = new byte[vlt.Length];
			vlt.Read(data, 0, data.Length);
			_binVlt = new MemoryStream(data);
			
			_chunks = new ArrayList();
			BinaryReader br = new BinaryReader(_binVlt);

			// Load up the chunks
			VLTBase chunk;
			do
			{
				chunk = ReadChunk(br);
				if (chunk != null)
				{
					_chunks.Add(chunk);
				}
			} while (chunk != null);

			// Load up expression data
			VLTExpression expChunk = GetChunk(VLTChunkId.Expression) as VLTExpression;

			for(int i=0; i<expChunk.Count; i++)
			{
				VLTExpressionBlock block = expChunk[i];
				block.ReadData(br);
			}

			// Load up raw bin data
			if (bin == null)
			{
				DirectoryInfo di = new DirectoryInfo(_baseDir);
				VLTDependency dep = GetChunk(VLTChunkId.Dependency) as VLTDependency;
				string binName = dep.GetName(VLTDependency.BinFile);
				FileInfo[] fi = di.GetFiles(binName);
                if (fi.Length == 0)
                	throw new Exception("Required file " + binName + " was not found.");
				bin = new FileStream(fi[0].FullName, FileMode.Open, FileAccess.Read);
				data = new byte[bin.Length];
				bin.Read(data, 0, data.Length);
				bin.Close();
			} 
			else
			{
				data = new byte[bin.Length];
				bin.Read(data, 0, data.Length);
			}
			
			_binRaw = new MemoryStream(data);

			br = new BinaryReader(_binRaw);
			
			_binRaw.Seek(0, SeekOrigin.Begin);
			chunk = ReadChunk(br);
			chunk.Chunk.GotoStart(_binRaw);
			if (chunk.Chunk.ChunkId == VLTChunkId.StringsRaw)
			{
				int endPos = (int)_binRaw.Position + chunk.Chunk.DataLength;
				while(_binRaw.Position < endPos)
				{
					string str = NullTerminatedString.Read(br);
					if (str != "")
					{
						HashResolver.AddAuto(str);
					}
				}
			}

			VLTPointers ptrChunk = GetChunk(VLTChunkId.Pointers) as VLTPointers;
			ptrChunk.ResolveRawPointers(_binRaw);
			
		}

		

	}
}
