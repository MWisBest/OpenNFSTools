using System.IO;

namespace NFSTools.VLTEditLegacy.Types
{
	public class AttribVector2 : VLTBaseType
	{
		[DataValue( "X" )]
		public float x;
		[DataValue( "Y" )]
		public float y;

		public override void read( BinaryReader br )
		{
			this.x = br.ReadSingle();
			this.y = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.x );
			bw.Write( this.y );
		}
		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.x, this.y );
		}
	}
}
