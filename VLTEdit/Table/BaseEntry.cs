using System.IO;

namespace VLTEdit.Table
{
	// NOTE: This is for the actual entires in the database, NOT the table entries!
	public abstract class BaseEntry : IBinReadWrite
	{
		public UnknownAS as1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );

		public static BaseEntry a( EntryType A_0 )
		{
			switch( A_0 )
			{
				case EntryType.ROW:
					return new RowEntry();
				case EntryType.CLASS:
					return new ClassEntry();
				case EntryType.ROOT:
					return new RootEntry();
				default:
					return null;
			}
		}

		public RootEntry asRootEntry()
		{
			return this as RootEntry;
		}

		public ClassEntry asClassEntry()
		{
			return this as ClassEntry;
		}

		public RowEntry asRowEntry()
		{
			return this as RowEntry;
		}
	}
}
