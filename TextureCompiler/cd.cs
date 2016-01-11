using System;
using System.Collections;
using System.IO;
using System.Reflection;

[DefaultMember( "Item" )]
public class cd : d1, IEnumerable
{
	protected ArrayList arrayListA; // obf: "a"

	public cd()
	{
		this.arrayListA = new ArrayList();
	}

	public cd( a9 id )
	{
		this.jB.a9A = id;
		this.arrayListA = new ArrayList();
	}

	public d1 a( a9 A_0 )
	{
		IEnumerator enumerator = this.arrayListA.GetEnumerator();
		try
		{
			while( enumerator.MoveNext() )
			{
				d1 d = (d1)enumerator.Current;
				if( d is cd )
				{
					d1 d2 = ( d as cd ).a( A_0 );
					if( d2 != null )
					{
						d1 result = d2;
						return result;
					}
				}
				else if( d.d().a9A == A_0 )
				{
					d1 result = d;
					return result;
				}
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
		return null;
	}

	public d1 a( int A_0 )
	{
		return this.arrayListA[A_0] as d1;
	}

	public void a( int A_0, d1 A_1 )
	{
		this.arrayListA[A_0] = A_1;
	}

	public d1 b( a9 A_0 )
	{
		IEnumerator enumerator = this.arrayListA.GetEnumerator();
		try
		{
			while( enumerator.MoveNext() )
			{
				d1 d = (d1)enumerator.Current;
				if( d.d().a9A == A_0 )
				{
					return d;
				}
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
		return null;
	}

	public int b()
	{
		return this.arrayListA.Count;
	}

	public IEnumerator GetEnumerator() // obf: "c()"
	{
		return this.arrayListA.GetEnumerator();
	}

	public void a( d1 A_0 )
	{
		this.arrayListA.Add( A_0 );
	}

	public override void a( BinaryReader A_0 )
	{
		this.arrayListA = new ArrayList();
		long position = A_0.BaseStream.Position;
		while( A_0.BaseStream.Position - position < (long)( (ulong)this.jB.uintB ) )
		{
			j a_ = default( j );
			a_.a( A_0 );
			long position2 = A_0.BaseStream.Position;
			d1 d;
			if( ( a_.a9A & (a9)2147483648u ) != a9.n )
			{
				d = new cd();
			}
			else
			{
				a9 a = a_.a9A;
				if( a != a9.n )
				{
					switch( a )
					{
						case a9.f:
							d = new u();
							break;
						case a9.g:
							d = new @do();
							break;
						case a9.h:
							d = new cg();
							break;
						default:
							switch( a )
							{
								case a9.l:
									d = new c2();
									break;
								case a9.m:
									d = new cy();
									break;
								default:
									d = new bf();
									break;
							}
							break;
					}
				}
				else
				{
					d = new cx();
				}
			}
			d.a( a_ );
			d.a( A_0 );
			this.arrayListA.Add( d );
			A_0.BaseStream.Seek( position2 + (long)( (ulong)a_.uintB ), SeekOrigin.Begin );
		}
	}

	protected override void a( BinaryWriter A_0 )
	{
		IEnumerator enumerator = this.arrayListA.GetEnumerator();
		try
		{
			while( enumerator.MoveNext() )
			{
				d1 d = (d1)enumerator.Current;
				d.b( A_0 );
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
	}
}
