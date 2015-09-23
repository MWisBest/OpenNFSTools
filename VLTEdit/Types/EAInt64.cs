using System;
using System.IO;

namespace VLTEdit
{
	public class EAInt64 : EABaseType
	{
		[DataValue( "Value" )]
		public long value;

		public override void read( BinaryReader A_0 )
		{
			this.value = A_0.ReadInt64();
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
