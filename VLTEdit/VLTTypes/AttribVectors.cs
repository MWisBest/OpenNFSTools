using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AttribVector2 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}", this.X, this.Y );
		}
	}

	public class AttribVector3 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;
		[DataValue( "Z" )]
		public float Z;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
			this.Z = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
			bw.Write( this.Z );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}", this.X, this.Y, this.Z );
		}
	}

	public class AttribVector4 : VLTDataItem
	{
		[DataValue( "X" )]
		public float X;
		[DataValue( "Y" )]
		public float Y;
		[DataValue( "Z" )]
		public float Z;
		[DataValue( "W" )]
		public float W;

		public override void Read( BinaryReader br )
		{
			this.X = br.ReadSingle();
			this.Y = br.ReadSingle();
			this.Z = br.ReadSingle();
			this.W = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.X );
			bw.Write( this.Y );
			bw.Write( this.Z );
			bw.Write( this.W );
		}

		public override string ToString()
		{
			return string.Format( "{0}, {1}, {2}, {3}", this.X, this.Y, this.Z, this.W );
		}
	}
}
