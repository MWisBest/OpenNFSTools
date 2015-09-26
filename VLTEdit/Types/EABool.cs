using System.IO;

namespace VLTEdit
{
	public class EABool : EABaseType
	{
		[DataValue( "Value" )]
		public bool value;

		public override void read( BinaryReader A_0 )
		{
			// TODO: Do we need to read an entire int here?
			this.value = A_0.ReadBoolean();
		}

		public override void write( BinaryWriter A_0 )
		{
			// As well as write one here?
			A_0.Write( this.value );
		}

		public override string ToString()
		{
			return this.value.ToString();
		}
	}
}
