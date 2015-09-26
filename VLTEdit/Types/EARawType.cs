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

		public override void read( BinaryReader A_0 )
		{
			this.ba1 = A_0.ReadBytes( this.len );
			this.data = "";
			if( this.len <= 32 )
			{
				for( int i = 0; i < this.ba1.Length; ++i )
				{
					this.data += string.Format( "{0:x}", this.ba1[i] ).PadLeft( 2, '0' ) + " ";
				}
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ba1 );
		}
	}
}
