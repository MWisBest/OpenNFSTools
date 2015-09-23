using System;
using System.Collections;
using System.Reflection;

namespace VLTEdit
{
	// VLTRoot?!
	[DefaultMember( "Item" )]
	public class UnknownDR : IEnumerable // OBF: dr.cs
	{
		private UnknownB0 b01;
		private UnknownC c1;
		private EABaseType[] bba1;
		private bool[] booa1;
		private VLTClass dq1;

		public VLTClass d()
		{
			return this.dq1;
		}

		public void a( VLTClass A_0 )
		{
			this.dq1 = A_0;
		}

		public UnknownB0 c()
		{
			return this.b01;
		}

		public void a( UnknownB0 A_0 )
		{
			this.b01 = A_0;
		}

		public UnknownC b()
		{
			return this.c1;
		}

		public void a( UnknownC A_0 )
		{
			this.c1 = A_0;
		}

		public UnknownDR( int count )
		{
			this.bba1 = new EABaseType[count];
			this.booa1 = new bool[count];
		}

		public EABaseType a( int A_0 )
		{
			return this.bba1[A_0];
		}

		public void a( int A_0, EABaseType A_1 )
		{
			this.booa1[A_0] = true;
			this.bba1[A_0] = A_1;
		}

		public bool b( int A_0 )
		{
			return this.booa1[A_0];
		}

		public IEnumerator GetEnumerator()
		{
			return this.bba1.GetEnumerator();
		}
	}
}
