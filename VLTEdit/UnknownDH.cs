using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownDH : UnknownC0, IEnumerable<UnknownAS>
	{
		public UnknownAS[] asa1;

		public override void read( BinaryReader br )
		{
			int num = br.ReadInt32();
			this.asa1 = new UnknownAS[num];
			for( int i = 0; i < num; ++i )
			{
				this.asa1[i] = new UnknownAS();
				this.asa1[i].read( br );
			}
		}

		public override void write( BinaryWriter bw )
		{
			bw.Write( this.asa1.Length );
			foreach( UnknownAS asa in this.asa1 )
			{
				asa.write( bw );
			}
		}

		public IEnumerator<UnknownAS> GetEnumerator()
		{
			return (IEnumerator<UnknownAS>)this.asa1.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
    }
}
