using System;
using System.IO;

namespace vltedit.VLTDataItems
{

	
	public class Unknown : VLTDataItem 
	{
		[DataValue("Length", Hex=true)]
		public int Length;
		[DataValue("Data")]
		public string DataHex;

		private byte[] Data;


		public void SetLength(int length)
		{
			Length = length;
		}
		public override void Read(BinaryReader br)
		{
			Data = br.ReadBytes(Length);
			DataHex = "";
			if (Length <= 0x20)
			{
				foreach(byte d in Data)
				{
					DataHex += string.Format("{0:x}", d).PadLeft(2, '0') + " ";
				}
			}
		}
		public override void Write(BinaryWriter bw)
		{
			bw.Write(Data);
		}

		
	}
	
	
	public class UpgradeSpecs : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue("Class")]
		public string Class;
		[DataValue("Collection")]
		public string RowName;
		[DataValue("Level")]
		public uint Level;

		public override void Read(BinaryReader br)
		{
			ClassHash = br.ReadUInt32();
			RowNameHash = br.ReadUInt32();
			br.ReadInt32();
			Level = br.ReadUInt32();

			Class = HashResolver.Resolve(ClassHash);
			RowName = HashResolver.Resolve(RowNameHash);

		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(VLTHasher.Hash(Class));
			bw.Write(VLTHasher.Hash(RowName));
			bw.Write(0);
			bw.Write(Level);
		}
	}

	public class JunkmanMod : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue("Class")]
		public string Class;
		[DataValue("Collection")]
		public string RowName;
		[DataValue("Factor")]
		public float Factor;

		public override void Read(BinaryReader br)
		{
			ClassHash = br.ReadUInt32();
			RowNameHash = br.ReadUInt32();
			Factor = br.ReadSingle();

			Class = HashResolver.Resolve(ClassHash);
			RowName = HashResolver.Resolve(RowNameHash);

		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(VLTHasher.Hash(Class));
			bw.Write(VLTHasher.Hash(RowName));
			bw.Write(0);
			bw.Write(Factor);
		}
	}

	public class RefSpec : VLTDataItem
	{
		private uint ClassHash;
		private uint RowNameHash;

		[DataValue("Class")]
		public string Class;
		[DataValue("Collection")]
		public string RowName;

		public override void Read(BinaryReader br)
		{
			ClassHash = br.ReadUInt32();
			RowNameHash = br.ReadUInt32();
			br.ReadInt32();

			Class = HashResolver.Resolve(ClassHash);
			RowName = HashResolver.Resolve(RowNameHash);

		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(VLTHasher.Hash(Class));
			bw.Write(VLTHasher.Hash(RowName));
			bw.Write(0);
		}

		public override string ToString()
		{
			return Class + " -> " + RowName;
		}

	}

	public class GCollectionKey : VLTDataItem
	{
		[DataValue("Hash")]
		public uint Hash;
		[DataValue("Value")]
		public string Value;

		public override void Read(BinaryReader br)
		{
			Hash = br.ReadUInt32();
			Value = HashResolver.Resolve(Hash);

		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(VLTHasher.Hash(Value));
		}

		public override string ToString()
		{
			return Value;
		}

	}

	public class Blob : VLTDataItem
	{
		[DataValue("Length", Hex=true)]
		public uint DataLength;
		[DataValue("Offset", Hex=true)]
		public uint DataOffset;

		public override void Read(BinaryReader br)
		{
			DataLength = br.ReadUInt32();
			DataOffset = br.ReadUInt32();

		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(DataLength);
			bw.Write(DataOffset);
		}
	}

	public class StringKey : VLTDataItem
	{
		[DataValue("Hash64", Hex=true)]
		public ulong Hash64;
		[DataValue("Hash32", Hex=true)]
		public uint Hash;
		[DataValue("Offset", Hex=true)]
		public uint StringOffset;
		[DataValue("Value")]
		public string Value;
			
		public override void Read(BinaryReader br)
		{
			Hash64 = br.ReadUInt64();
			Hash = br.ReadUInt32();
			StringOffset = br.ReadUInt32();
			if (StringOffset == 0)
			{
				Value = "(null)";
			} 
			else
			{	
				if (StringOffset > br.BaseStream.Length)
				{
					Value = "(offset is outta here)";
				}
				else
				{
					long position = br.BaseStream.Position;
					br.BaseStream.Seek(StringOffset, SeekOrigin.Begin);
					Value = NullTerminatedString.Read(br);
					br.BaseStream.Seek(position, SeekOrigin.Begin);					
				}
			}
		}

		public override void Write(BinaryWriter bw)
		{
			//Hash64 = VLTHasher.Hash64(Value);
			//Hash = VLTHasher.Hash(Value);

			if (StringOffset == 0)
			{
				bw.Write((ulong)0);
				bw.Write((uint)0);
			} 
			else
			{
				bw.Write(Hash64);
				bw.Write(Hash);
			}
			bw.Write(StringOffset);
		}

		public override string ToString()
		{
			return Value;
		}

	}

#if CARBON
	public class RuntimeLinkage : VLTDataItem
	{
		[DataValue("Info")]
		public string Value;

		public RuntimeLinkage() : base() 
		{
			Value = "Runtime Linkage";
		}

		public override void Read(BinaryReader br)
		{
			// do nothing
		}

		public override void Write(BinaryWriter bw)
		{
			// do nothing
		}

		public override string ToString()
		{
			return Value;
		}

	}
#endif

	public class Text : VLTDataItem
	{
		[DataValue("Offset", Hex=true)]
		public uint StringOffset;
		[DataValue("Value")]
		public string Value;
			
		public override void Read(BinaryReader br)
		{
#if NEVEREVALUATESTOTRUE
			StringOffset = Offset;
			Value = NullTerminatedString.Read(br);			
#else
			StringOffset = br.ReadUInt32();
			if (StringOffset > br.BaseStream.Length)
			{
				StringOffset = Offset;
			}
			if (StringOffset == 0)
			{
				Value = "(null)";
			} 
			else
			{					
				long position = br.BaseStream.Position;
				br.BaseStream.Seek(StringOffset, SeekOrigin.Begin);
				Value = NullTerminatedString.Read(br);
				br.BaseStream.Seek(position, SeekOrigin.Begin);
			}
#endif
		}

		public override void Write(BinaryWriter bw)
		{
#if NEVEREVALUATESTOTRUE
			// nauhh
#else
			bw.Write(StringOffset);
#endif
		}

		public override string ToString()
		{
			return Value;
		}


	}

	public class Vector2 : VLTDataItem
	{
		[DataValue("X")]
		public float X;
		[DataValue("Y")]
		public float Y;

		public override void Read(BinaryReader br)
		{
			X = br.ReadSingle();
			Y = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(X);
			bw.Write(Y);
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", X, Y);
		}

	}
	public class Vector3 : VLTDataItem
	{
		[DataValue("X")]
		public float X;
		[DataValue("Y")]
		public float Y;
		[DataValue("Z")]
		public float Z;

		public override void Read(BinaryReader br)
		{
			X = br.ReadSingle();
			Y = br.ReadSingle();
			Z = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(X);
			bw.Write(Y);
			bw.Write(Z);
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", X, Y, Z);
		}
	}
	public class Vector4 : VLTDataItem
	{
		[DataValue("X")]
		public float X;
		[DataValue("Y")]
		public float Y;
		[DataValue("Z")]
		public float Z;
		[DataValue("W")]
		public float W;

		public override void Read(BinaryReader br)
		{
			X = br.ReadSingle();
			Y = br.ReadSingle();
			Z = br.ReadSingle();
			W = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(X);
			bw.Write(Y);
			bw.Write(Z);
			bw.Write(W);
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}, {3}", X, Y, Z, W);
		}

	}
	public class Matrix : VLTDataItem
	{
		[DataValue("M[1,1]")]
		public float M11;
		[DataValue("M[1,2]")]
		public float M12;
		[DataValue("M[1,3]")]
		public float M13;
		[DataValue("M[1,4]")]
		public float M14;
		[DataValue("M[2,1]")]
		public float M21;
		[DataValue("M[2,2]")]
		public float M22;
		[DataValue("M[2,3]")]
		public float M23;
		[DataValue("M[2,4]")]
		public float M24;
		[DataValue("M[3,1]")]
		public float M31;
		[DataValue("M[3,2]")]
		public float M32;
		[DataValue("M[3,3]")]
		public float M33;
		[DataValue("M[3,4]")]
		public float M34;
		[DataValue("M[4,1]")]
		public float M41;
		[DataValue("M[4,2]")]
		public float M42;
		[DataValue("M[4,3]")]
		public float M43;
		[DataValue("M[4,4]")]
		public float M44;
		public override void Read(BinaryReader br)
		{
			M11 = br.ReadSingle();
			M12 = br.ReadSingle();
			M13 = br.ReadSingle();
			M14 = br.ReadSingle();
			M21 = br.ReadSingle();
			M22 = br.ReadSingle();
			M23 = br.ReadSingle();
			M24 = br.ReadSingle();
			M31 = br.ReadSingle();
			M32 = br.ReadSingle();
			M33 = br.ReadSingle();
			M34 = br.ReadSingle();
			M41 = br.ReadSingle();
			M42 = br.ReadSingle();
			M43 = br.ReadSingle();
			M44 = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(M11);
			bw.Write(M12);
			bw.Write(M13);
			bw.Write(M14);
			bw.Write(M21);
			bw.Write(M22);
			bw.Write(M23);
			bw.Write(M24);
			bw.Write(M31);
			bw.Write(M32);
			bw.Write(M33);
			bw.Write(M34);
			bw.Write(M41);
			bw.Write(M42);
			bw.Write(M43);
			bw.Write(M44);
		}
	}
	public class Bool : VLTDataItem
	{
		[DataValue("Value")]
		public bool Value;

		public override void Read(BinaryReader br)
		{
			Value = (br.ReadByte() == 1);
		}

		public override void Write(BinaryWriter bw)
		{
			if (Value)
				bw.Write((byte)1);
			else
				bw.Write((byte)0);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class Double : VLTDataItem
	{
		[DataValue("Value")]
		public double Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadDouble();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}

		public override string ToString()
		{
			return Value.ToString();
		}	
	}
	public class Float : VLTDataItem
	{
		[DataValue("Value")]
		public float Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class Int64 : VLTDataItem
	{
		[DataValue("Value")]
		public long Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadInt64();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class Int32 : VLTDataItem
	{
		[DataValue("Value")]
		public int Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadInt32();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class Int16 : VLTDataItem
	{
		[DataValue("Value")]
		public short Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadInt16();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class Int8 : VLTDataItem
	{
		[DataValue("Value")]
		public sbyte Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadSByte();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class UInt64 : VLTDataItem
	{
		[DataValue("Value")]
		public ulong Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadUInt64();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class UInt32 : VLTDataItem
	{
		[DataValue("Value")]
		public uint Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadUInt32();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class UInt16 : VLTDataItem
	{
		[DataValue("Value")]
		public ushort Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadUInt16();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}
	public class UInt8 : VLTDataItem
	{
		[DataValue("Value")]
		public byte Value;

		public override void Read(BinaryReader br)
		{
			Value = br.ReadByte();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value);
		}
		
		public override string ToString()
		{
			return Value.ToString();
		}
	}


	public class AxlePair : VLTDataItem
	{
		[DataValue("Front")]
		public float Value1;
		[DataValue("Rear")]
		public float Value2;

		public override void Read(BinaryReader br)
		{
			Value1 = br.ReadSingle();
			Value2 = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value1);
			bw.Write(Value2);
		}

		
		public override string ToString()
		{
			return string.Format("{0}, {1}", Value1, Value2);
		}
	}

	public class CarBodyMotion: VLTDataItem
	{
		[DataValue("Value1")]
		public float Value1;
		[DataValue("Value2")]
		public float Value2;
		[DataValue("Value3")]
		public float Value3;

		public override void Read(BinaryReader br)
		{
			Value1 = br.ReadSingle();
			Value2 = br.ReadSingle();
			Value3 = br.ReadSingle();
		}

		public override void Write(BinaryWriter bw)
		{
			bw.Write(Value1);
			bw.Write(Value2);
			bw.Write(Value3);
		}
	}
}

