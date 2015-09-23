using System;
using System.IO;

namespace VLTEdit
{
	public class EACarBodyMotion : EABaseType
	{
		[DataValue( "Value1" )]
		public float value1;
		[DataValue( "Value2" )]
		public float value2;
		[DataValue( "Value3" )]
		public float value3;

		public override void read( BinaryReader A_0 )
		{
			this.value1 = A_0.ReadSingle();
			this.value2 = A_0.ReadSingle();
			this.value3 = A_0.ReadSingle();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.value1 );
			A_0.Write( this.value2 );
			A_0.Write( this.value3 );
		}
	}
}
