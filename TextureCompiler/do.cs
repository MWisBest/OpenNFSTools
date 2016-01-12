using System.Collections;
using System.IO;
using System.Reflection;

namespace NFSTools.TextureCompiler
{
	[DefaultMember( "Item" )]
	public class @do : d1, IEnumerable
	{
		private uint[] uintArrayA; // obf: "a"

		private int[] intArrayB; // obf: "b"

		public @do() : base( a9.g )
		{
		}

		public int a()
		{
			return this.uintArrayA.Length;
		}

		public void a( int A_0 )
		{
			this.uintArrayA = new uint[A_0];
			this.intArrayB = new int[A_0];
		}

		public uint b( int A_0 )
		{
			return this.uintArrayA[A_0];
		}

		public void a( int A_0, uint A_1 )
		{
			this.uintArrayA[A_0] = A_1;
		}

		public override void a( BinaryReader A_0 )
		{
			int num = (int)( this.jB.uintB / 8u );
			this.uintArrayA = new uint[num];
			this.intArrayB = new int[num];
			for( int i = 0; i < num; i++ )
			{
				this.uintArrayA[i] = A_0.ReadUInt32();
				this.intArrayB[i] = A_0.ReadInt32();
			}
		}

		protected override void a( BinaryWriter A_0 )
		{
			for( int i = 0; i < this.uintArrayA.Length; i++ )
			{
				A_0.Write( this.uintArrayA[i] );
				A_0.Write( this.intArrayB[i] );
			}
		}

		public IEnumerator GetEnumerator() // obf: "b()"
		{
			return this.uintArrayA.GetEnumerator();
		}
	}
}
