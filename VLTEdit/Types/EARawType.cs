using System.IO;

namespace VLTEdit
{
	public class EARawType : EABaseType // OBF: ad.cs
	{
		[DataValue( "Length", Hex = true )]
		public int len;
		[DataValue( "Data" )]
		public string data;
		private byte[] ba1;

		public override void read( BinaryReader br )
		{
			this.ba1 = br.ReadBytes( this.len );
			this.data = "";
			if( this.len <= 32 )
			{
				for( int i = 0; i < this.ba1.Length; ++i )
				{
					this.data += string.Format( "{0:x}", this.ba1[i] ).PadLeft( 2, '0' ) + " ";
				}
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.ba1 );
		}
	}
}
