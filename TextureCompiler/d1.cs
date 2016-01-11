using System.IO;
using System.Text;

public abstract class d1
{
	protected cd cdA; // obf: "a"

	protected j jB; // obf: "b"

	public j d()
	{
		return this.jB;
	}

	public void a( j A_0 )
	{
		this.jB = A_0;
	}

	public abstract void a( BinaryReader A_0 );

	protected abstract void a( BinaryWriter A_0 );

	public d1()
	{
	}

	public d1( a9 id )
	{
		this.jB.a9A = id;
	}

	public void b( BinaryWriter A_0 )
	{
		this.jB.a( A_0 );
		long position = A_0.BaseStream.Position;
		this.a( A_0 );
		uint num = (uint)( A_0.BaseStream.Position - position );
		A_0.BaseStream.Seek( position - 4L, SeekOrigin.Begin );
		A_0.Write( num );
		A_0.Seek( (int)num, SeekOrigin.Current );
	}

	protected static string a( BinaryReader A_0, int A_1 )
	{
		byte[] array = A_0.ReadBytes( A_1 );
		int num = 0;
		while( num < A_1 && array[num] != 0 )
		{
			num++;
		}
		return Encoding.ASCII.GetString( array, 0, num );
	}

	protected static void a( BinaryWriter A_0, string A_1, int A_2 )
	{
		string text = A_1.PadRight( A_2, '\0' );
		byte[] bytes = Encoding.ASCII.GetBytes( text );
		A_0.Write( bytes );
	}
}
