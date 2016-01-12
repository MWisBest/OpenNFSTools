using System.IO;

namespace NFSTools.TextureCompiler
{
	public class c2 : d1
	{
		public int intA; // obf: "a"

		public int intB; // obf: "b"

		public int intC; // obf: "c"

		public uint uintD; // obf: "d"

		public int intE; // obf: "e"

		public int intF; // obf: "f"

		public c2() : base( a9.l )
		{
		}

		public override void a( BinaryReader A_0 )
		{
			this.intA = A_0.ReadInt32();
			this.intB = A_0.ReadInt32();
			this.intC = A_0.ReadInt32();
			this.uintD = A_0.ReadUInt32();
			this.intE = A_0.ReadInt32();
			this.intF = A_0.ReadInt32();
		}

		protected override void a( BinaryWriter A_0 )
		{
			A_0.Write( this.intA );
			A_0.Write( this.intB );
			A_0.Write( this.intC );
			A_0.Write( this.uintD );
			A_0.Write( this.intE );
			A_0.Write( this.intF );
		}
	}
}
