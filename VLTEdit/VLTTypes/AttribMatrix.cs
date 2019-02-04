using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class AttribMatrix : VLTDataItem
	{
		[DataValue( "M[1,1]" )]
		public float M11;
		[DataValue( "M[1,2]" )]
		public float M12;
		[DataValue( "M[1,3]" )]
		public float M13;
		[DataValue( "M[1,4]" )]
		public float M14;
		[DataValue( "M[2,1]" )]
		public float M21;
		[DataValue( "M[2,2]" )]
		public float M22;
		[DataValue( "M[2,3]" )]
		public float M23;
		[DataValue( "M[2,4]" )]
		public float M24;
		[DataValue( "M[3,1]" )]
		public float M31;
		[DataValue( "M[3,2]" )]
		public float M32;
		[DataValue( "M[3,3]" )]
		public float M33;
		[DataValue( "M[3,4]" )]
		public float M34;
		[DataValue( "M[4,1]" )]
		public float M41;
		[DataValue( "M[4,2]" )]
		public float M42;
		[DataValue( "M[4,3]" )]
		public float M43;
		[DataValue( "M[4,4]" )]
		public float M44;

		public override void Read( BinaryReader br )
		{
			this.M11 = br.ReadSingle();
			this.M12 = br.ReadSingle();
			this.M13 = br.ReadSingle();
			this.M14 = br.ReadSingle();
			this.M21 = br.ReadSingle();
			this.M22 = br.ReadSingle();
			this.M23 = br.ReadSingle();
			this.M24 = br.ReadSingle();
			this.M31 = br.ReadSingle();
			this.M32 = br.ReadSingle();
			this.M33 = br.ReadSingle();
			this.M34 = br.ReadSingle();
			this.M41 = br.ReadSingle();
			this.M42 = br.ReadSingle();
			this.M43 = br.ReadSingle();
			this.M44 = br.ReadSingle();
		}

		public override void Write( BinaryWriter bw )
		{
			bw.Write( this.M11 );
			bw.Write( this.M12 );
			bw.Write( this.M13 );
			bw.Write( this.M14 );
			bw.Write( this.M21 );
			bw.Write( this.M22 );
			bw.Write( this.M23 );
			bw.Write( this.M24 );
			bw.Write( this.M31 );
			bw.Write( this.M32 );
			bw.Write( this.M33 );
			bw.Write( this.M34 );
			bw.Write( this.M41 );
			bw.Write( this.M42 );
			bw.Write( this.M43 );
			bw.Write( this.M44 );
		}
	}
}
