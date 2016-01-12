using System.IO;

namespace NFSTools.TextureCompiler
{
	public class u : d1
	{
		public int intA; // obf: "a"

		public string stringB; // obf: "b"

		public string stringC; // obf: "c"

		public uint uintD; // obf: "d"

		private byte[] byteArrayE; // obf: "e"

		public u() : base( a9.f )
		{
		}

		public override void a( BinaryReader A_0 )
		{
			this.intA = A_0.ReadInt32();
			this.stringB = d1.a( A_0, 28 );
			this.stringC = d1.a( A_0, 64 );
			this.uintD = A_0.ReadUInt32();
			this.byteArrayE = A_0.ReadBytes( 24 );
		}

		protected override void a( BinaryWriter A_0 )
		{
			A_0.Write( this.intA );
			d1.a( A_0, this.stringB, 28 );
			d1.a( A_0, this.stringC, 64 );
			A_0.Write( this.uintD );
			if( this.byteArrayE == null )
			{
				this.byteArrayE = new byte[24];
			}
			A_0.Write( this.byteArrayE );
		}
	}
}
