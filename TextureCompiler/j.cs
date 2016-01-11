using System.IO;

public struct j
{
	public a9 a9A; // obf: "a"

	public uint b;

	public void a( BinaryReader A_0 )
	{
		this.a9A = (a9)A_0.ReadUInt32();
		this.b = A_0.ReadUInt32();
	}

	public void a( BinaryWriter A_0 )
	{
		A_0.Write( (uint)this.a9A );
		A_0.Write( this.b );
	}
}
