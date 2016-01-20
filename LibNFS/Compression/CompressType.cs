namespace NFSTools.LibNFS.Compression
{
	public enum CompressType : uint
	{
		NULL =  0,
		RAWW =  1 << 0,
		JDLZ =  1 << 1,
		HUFF =  1 << 2,
		RFPK =  1 << 3,
		ALL  = (1 << 30) - 1
	}
}
