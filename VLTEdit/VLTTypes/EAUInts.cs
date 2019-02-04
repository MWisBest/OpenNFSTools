using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EAUInt8 : VLTDataItem
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

	public class EAUInt16 : VLTDataItem
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

	public class EAUInt32 : VLTDataItem
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

	public class EAUInt64 : VLTDataItem
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
}
