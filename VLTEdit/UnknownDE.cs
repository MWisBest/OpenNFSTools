using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownDE : IEnumerable
	{
		public class aclzz
		{
			public uint ui1;
			public string s1;
			public int i1;
		}

		private Hashtable ht1;
		private Hashtable ht2;

		public UnknownDE.aclzz am( uint A_0 )
		{
			return this.ht1[A_0] as UnknownDE.aclzz;
		}

		public VLTClass[] b()
		{
			VLTClass[] array = new VLTClass[this.ht2.Values.Count];
			this.ht2.Values.CopyTo( array, 0 );
			return array;
		}

		public VLTClass b( uint A_0 )
		{
			return this.ht2[A_0] as VLTClass; // NOTE: if frontend is loaded before db, NPE here...
		}

		public void am( UnknownW A_0, UnknownB0 A_1 )
		{
			UnknownA8 a = A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8;
			int num = a.a( A_0.a() ).b();
			A_1.a().Seek( (long)num, 0 );
			BinaryReader a_ = new BinaryReader( A_1.a() );
			this.ht1 = new Hashtable( A_0.b() );
			for( int i = 0; i < A_0.b(); i++ )
			{
				UnknownDE.aclzz a2 = new UnknownDE.aclzz();
				a2.s1 = UnknownAP.a( a_ );
				a2.i1 = A_0.b( i );
				a2.ui1 = HashUtil.getHash32( a2.s1 );
				this.ht1.Add( a2.ui1, a2 );
				HashTracker.b( a2.s1 );
			}
			this.ht2 = new Hashtable();
		}

		public void a( UnknownC6 A_0, UnknownB0 A_1 )
		{
			VLTClass dq = new VLTClass();
			dq.a( A_0, A_1 );
			dq.a( this );
			this.ht2.Add( A_0.b(), dq );
		}

		public IEnumerator GetEnumerator()
		{
			return this.ht2.Values.GetEnumerator();
		}
	}
}
