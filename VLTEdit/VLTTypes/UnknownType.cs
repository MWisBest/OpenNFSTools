using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class UnknownType : VLTDataItem
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
}
