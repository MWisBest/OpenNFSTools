using System;
using System.IO;

namespace VLTEdit
{
	public class EAVector2 : EABaseType
	{
		[DataValue( "X" )]
		public float x;
		[DataValue( "Y" )]
		public float y;

		public override void read( BinaryReader A_0 )
		{
			this.x = A_0.ReadSingle();
			this.y = A_0.ReadSingle();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.x );
			A_0.Write( this.y );
		}
		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.x, this.y );
		}
	}
}
