using System;
using System.IO;

namespace VLTEdit
{
	public class EABool : EABaseType
	{
		[DataValue( "Value" )]
		public bool value;

		public override void read( BinaryReader A_0 )
		{
			this.value = ( A_0.ReadByte() == 1 );
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.value ? 1 : 0 );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
