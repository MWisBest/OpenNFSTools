using System;
using System.IO;

namespace VLTEdit
{
	public abstract class UnknownDI : UnknownCY, IBinReadWrite
	{
		protected UnknownAS as1;
		protected long l1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );

		public long getLong()
		{
			return this.l1;
		}

		public void setLong( long A_0 )
		{
			this.l1 = A_0;
		}

		public UnknownAS j()
		{
			return this.as1;
		}

		public void a( UnknownAS A_0 )
		{
			this.as1 = A_0;
		}

		public static UnknownDI a( VLTCommonValue A_0 )
		{
			if( BuildConfig.CARBON )
			{
				Console.WriteLine( "DI: VLTCommonValue: " + A_0.ToString() );
			}
			switch( A_0 )
			{
				case VLTCommonValue.COMMON:
					return new UnknownC();
				case VLTCommonValue.UNCOMMON:
					return new UnknownC6();
				case VLTCommonValue.RARE:
					return new UnknownW();
				default:
					Console.WriteLine( "DI: WTF VLTCommonValue: " + A_0.ToString() );
					return null;
			}
		}

		public UnknownW g()
		{
			return this as UnknownW;
		}

		public UnknownC6 k()
		{
			return this as UnknownC6;
		}

		public UnknownC i()
		{
			return this as UnknownC;
		}
	}
}
