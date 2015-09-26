using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownA8 : UnknownC0
	{
		private ArrayList al1;
		private Hashtable ht1;
		private ArrayList al2;

		public UnknownB8 a( int A_0 )
		{
			return this.ht1[A_0] as UnknownB8;
		}

		public void a( int A_0, UnknownB8 A_1 )
		{
			this.ht1[A_0] = A_1;
		}

		public void a( Stream A_0 )
		{
			BinaryWriter binaryWriter = new BinaryWriter( A_0 );
			IEnumerator enumerator = this.al2.GetEnumerator();
			try
			{
				while( enumerator.MoveNext() )
				{
					UnknownB8 b = (UnknownB8)enumerator.Current;
					binaryWriter.BaseStream.Seek( b.c(), SeekOrigin.Begin );
					binaryWriter.Write( b.b() );
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

		public override void read( BinaryReader A_0 )
		{
			this.al1 = new ArrayList();
			this.ht1 = new Hashtable();
			this.al2 = new ArrayList();
			bool flag = false;
			bool flag2 = false;
			UnknownB8 b;
			while( true )
			{
				b = new UnknownB8();
				b.read( A_0 );
				this.al1.Add( b );
				if( b.d() == 2 && ( b.a() == 0 || b.a() == 1 ) )
				{
					if( b.a() == 1 )
					{
						flag = false;
						flag2 = true;
					}
					else
					{
						if( b.a() == 0 )
						{
							flag = true;
							flag2 = false;
						}
					}
				}
				else
				{
					if( b.d() == 1 )
					{
						if( flag )
						{
							this.ht1[b.c()] = b;
						}
						if( flag2 )
						{
							this.al2.Add( b );
						}
					}
					else
					{
						if( b.d() != 3 || b.a() != 1 )
						{
							break;
						}
						if( flag )
						{
							this.ht1[b.c()] = b;
						}
						if( flag2 )
						{
							this.al2.Add( b );
						}
					}
				}
			}
			if( b.d() == 0 )
			{
				return;
			}
			throw new Exception( "Unknown ptr type." );
		}

		public override void write( BinaryWriter A_0 )
		{
			IEnumerator enumerator = this.al1.GetEnumerator();
			try
			{
				while( enumerator.MoveNext() )
				{
					UnknownB8 b = (UnknownB8)enumerator.Current;
					b.write( A_0 );
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
}
