using System;
using System.Collections.Generic;
using System.IO;

namespace NFSTools.VLTEditLegacy
{
	public class UnknownA8 : UnknownC0, ITrimmable
	{
		private List<UnknownB8> genb8list;
		private List<UnknownB8> genb8listTwo;
		public Dictionary<int, UnknownB8> genht1;

		public void a( Stream A_0 )
		{
			BinaryWriter bw = new BinaryWriter( A_0 );
			foreach( UnknownB8 b in this.genb8listTwo )
			{
				bw.BaseStream.Seek( b.i1, SeekOrigin.Begin );
				bw.Write( b.i2 );
			}
		}

		public override void read( BinaryReader br )
		{
			this.genb8list = new List<UnknownB8>();
			this.genb8listTwo = new List<UnknownB8>();
			this.genht1 = new Dictionary<int, UnknownB8>();

			bool flag = false;

			UnknownB8 b;
			while( true )
			{
				b = new UnknownB8();
				b.read( br );
				this.genb8list.Add( b );
				if( b.s1 == 2 && ( b.s2 == 0 || b.s2 == 1 ) )
				{
					if( b.s2 == 1 )
					{
						flag = false;
					}
					else if( b.s2 == 0 )
					{
						flag = true;
					}
				}
				else
				{
					if( b.s1 == 1 )
					{
						if( flag )
						{
							this.genht1[b.i1] = b;
						}
						else
						{
							this.genb8listTwo.Add( b );
						}
					}
					else
					{
						if( b.s1 != 3 || b.s2 != 1 )
						{
							break;
						}
						if( flag )
						{
							this.genht1[b.i1] = b;
						}
						else
						{
							this.genb8listTwo.Add( b );
						}
					}
				}
			}

			this.Trim();

			if( b.s1 == 0 )
			{
				return;
			}
			throw new Exception( "Unknown ptr type." );
		}

		public override void write( BinaryWriter bw )
		{
			foreach( UnknownB8 b in this.genb8list )
			{
				b.write( bw );
			}
		}

		public void Trim()
		{
			if( BuildConfig.TRIMMING_ENABLED )
			{
				this.genb8list.TrimExcess();
				this.genb8listTwo.TrimExcess();
			}
		}
	}
}
