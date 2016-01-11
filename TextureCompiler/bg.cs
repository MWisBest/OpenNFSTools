using System.IO;
using System.Text;

public class bg
{
	private int intA; // obf: "a"

	private int b;

	private int c;

	public string d;

	public uint e;

	public int f;

	private int g;

	public uint h;

	public uint i;

	public uint j;

	public uint k;

	public int l;

	public ushort m;

	public ushort n;

	public int o;

	public int p;

	public int q;

	public int r;

	public int s;

	public int t;

	public int u;

	public int v;

	private byte[] w;

	private int x;

	private int y;

	public int z;

	public int aa;

	public int ab;

	public int ac;

	private int ad;

	private int ae;

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

	public void a( BinaryReader A_0 )
	{
		this.intA = A_0.ReadInt32();
		this.b = A_0.ReadInt32();
		this.c = A_0.ReadInt32();
		this.d = bg.a( A_0, 24 );
		this.e = A_0.ReadUInt32();
		this.f = A_0.ReadInt32();
		this.g = A_0.ReadInt32();
		this.h = A_0.ReadUInt32();
		this.i = A_0.ReadUInt32();
		this.j = A_0.ReadUInt32();
		this.k = A_0.ReadUInt32();
		this.l = A_0.ReadInt32();
		this.m = A_0.ReadUInt16();
		this.n = A_0.ReadUInt16();
		this.o = A_0.ReadInt32();
		this.p = A_0.ReadInt32();
		this.q = A_0.ReadInt32();
		this.r = A_0.ReadInt32();
		this.s = A_0.ReadInt32();
		this.t = A_0.ReadInt32();
		this.u = A_0.ReadInt32();
		this.v = A_0.ReadInt32();
		this.w = A_0.ReadBytes( 20 );
		this.x = A_0.ReadInt32();
		this.y = A_0.ReadInt32();
		this.z = A_0.ReadInt32();
		this.aa = A_0.ReadInt32();
		this.ab = A_0.ReadInt32();
		this.ac = A_0.ReadInt32();
		this.ad = A_0.ReadInt32();
		this.ae = A_0.ReadInt32();
	}

	public void a( BinaryWriter A_0 )
	{
		if( this.w == null )
		{
			this.w = new byte[20];
		}
		A_0.Write( this.intA );
		A_0.Write( this.b );
		A_0.Write( this.c );
		bg.a( A_0, this.d, 24 );
		A_0.Write( this.e );
		A_0.Write( this.f );
		A_0.Write( this.g );
		A_0.Write( this.h );
		A_0.Write( this.i );
		A_0.Write( this.j );
		A_0.Write( this.k );
		A_0.Write( this.l );
		A_0.Write( this.m );
		A_0.Write( this.n );
		A_0.Write( this.o );
		A_0.Write( this.p );
		A_0.Write( this.q );
		A_0.Write( this.r );
		A_0.Write( this.s );
		A_0.Write( this.t );
		A_0.Write( this.u );
		A_0.Write( this.v );
		A_0.Write( this.w );
		A_0.Write( this.x );
		A_0.Write( this.y );
		A_0.Write( this.z );
		A_0.Write( this.aa );
		A_0.Write( this.ab );
		A_0.Write( this.ac );
		A_0.Write( this.ad );
		A_0.Write( this.ae );
	}
}
