using System;
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

		public override void read( BinaryReader A_0 )
		{
			this.x = A_0.ReadSingle();
			this.y = A_0.ReadSingle();
			this.z = A_0.ReadSingle();
			this.w = A_0.ReadSingle();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.x );
			A_0.Write( this.y );
			A_0.Write( this.z );
			A_0.Write( this.w );
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
