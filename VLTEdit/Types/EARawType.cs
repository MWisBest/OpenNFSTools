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
				byte[] array = this.ba1;
				for( int i = 0; i < array.Length; i++ )
				{
					byte b = array[i];
					this.data += string.Format( "{0:x}", b ).PadLeft( 2, '0' ) + " ";
				}
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ba1 );
		}
	}
}
