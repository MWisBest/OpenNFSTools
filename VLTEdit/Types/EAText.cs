using System.IO;

namespace VLTEdit.Types
{
	public class EAText : EABaseType
	{
		[DataValue( "Offset", Hex = true )]
		public uint offset;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader br )
		{
			this.offset = br.ReadUInt32();
			if( this.offset > (ulong)br.BaseStream.Length )
			{
				this.offset = base.ui1;
			}
			if( this.offset == 0u )
			{
				this.value = "(null)";
				return;
			}
			long position = br.BaseStream.Position;
			br.BaseStream.Seek( (long)( (ulong)this.offset ), SeekOrigin.Begin );
			this.value = UnknownAP.a( br );
			br.BaseStream.Seek( position, SeekOrigin.Begin );
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.offset );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
