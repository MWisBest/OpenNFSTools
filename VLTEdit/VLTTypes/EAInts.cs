using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EAInt8 : VLTDataItem
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

	public class EAInt16 : VLTDataItem
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

	public class EAInt32 : VLTDataItem
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

	public class EAInt64 : VLTDataItem
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
}
