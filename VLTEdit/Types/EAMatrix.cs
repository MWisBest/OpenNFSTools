using System;
using System.IO;

namespace VLTEdit
{
	public class EAMatrix : EABaseType
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

		public override void read( BinaryReader A_0 )
		{
			this.m11 = A_0.ReadSingle();
			this.m12 = A_0.ReadSingle();
			this.m13 = A_0.ReadSingle();
			this.m14 = A_0.ReadSingle();
			this.m21 = A_0.ReadSingle();
			this.m22 = A_0.ReadSingle();
			this.m23 = A_0.ReadSingle();
			this.m24 = A_0.ReadSingle();
			this.m31 = A_0.ReadSingle();
			this.m32 = A_0.ReadSingle();
			this.m33 = A_0.ReadSingle();
			this.m34 = A_0.ReadSingle();
			this.m41 = A_0.ReadSingle();
			this.m42 = A_0.ReadSingle();
			this.m43 = A_0.ReadSingle();
			this.m44 = A_0.ReadSingle();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.m11 );
			A_0.Write( this.m12 );
			A_0.Write( this.m13 );
			A_0.Write( this.m14 );
			A_0.Write( this.m21 );
			A_0.Write( this.m22 );
			A_0.Write( this.m23 );
			A_0.Write( this.m24 );
			A_0.Write( this.m31 );
			A_0.Write( this.m32 );
			A_0.Write( this.m33 );
			A_0.Write( this.m34 );
			A_0.Write( this.m41 );
			A_0.Write( this.m42 );
			A_0.Write( this.m43 );
			A_0.Write( this.m44 );
		}
	}
}
