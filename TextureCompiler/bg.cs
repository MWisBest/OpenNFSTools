using System.IO;
using System.Text;

public class bg
{
	private int intA; // obf: "a"

	private int intB; // obf: "b"

	private int intC; // obf: "c"

	public string stringD; // obf: "d"

	public uint uintE; // obf: "e"

	public int intF; // obf: "f"

	private int intG; // obf: "g"

	public uint uintH; // obf: "h"

	public uint uintI; // obf: "i"

	public uint uintJ; // obf: "j"

	public uint uintK; // obf: "k"

	public int intL; // obf: "l"

	public ushort ushortM; // obf: "m"

	public ushort ushortN; // obf: "n"

	public int intO; // obf: "o"

	public int intP; // obf: "p"

	public int intQ; // obf: "q"

	public int intR; // obf: "r"

	public int intS; // obf: "s"

	public int intT; // obf: "t"

	public int intU; // obf: "u"

	public int intV; // obf: "v"

	private byte[] byteArrayW; // obf: "w"

	private int intX; // obf: "x"

	private int intY; // obf: "y"

	public int intZ; // obf: "z"

	public int intAA; // obf: "aa"

	public int intAB; // obf: "ab"

	public int intAC; // obf: "ac"

	private int intAD; // obf: "ad"

	private int intAE; // obf: "ae"

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
		this.intB = A_0.ReadInt32();
		this.intC = A_0.ReadInt32();
		this.stringD = bg.a( A_0, 24 );
		this.uintE = A_0.ReadUInt32();
		this.intF = A_0.ReadInt32();
		this.intG = A_0.ReadInt32();
		this.uintH = A_0.ReadUInt32();
		this.uintI = A_0.ReadUInt32();
		this.uintJ = A_0.ReadUInt32();
		this.uintK = A_0.ReadUInt32();
		this.intL = A_0.ReadInt32();
		this.ushortM = A_0.ReadUInt16();
		this.ushortN = A_0.ReadUInt16();
		this.intO = A_0.ReadInt32();
		this.intP = A_0.ReadInt32();
		this.intQ = A_0.ReadInt32();
		this.intR = A_0.ReadInt32();
		this.intS = A_0.ReadInt32();
		this.intT = A_0.ReadInt32();
		this.intU = A_0.ReadInt32();
		this.intV = A_0.ReadInt32();
		this.byteArrayW = A_0.ReadBytes( 20 );
		this.intX = A_0.ReadInt32();
		this.intY = A_0.ReadInt32();
		this.intZ = A_0.ReadInt32();
		this.intAA = A_0.ReadInt32();
		this.intAB = A_0.ReadInt32();
		this.intAC = A_0.ReadInt32();
		this.intAD = A_0.ReadInt32();
		this.intAE = A_0.ReadInt32();
	}

	public void a( BinaryWriter A_0 )
	{
		if( this.byteArrayW == null )
		{
			this.byteArrayW = new byte[20];
		}
		A_0.Write( this.intA );
		A_0.Write( this.intB );
		A_0.Write( this.intC );
		bg.a( A_0, this.stringD, 24 );
		A_0.Write( this.uintE );
		A_0.Write( this.intF );
		A_0.Write( this.intG );
		A_0.Write( this.uintH );
		A_0.Write( this.uintI );
		A_0.Write( this.uintJ );
		A_0.Write( this.uintK );
		A_0.Write( this.intL );
		A_0.Write( this.ushortM );
		A_0.Write( this.ushortN );
		A_0.Write( this.intO );
		A_0.Write( this.intP );
		A_0.Write( this.intQ );
		A_0.Write( this.intR );
		A_0.Write( this.intS );
		A_0.Write( this.intT );
		A_0.Write( this.intU );
		A_0.Write( this.intV );
		A_0.Write( this.byteArrayW );
		A_0.Write( this.intX );
		A_0.Write( this.intY );
		A_0.Write( this.intZ );
		A_0.Write( this.intAA );
		A_0.Write( this.intAB );
		A_0.Write( this.intAC );
		A_0.Write( this.intAD );
		A_0.Write( this.intAE );
	}
}
