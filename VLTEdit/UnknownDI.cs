using System;
using System.IO;

namespace VLTEdit
{
	public abstract class UnknownDI : IBinReadWrite
	{
		public UnknownAS as1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );

		public static UnknownDI a( VLTCommonValue A_0 )
		{
			switch( A_0 )
			{
				case VLTCommonValue.COMMON:
					return new UnknownC();
				case VLTCommonValue.UNCOMMON:
					return new UnknownC6();
				case VLTCommonValue.RARE:
					return new UnknownW();
				default:
					return null;
			}
		}

		public UnknownW asUnknownW()
		{
			return this as UnknownW;
		}

		public UnknownC6 asUnknownC6()
		{
			return this as UnknownC6;
		}

		public UnknownC asUnknownC()
		{
			return this as UnknownC;
		}
	}
}
