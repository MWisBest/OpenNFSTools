using System;
using System.Collections;
using System.IO;
using System.Reflection;

[DefaultMember( "Item" )]
public class cg : d1, IEnumerable
{
	public class subclassA // obf: "a"
	{
		public uint a;

		public uint b;

		public uint c;

		public uint d;

		public int e;

		public int f;
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
		int num = (int)( this.jB.b / 24u );
		for( int i = 0; i < num; i++ )
		{
			cg.subclassA a = new cg.subclassA();
			a.a = A_0.ReadUInt32();
			a.b = A_0.ReadUInt32();
			a.c = A_0.ReadUInt32();
			a.d = A_0.ReadUInt32();
			a.e = A_0.ReadInt32();
			a.f = A_0.ReadInt32();
			this.htA[a.a] = a;
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
				arrayList.Add( ( dictionaryEntry.Value as cg.subclassA ).a );
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
				A_0.Write( a.a );
				A_0.Write( a.b );
				A_0.Write( a.c );
				A_0.Write( a.d );
				A_0.Write( a.e );
				A_0.Write( a.f );
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
