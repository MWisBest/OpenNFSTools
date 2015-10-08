using System.IO;
using System.Text;

namespace VLTEdit.Types
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
			this.data = string.Empty;
			if( this.len <= 32 )
			{
				StringBuilder sb = new StringBuilder( this.ba1.Length * 3 - 1 );
				int i = 0;

				while( i < this.ba1.Length )
				{
					sb.Append( this.ba1[i].ToString( "x" ).PadLeft( 2, '0' ) );
					if( ++i != this.ba1.Length )
					{
						sb.Append( ' ' );
					}
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
