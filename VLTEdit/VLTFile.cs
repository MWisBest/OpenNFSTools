using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NFSTools.VLTEdit
{
	public interface IFileAccess
	{
		void Read( BinaryReader br );
		void Write( BinaryWriter bw );
	}

	public interface IAddressable
	{
		long Address
		{
			get; set;
		}
	}

	public class NullTerminatedString
	{
		public static string Read( BinaryReader br )
		{
			StringBuilder sb = new StringBuilder();
			byte b;
			do
			{
				b = br.ReadByte();
				if( b != 0 )
				{
					sb.Append( (char)b );
				}
			} while( b != 0 );
			return sb.ToString();
		}
		public static void Write( BinaryWriter bw, string value )
		{
			byte[] str = Encoding.ASCII.GetBytes( value );
			bw.Write( str );
			bw.Write( (byte)0 );
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
		long _offset;

		public VLTChunkId ChunkId { get; set; }

		public int Length { get; set; }

		public int DataLength
		{
			get
			{
				return this.Length - 0x8;
			}
			set
			{
				this.Length = value + 0x8;
			}
		}

		public bool IsValid
		{
			get
			{
				return this.Length >= 0x8;
			}
		}

		public void GotoStart( Stream stream )
		{
			stream.Seek( this._offset + 0x8, SeekOrigin.Begin );
		}

		public void SkipChunk( Stream stream )
		{
			stream.Seek( this._offset + this.Length, SeekOrigin.Begin );
		}

		#region IFileAccess Members

		public void Read( BinaryReader br )
		{
			this._offset = br.BaseStream.Position;
			this.ChunkId = (VLTChunkId)br.ReadInt32();
			this.Length = br.ReadInt32();
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( (int)this.ChunkId );
			bw.Write( this.Length );
		}

		#endregion
	}

	public abstract class VLTBase : IFileAccess
	{
		protected VLTChunk _chunk;
		public VLTChunk Chunk
		{
			get
			{
				return this._chunk;
			}
			set
			{
				this._chunk = value;
			}
		}

		public abstract void Read( BinaryReader br );
		public abstract void Write( BinaryWriter bw );
	}

	public class VLTDependency : VLTBase
	{
		public const int VltFile = 0;
		public const int BinFile = 1;

		int _count;
		uint[] _hashes;
		string[] _names;

		public uint GetHash( int index )
		{
			return this._hashes[index];
		}

		public string GetName( int index )
		{
			return this._names[index];
		}

		public override void Read( BinaryReader br )
		{
			int[] offsets;
			long position;

			this._count = br.ReadInt32();
			this._hashes = new uint[this._count];
			this._names = new string[this._count];
			offsets = new int[this._count];
			for( int i = 0; i < this._count; i++ )
			{
				this._hashes[i] = br.ReadUInt32();
			}

			for( int i = 0; i < this._count; i++ )
			{
				offsets[i] = br.ReadInt32();
			}

			position = br.BaseStream.Position;
			for( int i = 0; i < this._count; i++ )
			{
				br.BaseStream.Seek( position + offsets[i], SeekOrigin.Begin );
				this._names[i] = NullTerminatedString.Read( br );
			}
		}

		public override void Write( BinaryWriter bw )
		{
			int length = 0;
			bw.Write( this._count );
			for( int i = 0; i < this._count; i++ )
			{
				bw.Write( this._hashes[i] );
			}

			for( int i = 0; i < this._count; i++ )
			{
				bw.Write( length );
				length += this._names[i].Length + 1;
			}
			for( int i = 0; i < this._count; i++ )
			{
				NullTerminatedString.Write( bw, this._names[i] );
			}
		}

	}

	public class VLTRaw : VLTBase
	{
		public byte[] Data { get; set; }

		public Stream GetStream()
		{
			return new MemoryStream( this.Data );
		}

		public override void Read( BinaryReader br )
		{
			this.Data = br.ReadBytes( this._chunk.DataLength );
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Data );
		}
	}
	public abstract class VLTDataBlock : IAddressable, IFileAccess
	{
		protected VLTExpressionBlock _expression;
		protected long _address;

		public abstract void Read( BinaryReader br );
		public abstract void Write( BinaryWriter bw );

		public long Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public VLTExpressionBlock Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				this._expression = value;
			}
		}

		public static VLTDataBlock CreateOfType( VLTExpressionType type )
		{
			switch( type )
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

		public int this[int index]
		{
			get
			{
				return this._sizes[index];
			}
			set
			{
				this._sizes[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._count;
			}
		}

		public int Num1
		{
			get
			{
				return this._num1;
			}
			set
			{
				this._num1 = value;
			}
		}

		public int Num2
		{
			get
			{
				return this._num2;
			}
			set
			{
				this._num2 = value;
			}
		}

		/// <summary>
		/// Gets the pointer to a string array of types
		/// </summary>
		public int Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		public override void Read( BinaryReader br )
		{
			this._num1 = br.ReadInt32();
			this._num2 = br.ReadInt32();
			this._count = br.ReadInt32();
			this._pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			this._sizes = new int[this._count];
			for( int i = 0; i < this._count; i++ )
			{
				this._sizes[i] = br.ReadInt32();
			}
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this._num1 );
			bw.Write( this._num2 );
			bw.Write( this._count );
			bw.Write( 0xEFFECADD );
			for( int i = 0; i < this._count; i++ )
			{
				bw.Write( this._sizes[i] );
			}
		}
	}
	public class VLTDataClassLoad : VLTDataBlock
	{
		int _zero1, _zero2;

		public uint NameHash { get; private set; }

		public int CollectionCount { get; set; }

		public int Num2 { get; set; }

		public int TotalFieldsCount { get; set; }

		public int RequiredFieldsCount { get; set; }

		public int Pointer { get; private set; }

		public override void Read( BinaryReader br )
		{
			this.NameHash = br.ReadUInt32();
			this.CollectionCount = br.ReadInt32();
			this.TotalFieldsCount = br.ReadInt32();

			this.Pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			this.Num2 = br.ReadInt32();
			this._zero1 = br.ReadInt32();
			this.RequiredFieldsCount = br.ReadInt32();
			this._zero2 = br.ReadInt32();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.NameHash );
			bw.Write( this.CollectionCount );
			bw.Write( this.TotalFieldsCount );
			bw.Write( 0xEFFECADD );
			bw.Write( this.Num2 );
			bw.Write( this._zero1 );
			bw.Write( this.RequiredFieldsCount );
			bw.Write( this._zero2 );
		}
	}
	public class VLTDataCollectionLoad : VLTDataBlock
	{
		public class OptionalData : IFileAccess
		{
			public uint NameHash { get; private set; }

			public int Pointer { get; private set; }

			public short Flags1 { get; private set; }

			public short Flags2 { get; private set; }

			public bool IsDataEmbedded
			{
				get
				{
					return ( this.Flags2 & 0x20 ) != 0;
				}
			}

			#region IFileAccess Members

			public void Read( BinaryReader br )
			{
				this.NameHash = br.ReadUInt32();
				this.Pointer = (int)br.BaseStream.Position;
				br.ReadInt32();
				this.Flags1 = br.ReadInt16();
				this.Flags2 = br.ReadInt16();
			}

			public void Write( BinaryWriter bw )
			{
				bw.Write( this.NameHash );
				bw.Write( 0xEFFECADD ); // beware, sometimes this is a ptr, sometimes this is data
										// recommend writing collload block before writing data
				bw.Write( this.Flags1 );
				bw.Write( this.Flags2 );
			}

			#endregion
		}

		int _countOpt2;
		uint[] _typeHashes;
		OptionalData[] _optionalData;
#if CARBON
		short _countTypesTotal;
#endif

		public uint NameHash { get; private set; }

		public uint ClassNameHash { get; private set; }

		public uint ParentHash { get; private set; }

		public int Count { get; private set; }

		public int CountOptional { get; private set; }

		public int Pointer { get; private set; }

		/*
		public uint this[int index]
		{
			get { return  _typeHashes[index]; }
		}
		*/

		public OptionalData this[int index]
		{
			get
			{
				return this._optionalData[index];
			}
		}

		public int Num1 { get; private set; }

		public override void Read( BinaryReader br )
		{
			this.NameHash = br.ReadUInt32();
			this.ClassNameHash = br.ReadUInt32();
			this.ParentHash = br.ReadUInt32();
			this.CountOptional = br.ReadInt32();
			this.Num1 = br.ReadInt32();            // not always 0, 0xc in one case
			this._countOpt2 = br.ReadInt32();

			Debug.Assert( this.CountOptional == this._countOpt2, "CountOpt1 not equal to CountOpt2" );

#if CARBON
			this.Count = br.ReadInt16();
			this._countTypesTotal = br.ReadInt16();
#else
			_countTypes = br.ReadInt32();
#endif

			this.Pointer = (int)br.BaseStream.Position;
			br.ReadInt32();
			this._typeHashes = new uint[this.Count];
			for( int i = 0; i < this.Count; i++ )
			{
				this._typeHashes[i] = br.ReadUInt32();
			}
#if CARBON
			for( int i = this.Count; i < this._countTypesTotal; i++ )
			{
				br.ReadInt32();
			}
#endif
			this._optionalData = new OptionalData[this.CountOptional];
			for( int i = 0; i < this.CountOptional; i++ )
			{
				this._optionalData[i] = new OptionalData();
				this._optionalData[i].Read( br );
			}
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.NameHash );
			bw.Write( this.ClassNameHash );
			bw.Write( this.ParentHash );
			bw.Write( this.CountOptional );
			bw.Write( this.Num1 );
			bw.Write( this._countOpt2 );
#if CARBON
			bw.Write( (short)this.Count );
			bw.Write( this._countTypesTotal );
#else
			bw.Write(_countTypes);
#endif
			bw.Write( 0xEFFECADD );
			for( int i = 0; i < this.Count; i++ )
			{
				bw.Write( this._typeHashes[i] );
			}
#if CARBON
			for( int i = this.Count; i < this._countTypesTotal; i++ )
			{
				bw.Write( (int)0 );
			}
#endif
			for( int i = 0; i < this.CountOptional; i++ )
			{
				this._optionalData[i].Write( bw );
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
		public uint Id { get; set; }

		public VLTExpressionType ExpressionType { get; set; }

		public int Length { get; set; }

		public int Offset { get; set; }

		public VLTDataBlock Data { get; set; }

		public void ReadData( BinaryReader br )
		{
			br.BaseStream.Seek( this.Offset, SeekOrigin.Begin );
			this.Data = VLTDataBlock.CreateOfType( this.ExpressionType );
			this.Data.Address = this.Offset;
			this.Data.Expression = this;
			this.Data.Read( br );
		}

		public void WriteData( BinaryWriter bw )
		{
			bw.BaseStream.Seek( this.Offset, SeekOrigin.Begin );
			this.Data.Write( bw );
		}

		#region IFileAccess Members

		public void Read( BinaryReader br )
		{
			this.Id = br.ReadUInt32();
			this.ExpressionType = (VLTExpressionType)br.ReadUInt32();
#if !CARBON
			_zero = br.ReadInt32();
#endif
			this.Length = br.ReadInt32();
			this.Offset = br.ReadInt32();

#if CARBON
			if( this.ExpressionType == VLTExpressionType.ClassLoadData )
			{
				Debug.Assert( this.Length == 0x20, "sizeof(ClassLoadDataExpression) not 0x20" );
			}
#endif
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( this.Id );
			bw.Write( (uint)this.ExpressionType );
#if !CARBON
			bw.Write(_zero);
#endif
			bw.Write( this.Length );
			bw.Write( this.Offset );
		}

		#endregion
	}
	public class VLTExpression : VLTBase, IEnumerable
	{
		VLTExpressionBlock[] _blocks;

		public VLTExpressionBlock this[int index]
		{
			get
			{
				return this._blocks[index];
			}
			set
			{
				this._blocks[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._blocks.Length;
			}
		}

		public override void Read( BinaryReader br )
		{
			int count = br.ReadInt32();
			this._blocks = new VLTExpressionBlock[count];
			for( int i = 0; i < count; i++ )
			{
				this._blocks[i] = new VLTExpressionBlock();
				this._blocks[i].Read( br );
			}
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( (int)this._blocks.Length );
			for( int i = 0; i < this._blocks.Length; i++ )
			{
				this._blocks[i].Write( bw );
			}
		}
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return this._blocks.GetEnumerator();
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

		short _blockType;

		public int OffsetSource { get; set; }

		public BlockType Type
		{
			get
			{
				return (BlockType)this._blockType;
			}
			set
			{
				this._blockType = (short)value;
			}
		}

		public short Identifier { get; set; }

		public int OffsetDest { get; set; }

		#region IFileAccess Members

		public void Read( BinaryReader br )
		{
			this.OffsetSource = br.ReadInt32();
			this._blockType = br.ReadInt16();
			this.Identifier = br.ReadInt16();
			this.OffsetDest = br.ReadInt32();
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( this.OffsetSource );
			bw.Write( this._blockType );
			bw.Write( this.Identifier );
			bw.Write( this.OffsetDest );
		}

		#endregion

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			switch( (BlockType)this._blockType )
			{
				case BlockType.Done:
					sb.Append( "Done" );
					break;
				case BlockType.RuntimeLink:
					sb.Append( "RunL" );
					break;
				case BlockType.Load:
					sb.Append( "Load" );
					break;
				case BlockType.Switch:
					sb.Append( "SwiS" );
					break;
			}
			sb.AppendFormat( "\tId={0}\tFrom={1:x}\tTo:{2:x}", this.Identifier, this.OffsetSource, this.OffsetDest );
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
			get
			{
				return this._vltBlocks[offset] as VLTPointerBlock;
			}
			set
			{
				this._vltBlocks[offset] = value;
			}
		}

		public void ResolveRawPointers( Stream stream )
		{
			BinaryWriter bw = new BinaryWriter( stream );
			foreach( VLTPointerBlock bk in this._rawBlocks )
			{
				bw.BaseStream.Seek( bk.OffsetSource, SeekOrigin.Begin );
				bw.Write( bk.OffsetDest );
			}
		}

		public override void Read( BinaryReader br )
		{
			this._allBlocks = new ArrayList();
			this._vltBlocks = new Hashtable();
			this._rawBlocks = new ArrayList();
			bool loadVlt = false;
			bool loadRaw = false;
			while( true )
			{
				VLTPointerBlock bk = new VLTPointerBlock();
				bk.Read( br );
				if( bk.Type != VLTPointerBlock.BlockType.Load )
				{
					Debug.Write( string.Format( "{0:x}\t", br.BaseStream.Position - 0xC ) );
					Debug.WriteLine( bk.ToString() );
				}
				this._allBlocks.Add( bk );
				if( bk.Type == VLTPointerBlock.BlockType.Switch && ( bk.Identifier == 0 || bk.Identifier == 1 ) )
				{
					if( bk.Identifier == 1 )
					{
						loadVlt = false;
						loadRaw = true;
					}
					else if( bk.Identifier == 0 )
					{
						loadVlt = true;
						loadRaw = false;
					}
				}
				else if( bk.Type == VLTPointerBlock.BlockType.RuntimeLink )
				{
					// Linked at runtime.
					if( loadVlt )
					{
						this._vltBlocks[bk.OffsetSource] = bk;
					}

					if( loadRaw )
					{
						this._rawBlocks.Add( bk );
					}
				}
				else if( bk.Type == VLTPointerBlock.BlockType.Load && bk.Identifier == 1 )
				{
					if( loadVlt )
					{
						this._vltBlocks[bk.OffsetSource] = bk;
					}

					if( loadRaw )
					{
						this._rawBlocks.Add( bk );
					}
				}
				else if( bk.Type == VLTPointerBlock.BlockType.Done )
				{
					break;
				}
				else
				{
					throw new Exception( "Unknown ptr type." );
				}
			}
		}

		public override void Write( BinaryWriter bw )
		{
			foreach( VLTPointerBlock bk in this._allBlocks )
			{
				bk.Write( bw );
			}
		}
	}

	public class VLTFile
	{
		string _filename;
		string _baseDir;
		ArrayList _chunks;
		MemoryStream _binRaw;
		MemoryStream _binVlt;

		private void WriteChunk( BinaryWriter bw, VLTBase vltbase )
		{
			long position = bw.BaseStream.Position;
			bw.BaseStream.Seek( 8, SeekOrigin.Current );
			vltbase.Write( bw );
			long endPosition = bw.BaseStream.Position;
			if( ( endPosition % 0x10 ) != 0 )
			{
				endPosition += 0x10 - ( endPosition % 0x10 );
			}

			vltbase.Chunk.Length = (int)( endPosition - position );
			bw.BaseStream.Seek( position, SeekOrigin.Begin );
			vltbase.Chunk.Write( bw );
			bw.BaseStream.Seek( endPosition, SeekOrigin.Begin );
		}

		private VLTBase ReadChunk( BinaryReader br )
		{

			if( br.BaseStream.Position == br.BaseStream.Length )
			{
				return null;
			}

			VLTChunk chunk = new VLTChunk();
			chunk.Read( br );

			if( chunk.IsValid )
			{
				VLTBase vltbase = null;
				switch( chunk.ChunkId )
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
				vltbase.Read( br );
				chunk.SkipChunk( br.BaseStream );
				return vltbase;
			}
			else
			{
				return null;
			}

		}

		public Stream RawStream
		{
			get
			{
				return this._binRaw;
			}
		}

		public Stream VltStream
		{
			get
			{
				return this._binVlt;
			}
		}

		public VLTBase GetChunk( VLTChunkId id )
		{
			foreach( VLTBase chunk in this._chunks )
			{
				if( chunk.Chunk.ChunkId == id )
				{
					return chunk;
				}
			}

			return null;
		}

		public VLTFile()
		{
		}

		public void Open( string vltFilename )
		{
			FileInfo fi = new FileInfo( vltFilename );
			this._baseDir = fi.Directory.FullName;
			this._filename = vltFilename;

			FileStream fs = new FileStream( vltFilename, FileMode.Open, FileAccess.Read );
			this.Open( fs, null );
			fs.Close();
		}

		public void Open( Stream vlt, Stream bin )
		{
			byte[] data = new byte[vlt.Length];
			vlt.Read( data, 0, data.Length );
			this._binVlt = new MemoryStream( data );

			this._chunks = new ArrayList();
			BinaryReader br = new BinaryReader( this._binVlt );

			// Load up the chunks
			VLTBase chunk;
			do
			{
				chunk = this.ReadChunk( br );
				if( chunk != null )
				{
					this._chunks.Add( chunk );
				}
			} while( chunk != null );

			// Load up expression data
			VLTExpression expChunk = this.GetChunk( VLTChunkId.Expression ) as VLTExpression;

			for( int i = 0; i < expChunk.Count; i++ )
			{
				VLTExpressionBlock block = expChunk[i];
				block.ReadData( br );
			}

			// Load up raw bin data
			if( bin == null )
			{
				DirectoryInfo di = new DirectoryInfo( this._baseDir );
				VLTDependency dep = this.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string binName = dep.GetName( VLTDependency.BinFile );
				FileInfo[] fi = di.GetFiles( binName );
				if( fi.Length == 0 )
				{
					throw new Exception( "Required file " + binName + " was not found." );
				}

				bin = new FileStream( fi[0].FullName, FileMode.Open, FileAccess.Read );
				data = new byte[bin.Length];
				bin.Read( data, 0, data.Length );
				bin.Close();
			}
			else
			{
				data = new byte[bin.Length];
				bin.Read( data, 0, data.Length );
			}

			this._binRaw = new MemoryStream( data );

			br = new BinaryReader( this._binRaw );

			this._binRaw.Seek( 0, SeekOrigin.Begin );
			chunk = this.ReadChunk( br );
			chunk.Chunk.GotoStart( this._binRaw );
			if( chunk.Chunk.ChunkId == VLTChunkId.StringsRaw )
			{
				int endPos = (int)this._binRaw.Position + chunk.Chunk.DataLength;
				while( this._binRaw.Position < endPos )
				{
					string str = NullTerminatedString.Read( br );
					if( str != "" )
					{
						HashResolver.AddAuto( str );
					}
				}
			}

			VLTPointers ptrChunk = this.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
			ptrChunk.ResolveRawPointers( this._binRaw );
		}
	}
}
