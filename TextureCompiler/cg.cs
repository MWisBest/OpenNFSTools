using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NFSTools.TextureCompiler
{
	[DefaultMember( "Item" )]
	public class cg : d1, IEnumerable<KeyValuePair<uint, cg.subclassA>>
	{
		public class subclassA // obf: "a"
		{
			public uint uintA; // obf: "a"

			public uint uintB; // obf: "b"

			public uint uintC; // obf: "c"

			public uint uintD; // obf: "d"

			public int intE; // obf: "e"

			public int intF; // obf: "f"
		}

		//private Hashtable htA; // obf: "a"
		private Dictionary<uint, cg.subclassA> dictB;

		public cg() : base( a9.h )
		{
			this.dictB = new Dictionary<uint, cg.subclassA>();
		}

		public cg.subclassA a( uint A_0 )
		{
			return this.dictB[A_0];
		}

		public void a( uint A_0, cg.subclassA A_1 )
		{
			this.dictB[A_0] = A_1;
		}

		public override void a( BinaryReader A_0 )
		{
			int num = (int)( this.jB.uintB / 24u );
			for( int i = 0; i < num; i++ )
			{
				cg.subclassA a = new cg.subclassA();
				a.uintA = A_0.ReadUInt32();
				a.uintB = A_0.ReadUInt32();
				a.uintC = A_0.ReadUInt32();
				a.uintD = A_0.ReadUInt32();
				a.intE = A_0.ReadInt32();
				a.intF = A_0.ReadInt32();
				this.dictB[a.uintA] = a;
			}
		}

		protected override void a( BinaryWriter A_0 )
		{
			List<uint> uintList = new List<uint>( this.dictB.Count );

			foreach( cg.subclassA a in this.dictB.Values )
			{
				uintList.Add( a.uintA );
			}

			uintList.Sort();

			foreach( uint num in uintList )
			{
				cg.subclassA a = this.dictB[num];
				A_0.Write( a.uintA );
				A_0.Write( a.uintB );
				A_0.Write( a.uintC );
				A_0.Write( a.uintD );
				A_0.Write( a.intE );
				A_0.Write( a.intF );
			}
		}

		public IEnumerator<KeyValuePair<uint, subclassA>> GetEnumerator()
		{
			return this.dictB.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
