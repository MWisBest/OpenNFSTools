using System;
using System.IO;

namespace VLTEdit
{
	public class EAAxlePair : EABaseType
	{
		[DataValue( "Front" )]
		public float front;
		[DataValue( "Rear" )]
		public float rear;

		public override void read( BinaryReader A_0 )
		{
			this.front = A_0.ReadSingle();
			this.rear = A_0.ReadSingle();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.front );
			A_0.Write( this.rear );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.front, this.rear );
		}
	}
}
