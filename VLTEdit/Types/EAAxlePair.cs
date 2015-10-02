using System.IO;

namespace VLTEdit
{
	public class EAAxlePair : EABaseType
	{
		[DataValue( "Front" )]
		public float front;
		[DataValue( "Rear" )]
		public float rear;

		public override void read( BinaryReader br )
		{
			this.front = br.ReadSingle();
			this.rear = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.front );
			bw.Write( this.rear );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.front, this.rear );
		}
	}
}
