using System;
using System.IO;

public class co : cd
{
	private string stringA; // obf: "a"

	public co() : base( a9.c )
	{
	}

	public void a( string A_0 )
	{
		try
		{
			FileStream fileStream = new FileStream( A_0, (FileMode)3, (FileAccess)1 );
			this.stringA = A_0;
			BinaryReader a_ = new BinaryReader( fileStream );
			this.a( a_ );
			fileStream.Close();
		}
		catch( Exception ex )
		{
			throw new Exception( "Could not open texture file. " + ex.Message );
		}
	}

	public void a()
	{
		this.b( this.stringA );
	}

	public void b( string A_0 )
	{
		try
		{
			FileStream fileStream = new FileStream( A_0, (FileMode)2, (FileAccess)2 );
			BinaryWriter a_ = new BinaryWriter( fileStream );
			this.b( a_ );
			fileStream.Close();
		}
		catch( Exception ex )
		{
			throw new Exception( "Could not save texture file. " + ex.Message );
		}
	}

	protected new void a( BinaryReader A_0 )
	{
		A_0.BaseStream.Seek( 0L, 0 );
		this.jB = default( j );
		this.jB.b = (uint)A_0.BaseStream.Length;
		base.a( A_0 );
	}

	protected new void b( BinaryWriter A_0 )
	{
		base.b( A_0 );
	}
}
