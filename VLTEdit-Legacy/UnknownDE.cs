using System.Collections;
using System.Collections.Generic;
using System.IO;
using NFSTools.LibNFS.Crypto;
using NFSTools.VLTEditLegacy.Table;

namespace NFSTools.VLTEditLegacy
{
	public class UnknownDE : IEnumerable<VLTClass>
	{
		public class aclzz
		{
			public uint ui1;
			public string s1;
			public int i1;
		}

		private Dictionary<uint, aclzz> genht1;
		public Dictionary<uint, VLTClass> genht2;

		public void am( RootRecord A_0, UnknownB0 A_1 )
		{
			UnknownA8 a = A_1.a( VLTOtherValue.TABLE_END ) as UnknownA8;
			int num = a.genht1[A_0.position].i2;
			A_1.ms1.Seek( num, SeekOrigin.Begin );
			BinaryReader a_ = new BinaryReader( A_1.ms1 );
			this.genht1 = new Dictionary<uint, aclzz>( A_0.i3 );
			for( int i = 0; i < A_0.i3; ++i )
			{
				UnknownDE.aclzz a2 = new UnknownDE.aclzz();
				a2.s1 = UnknownAP.a( a_ );
				a2.i1 = A_0.ia1[i];
				a2.ui1 = JenkinsHash.getHash32( a2.s1 );
				this.genht1.Add( a2.ui1, a2 );
				HashTracker.addHashFromVLTDB( a2.s1 );
			}
			this.genht2 = new Dictionary<uint, VLTClass>();
		}

		public void a( ClassRecord A_0, UnknownB0 A_1 )
		{
			VLTClass dq = new VLTClass();
			dq.a( A_0, A_1 );
			this.genht2.Add( A_0.hash, dq );
		}

		public IEnumerator<VLTClass> GetEnumerator()
		{
			return this.genht2.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
