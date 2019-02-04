using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class EAText : VLTDataItem
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
}
