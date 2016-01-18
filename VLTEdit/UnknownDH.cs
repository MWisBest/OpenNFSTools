using System.Collections;
using System.IO;

namespace NFSTools.VLTEdit
{
	public class UnknownDH : UnknownC0, IEnumerable
	{
		public TableEntry[] asa1;

		public override void read( BinaryReader br )
		{
			int num = br.ReadInt32();
			this.asa1 = new TableEntry[num];
			for( int i = 0; i < num; ++i )
			{
				this.asa1[i] = new TableEntry();
				this.asa1[i].read( br );
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.asa1.Length );
			foreach( TableEntry asa in this.asa1 )
			{
				asa.write( bw );
			}
		}

		public IEnumerator GetEnumerator()
		{
			return this.asa1.GetEnumerator();
		}
	}
}
