using System;
using System.Collections;
using System.IO;
using System.Reflection;

[DefaultMember( "Item" )]
public class cg : d1, IEnumerable
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

	private Hashtable htA; // obf: "a"

	public cg() : base( a9.h )
	{
		this.htA = new Hashtable();
	}

	public cg.subclassA a( uint A_0 )
	{
		return (cg.subclassA)this.htA[A_0];
	}

	public void a( uint A_0, cg.subclassA A_1 )
	{
		this.htA[A_0] = A_1;
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
			this.htA[a.uintA] = a;
		}
	}

	protected override void a( BinaryWriter A_0 )
	{
		ArrayList arrayList = new ArrayList( this.htA.Count );
		IDictionaryEnumerator enumerator = this.htA.GetEnumerator();
		try
		{
			while( enumerator.MoveNext() )
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
				arrayList.Add( ( dictionaryEntry.Value as cg.subclassA ).uintA );
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if( disposable != null )
			{
				disposable.Dispose();
			}
		}
		arrayList.Sort();
		IEnumerator enumerator2 = arrayList.GetEnumerator();
		try
		{
			while( enumerator2.MoveNext() )
			{
				uint num = (uint)enumerator2.Current;
				cg.subclassA a = this.htA[num] as cg.subclassA;
				A_0.Write( a.uintA );
				A_0.Write( a.uintB );
				A_0.Write( a.uintC );
				A_0.Write( a.uintD );
				A_0.Write( a.intE );
				A_0.Write( a.intF );
			}
		}
		finally
		{
			IDisposable disposable = enumerator2 as IDisposable;
			if( disposable != null )
			{
				disposable.Dispose();
			}
		}
	}

	public IEnumerator GetEnumerator() // obf: "a()"
	{
		return this.htA.GetEnumerator();
	}
}
