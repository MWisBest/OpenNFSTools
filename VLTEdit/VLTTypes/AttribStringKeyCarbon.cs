using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AttribStringKeyCarbon : VLTDataItem
	{
		[DataValue( "Hash32", Hex = true )]
		public uint Hash;
		[DataValue( "Offset", Hex = true )]
		public uint StringOffset;
		[DataValue( "Value" )]
		public string Value;

		public override void Read( BinaryReader br )
		{
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
			//Hash = VLTHasher.Hash(Value);

			if( this.StringOffset == 0 )
			{
				bw.Write( (uint)0 );
			}
			else
			{
				bw.Write( this.Hash );
			}
			bw.Write( this.StringOffset );
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}
