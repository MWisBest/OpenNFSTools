using System;
using System.IO;

namespace VLTEdit
{
	public class EADouble : EABaseType
	{
		[DataValue( "Value" )]
		public double value;

		public override void read( BinaryReader A_0 )
		{
			this.value = A_0.ReadDouble();
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.value );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
