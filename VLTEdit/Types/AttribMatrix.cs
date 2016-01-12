using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class AttribMatrix : VLTBaseType
	{
		[DataValue( "M[1,1]" )]
		public float m11;
		[DataValue( "M[1,2]" )]
		public float m12;
		[DataValue( "M[1,3]" )]
		public float m13;
		[DataValue( "M[1,4]" )]
		public float m14;
		[DataValue( "M[2,1]" )]
		public float m21;
		[DataValue( "M[2,2]" )]
		public float m22;
		[DataValue( "M[2,3]" )]
		public float m23;
		[DataValue( "M[2,4]" )]
		public float m24;
		[DataValue( "M[3,1]" )]
		public float m31;
		[DataValue( "M[3,2]" )]
		public float m32;
		[DataValue( "M[3,3]" )]
		public float m33;
		[DataValue( "M[3,4]" )]
		public float m34;
		[DataValue( "M[4,1]" )]
		public float m41;
		[DataValue( "M[4,2]" )]
		public float m42;
		[DataValue( "M[4,3]" )]
		public float m43;
		[DataValue( "M[4,4]" )]
		public float m44;

		public override void read( BinaryReader br )
		{
			this.m11 = br.ReadSingle();
			this.m12 = br.ReadSingle();
			this.m13 = br.ReadSingle();
			this.m14 = br.ReadSingle();
			this.m21 = br.ReadSingle();
			this.m22 = br.ReadSingle();
			this.m23 = br.ReadSingle();
			this.m24 = br.ReadSingle();
			this.m31 = br.ReadSingle();
			this.m32 = br.ReadSingle();
			this.m33 = br.ReadSingle();
			this.m34 = br.ReadSingle();
			this.m41 = br.ReadSingle();
			this.m42 = br.ReadSingle();
			this.m43 = br.ReadSingle();
			this.m44 = br.ReadSingle();
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.m11 );
			bw.Write( this.m12 );
			bw.Write( this.m13 );
			bw.Write( this.m14 );
			bw.Write( this.m21 );
			bw.Write( this.m22 );
			bw.Write( this.m23 );
			bw.Write( this.m24 );
			bw.Write( this.m31 );
			bw.Write( this.m32 );
			bw.Write( this.m33 );
			bw.Write( this.m34 );
			bw.Write( this.m41 );
			bw.Write( this.m42 );
			bw.Write( this.m43 );
			bw.Write( this.m44 );
		}
	}
}
