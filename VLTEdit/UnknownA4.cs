using System;
using System.IO;

namespace VLTEdit
{
	public class UnknownA4 : UnknownC0
	{
		private byte[] ba1;

		public byte[] b()
		{
			return this.ba1;
		}

		public void a( byte[] A_0 )
		{
			this.ba1 = A_0;
		}

		public Stream a()
		{
			return new MemoryStream( this.ba1 );
		}

		public override void read( BinaryReader A_0 )
		{
			this.ba1 = A_0.ReadBytes( this.e1.a() );
		}

		public override void write( BinaryWriter A_0 )
		{
			A_0.Write( this.ba1 );
		}
	}
}
