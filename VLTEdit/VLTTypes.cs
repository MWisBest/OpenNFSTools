using System.IO;

namespace vltedit.VLTDataItems
{


	public class Unknown : VLTDataItem
	{
		[DataValue( "Length", Hex = true )]
		public int Length;
		[DataValue( "Data" )]
		public string DataHex;

		private byte[] Data;


		public void SetLength( int length )
		{
			this.Length = length;
		}
		public override void Read( BinaryReader br )
		{
			this.Data = br.ReadBytes( this.Length );
			this.DataHex = "";
			if( this.Length <= 0x20 )
			{
				foreach( byte d in this.Data )
				{
					this.DataHex += string.Format( "{0:x}", d ).PadLeft( 2, '0' ) + " ";
				}
			}
		}
		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Data );
		}


	}


	public class UpgradeSpecs : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue( "Class" )]
		public string Class;
		[DataValue( "Collection" )]
		public string RowName;
		[DataValue( "Level" )]
		public uint Level;

		public override void Read( BinaryReader br )
		{
			this.ClassHash = br.ReadUInt32();
			this.RowNameHash = br.ReadUInt32();
			br.ReadInt32();
			this.Level = br.ReadUInt32();

			this.Class = HashResolver.Resolve( this.ClassHash );
			this.RowName = HashResolver.Resolve( this.RowNameHash );

		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( VLTHasher.Hash( this.Class ) );
			bw.Write( VLTHasher.Hash( this.RowName ) );
			bw.Write( 0 );
			bw.Write( this.Level );
		}
	}

	public class JunkmanMod : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue( "Class" )]
		public string Class;
		[DataValue( "Collection" )]
		public string RowName;
		[DataValue( "Factor" )]
		public float Factor;

		public override void Read( BinaryReader br )
		{
			this.ClassHash = br.ReadUInt32();
			this.RowNameHash = br.ReadUInt32();
			this.Factor = br.ReadSingle();

			this.Class = HashResolver.Resolve( this.ClassHash );
			this.RowName = HashResolver.Resolve( this.RowNameHash );

		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( VLTHasher.Hash( this.Class ) );
			bw.Write( VLTHasher.Hash( this.RowName ) );
			bw.Write( 0 );
			bw.Write( this.Factor );
		}
	}

	public class RefSpec : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue( "Class" )]
		public string Class;
		[DataValue( "Collection" )]
		public string RowName;

		public override void Read( BinaryReader br )
		{
			this.ClassHash = br.ReadUInt32();
			this.RowNameHash = br.ReadUInt32();
			br.ReadInt32();

			this.Class = HashResolver.Resolve( this.ClassHash );
			this.RowName = HashResolver.Resolve( this.RowNameHash );

		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( VLTHasher.Hash( this.Class ) );
			bw.Write( VLTHasher.Hash( this.RowName ) );
			bw.Write( 0 );
		}

		public override string ToString()
		{
			return this.Class + " -> " + this.RowName;
		}

	}

	public class GCollectionKey : VLTDataItem
	{
		[DataValue( "Hash" )]
		public uint Hash;
		[DataValue( "Value" )]
		public string Value;

		public override void Read( BinaryReader br )
		{
			this.Hash = br.ReadUInt32();
			this.Value = HashResolver.Resolve( this.Hash );

		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( VLTHasher.Hash( this.Value ) );
		}

		public override string ToString()
		{
			return this.Value;
		}

	}

	public class Blob : VLTDataItem
	{
		[DataValue( "Length", Hex = true )]
		public uint DataLength;
		[DataValue( "Offset", Hex = true )]
		public uint DataOffset;

		public override void Read( BinaryReader br )
		{
			this.DataLength = br.ReadUInt32();
			this.DataOffset = br.ReadUInt32();

		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.DataLength );
			bw.Write( this.DataOffset );
		}
	}

	public class StringKey : VLTDataItem
	{
		[DataValue( "Hash64", Hex = true )]
		public ulong Hash64;
		[DataValue( "Hash32", Hex = true )]
		public uint Hash;
		[DataValue( "Offset", Hex = true )]
		public uint StringOffset;
		[DataValue( "Value" )]
		public string Value;

		public override void Read( BinaryReader br )
		{
			this.Hash64 = br.ReadUInt64();
			this.Hash = br.ReadUInt32();
			this.StringOffset = br.ReadUInt32();
			if( this.StringOffset == 0 )
			{
				this.Value = "(null)";
			}
			else
			{
				if( this.StringOffset > br.BaseStream.Length )
				{
					this.Value = "(offset is outta here)";
				}
				else
				{
					long position = br.BaseStream.Position;
					br.BaseStream.Seek( this.StringOffset, SeekOrigin.Begin );
					this.Value = NullTerminatedString.Read( br );
					br.BaseStream.Seek( position, SeekOrigin.Begin );
				}
			}
		}

		public override void Write( BinaryWriter bw )
		{
			//Hash64 = VLTHasher.Hash64(Value);
			//Hash = VLTHasher.Hash(Value);

			if( this.StringOffset == 0 )
			{
				bw.Write( (ulong)0 );
				bw.Write( (uint)0 );
			}
			else
			{
				bw.Write( this.Hash64 );
				bw.Write( this.Hash );
			}
			bw.Write( this.StringOffset );
		}

		public override string ToString()
		{
			return this.Value;
		}

	}

#if CARBON
	public class RuntimeLinkage : VLTDataItem
	{
		[DataValue( "Info" )]
		public string Value;

		public RuntimeLinkage() : base()
		{
			this.Value = "Runtime Linkage";
		}

		public override void Read( BinaryReader br )
		{
			// do nothing
		}

		public override void Write( BinaryWriter bw )
		{
			// do nothing
		}

		public override string ToString()
		{
			return this.Value;
		}

	}
#endif

	public class Text : VLTDataItem
	{
		[DataValue( "Offset", Hex = true )]
		public uint StringOffset;
		[DataValue( "Value" )]
		public string Value;

		public override void Read( BinaryReader br )
		{
#if NEVEREVALUATESTOTRUE
			StringOffset = Offset;
			Value = NullTerminatedString.Read(br);			
#else
			this.StringOffset = br.ReadUInt32();
			if( this.StringOffset > br.BaseStream.Length )
			{
				this.StringOffset = this.Offset;
			}
			if( this.StringOffset == 0 )
			{
				this.Value = "(null)";
			}
			else
			{
				long position = br.BaseStream.Position;
				br.BaseStream.Seek( this.StringOffset, SeekOrigin.Begin );
				this.Value = NullTerminatedString.Read( br );
				br.BaseStream.Seek( position, SeekOrigin.Begin );
			}
#endif
		}

		public override void Write( BinaryWriter bw )
		{
#if NEVEREVALUATESTOTRUE
			// nauhh
#else
			bw.Write( this.StringOffset );
#endif
		}

		public override string ToString()
		{
			return this.Value;
		}


	}

	public class Vector2 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.X, this.Y );
		}

	}
	public class Vector3 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;
		[DataValue( "Z" )]
		public float Z;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
			this.Z = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
			bw.Write( this.Z );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}", this.X, this.Y, this.Z );
		}
	}
	public class Vector4 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;
		[DataValue( "Z" )]
		public float Z;
		[DataValue( "W" )]
		public float W;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
			this.Z = br.ReadSingle();
			this.W = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
			bw.Write( this.Z );
			bw.Write( this.W );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}, {3}", this.X, this.Y, this.Z, this.W );
		}

	}
	public class Matrix : VLTDataItem
	{
		[DataValue( "M[1,1]" )]
		public float M11;
		[DataValue( "M[1,2]" )]
		public float M12;
		[DataValue( "M[1,3]" )]
		public float M13;
		[DataValue( "M[1,4]" )]
		public float M14;
		[DataValue( "M[2,1]" )]
		public float M21;
		[DataValue( "M[2,2]" )]
		public float M22;
		[DataValue( "M[2,3]" )]
		public float M23;
		[DataValue( "M[2,4]" )]
		public float M24;
		[DataValue( "M[3,1]" )]
		public float M31;
		[DataValue( "M[3,2]" )]
		public float M32;
		[DataValue( "M[3,3]" )]
		public float M33;
		[DataValue( "M[3,4]" )]
		public float M34;
		[DataValue( "M[4,1]" )]
		public float M41;
		[DataValue( "M[4,2]" )]
		public float M42;
		[DataValue( "M[4,3]" )]
		public float M43;
		[DataValue( "M[4,4]" )]
		public float M44;
		public override void Read( BinaryReader br )
		{
			this.M11 = br.ReadSingle();
			this.M12 = br.ReadSingle();
			this.M13 = br.ReadSingle();
			this.M14 = br.ReadSingle();
			this.M21 = br.ReadSingle();
			this.M22 = br.ReadSingle();
			this.M23 = br.ReadSingle();
			this.M24 = br.ReadSingle();
			this.M31 = br.ReadSingle();
			this.M32 = br.ReadSingle();
			this.M33 = br.ReadSingle();
			this.M34 = br.ReadSingle();
			this.M41 = br.ReadSingle();
			this.M42 = br.ReadSingle();
			this.M43 = br.ReadSingle();
			this.M44 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.M11 );
			bw.Write( this.M12 );
			bw.Write( this.M13 );
			bw.Write( this.M14 );
			bw.Write( this.M21 );
			bw.Write( this.M22 );
			bw.Write( this.M23 );
			bw.Write( this.M24 );
			bw.Write( this.M31 );
			bw.Write( this.M32 );
			bw.Write( this.M33 );
			bw.Write( this.M34 );
			bw.Write( this.M41 );
			bw.Write( this.M42 );
			bw.Write( this.M43 );
			bw.Write( this.M44 );
		}
	}
	public class Bool : VLTDataItem
	{
		[DataValue( "Value" )]
		public bool Value;

		public override void Read( BinaryReader br )
		{
			this.Value = ( br.ReadByte() == 1 );
		}

		public override void Write( BinaryWriter bw )
		{
			if( this.Value )
			{
				bw.Write( (byte)1 );
			}
			else
			{
				bw.Write( (byte)0 );
			}
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Double : VLTDataItem
	{
		[DataValue( "Value" )]
		public double Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadDouble();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Float : VLTDataItem
	{
		[DataValue( "Value" )]
		public float Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Int64 : VLTDataItem
	{
		[DataValue( "Value" )]
		public long Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadInt64();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Int32 : VLTDataItem
	{
		[DataValue( "Value" )]
		public int Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadInt32();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Int16 : VLTDataItem
	{
		[DataValue( "Value" )]
		public short Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadInt16();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class Int8 : VLTDataItem
	{
		[DataValue( "Value" )]
		public sbyte Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadSByte();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class UInt64 : VLTDataItem
	{
		[DataValue( "Value" )]
		public ulong Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadUInt64();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class UInt32 : VLTDataItem
	{
		[DataValue( "Value" )]
		public uint Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadUInt32();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class UInt16 : VLTDataItem
	{
		[DataValue( "Value" )]
		public ushort Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadUInt16();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}
	public class UInt8 : VLTDataItem
	{
		[DataValue( "Value" )]
		public byte Value;

		public override void Read( BinaryReader br )
		{
			this.Value = br.ReadByte();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value );
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}
	}


	public class AxlePair : VLTDataItem
	{
		[DataValue( "Front" )]
		public float Value1;
		[DataValue( "Rear" )]
		public float Value2;

		public override void Read( BinaryReader br )
		{
			this.Value1 = br.ReadSingle();
			this.Value2 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value1 );
			bw.Write( this.Value2 );
		}


		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.Value1, this.Value2 );
		}
	}

	public class CarBodyMotion : VLTDataItem
	{
		[DataValue( "Value1" )]
		public float Value1;
		[DataValue( "Value2" )]
		public float Value2;
		[DataValue( "Value3" )]
		public float Value3;

		public override void Read( BinaryReader br )
		{
			this.Value1 = br.ReadSingle();
			this.Value2 = br.ReadSingle();
			this.Value3 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.Value1 );
			bw.Write( this.Value2 );
			bw.Write( this.Value3 );
		}
	}
}

