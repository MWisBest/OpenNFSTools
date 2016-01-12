namespace NFSTools.VLTEdit.Table
{
	/**
	 * These seem to represent some sort of values found in the VLT files.
	 * They are stored here in opposite endianness (e.x. 2383490743 becomes B72E118E, NOT 8E112EB7).
	 */
	public enum EntryType : uint
	{
		ROOT = 0xCBBC628Fu, // 8F62BCCB, 1 instance in NFS:MW and NFS:C db.vlt

		CLASS = 0x5E970CBCu, // BC0C975E, 57 instances in NFS:MW db.vlt, 80 instances in NFS:C db.vlt

		// often 16 bytes between instances in NFS:MW VLT, but only 12 in NFS:C VLT! 
		ROW = 0x8E112EB7u // B72E118E, 2309 instances in NFS:MW db.vlt (!!), 4092 instances in NFS:C db.vlt
	}
}
