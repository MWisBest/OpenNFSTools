using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class AttribVector3 : VLTBaseType
	{
		[DataValue( "X" )]
		public float x;
		[DataValue( "Y" )]
		public float y;
		[DataValue( "Z" )]
		public float z;

		public override void read( BinaryReader br )
		{
			this.x = br.ReadSingle();
			this.y = br.ReadSingle();
			this.z = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.x );
			bw.Write( this.y );
			bw.Write( this.z );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}", this.x, this.y, this.z );
		}
	}
}
