using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NFSTools.TextureCompiler
{
	public class cd : d1, IEnumerable<d1>
	{
		//protected ArrayList arrayListA; // obf: "a"
		protected List<d1> listB;

		public cd()
		{
			this.listB = new List<d1>();
		}

		public cd( a9 id )
		{
			this.jB.a9A = id;
			this.listB = new List<d1>();
		}

		public d1 a( a9 A_0 )
		{
			foreach( d1 d in this.listB )
			{
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

			return null;
		}

		public d1 a( int A_0 )
		{
			return this.listB[A_0];
		}

		public void a( int A_0, d1 A_1 )
		{
			this.listB[A_0] = A_1;
		}

		public d1 b( a9 A_0 )
		{
			foreach( d1 d in this.listB )
			{
				if( d.d().a9A == A_0 )
				{
					return d;
				}
			}

			return null;
		}

		public int b()
		{
			return this.listB.Count;
		}

		public void a( d1 A_0 )
		{
			this.listB.Add( A_0 );
		}

		public override void a( BinaryReader A_0 )
		{
			this.listB = new List<d1>();
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
					switch( a_.a9A )
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
						case a9.l:
							d = new c2();
							break;
						case a9.m:
							d = new cy();
							break;
						case a9.n:
							d = new cx();
							break;
						default:
							d = new bf();
							break;
					}
				}
				d.a( a_ );
				d.a( A_0 );
				this.listB.Add( d );
				A_0.BaseStream.Seek( position2 + (long)( (ulong)a_.uintB ), SeekOrigin.Begin );
			}
		}

		protected override void a( BinaryWriter A_0 )
		{
			foreach( d1 d in this.listB )
			{
				d.b( A_0 );
			}
		}

		public IEnumerator<d1> GetEnumerator()
		{
			return this.listB.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
