using System.IO;

public class cx : d1
{
	private byte[] byteArrayA; // obf: "a"

	public cx() : base( a9.n )
	{
	}

	public cx( int length ) : base( a9.n )
	{
		this.byteArrayA = new byte[length];
		this.jB.b = (uint)length;
	}

	public override void a( BinaryReader A_0 )
	{
		if( this.jB.b > 0u )
		{
			this.byteArrayA = A_0.ReadBytes( (int)this.jB.b );
			return;
		}
		this.byteArrayA = null;
	}

	protected override void a( BinaryWriter A_0 )
	{
		if( this.jB.b > 0u )
		{
			A_0.Write( this.byteArrayA );
		}
	}
}
