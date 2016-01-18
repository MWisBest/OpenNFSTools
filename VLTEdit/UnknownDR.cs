using System.Collections;
using NFSTools.VLTEdit.Table;
using NFSTools.VLTEdit.Types;

namespace NFSTools.VLTEdit
{
	// VLTRoot?!
	public class UnknownDR : IEnumerable // OBF: dr.cs
	{
		public UnknownB0 b01;
		public RowRecord c1;
		public VLTBaseType[] bba1;
		public bool[] booa1;
		public VLTClass dq1;

		public UnknownDR( int count )
		{
			this.bba1 = new VLTBaseType[count];
			this.booa1 = new bool[count];
		}

		public void a( int A_0, VLTBaseType A_1 )
		{
			this.booa1[A_0] = true;
			this.bba1[A_0] = A_1;
		}

		public IEnumerator GetEnumerator()
		{
			return this.bba1.GetEnumerator();
		}
	}
}
