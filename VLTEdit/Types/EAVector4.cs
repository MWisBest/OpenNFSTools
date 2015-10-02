using System.IO;

namespace VLTEdit
{
	public class EAVector4 : EABaseType
	{
		[DataValue( "X" )]
		public float x;
		[DataValue( "Y" )]
		public float y;
		[DataValue( "Z" )]
		public float z;
		[DataValue( "W" )]
		public float w;

		public override void read( BinaryReader br )
		{
			this.x = br.ReadSingle();
			this.y = br.ReadSingle();
			this.z = br.ReadSingle();
			this.w = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.x );
			bw.Write( this.y );
			bw.Write( this.z );
			bw.Write( this.w );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}, {3}", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			} );
		}
	}
}
