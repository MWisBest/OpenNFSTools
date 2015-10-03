using System.IO;
using System.Text;

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
				StringBuilder sb = new StringBuilder();

				for( int i = 0; i < this.ba1.Length; ++i )
				{
					sb.Append( this.ba1[i].ToString( "x" ).PadLeft( 2, '0' ) ).Append( " " );
				}

				this.data = sb.ToString();
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.ba1 );
		}
	}
}
