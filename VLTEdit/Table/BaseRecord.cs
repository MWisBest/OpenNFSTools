using System.IO;

namespace VLTEdit.Table
{
	// NOTE: This is for the actual entires in the database, NOT the table entries!
	public abstract class BaseRecord : IBinReadWrite
	{
		public TableEntry as1;

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );

		public static BaseRecord a( EntryType A_0 )
		{
			switch( A_0 )
			{
				case EntryType.ROW:
					return new RowRecord();
				case EntryType.CLASS:
					return new ClassRecord();
				case EntryType.ROOT:
					return new RootRecord();
				default:
					return null;
			}
		}

		public RootRecord asRootEntry()
		{
			return this as RootRecord;
		}

		public ClassRecord asClassEntry()
		{
			return this as ClassRecord;
		}

		public RowRecord asRowEntry()
		{
			return this as RowRecord;
		}
	}
}
