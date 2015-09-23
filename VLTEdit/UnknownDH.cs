using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	[DefaultMember( "Item" )]
	public class UnknownDH : UnknownC0, IEnumerable
	{
		private UnknownAS[] asa1;

		public UnknownAS a( int A_0 )
		{
			return this.asa1[A_0];
		}

		public void a( int A_0, UnknownAS A_1 )
		{
			this.asa1[A_0] = A_1;
		}

		public int a()
		{
			return this.asa1.Length;
		}

		public override void read( BinaryReader A_0 )
		{
			int num = A_0.ReadInt32();
			this.asa1 = new UnknownAS[num];
			for( int i = 0; i < num; i++ )
			{
				this.asa1[i] = new UnknownAS();
				this.asa1[i].read( A_0 );
			}
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.asa1.Length );
			for( int i = 0; i < this.asa1.Length; i++ )
			{
				this.asa1[i].write( A_0 );
			}
		}

		public IEnumerator GetEnumerator()
		{
			return this.asa1.GetEnumerator();
		}
	}
}
