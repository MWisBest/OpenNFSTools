using System;

namespace VLTEdit
{
	/**
	 * These seem to represent some sort of values found in the VLT files.
	 * They are stored here in opposite endianness (e.x. 2383490743 becomes B72E118E, NOT 8E112EB7).
	 */
	public enum VLTCommonValue : uint
	{
		RARE = 3418120847, // 8F62BCCB, 1 instance in NFS:MW and NFS:C db.vlt


		UNCOMMON = 1586957500, // BC0C975E, 57 instances in NFS:MW db.vlt, 80 instances in NFS:C db.vlt

		// often 16 bytes between instances in NFS:MW VLT, but only 12 in NFS:C VLT! 
		COMMON = 2383490743  // B72E118E, 2309 instances in NFS:MW db.vlt (!!), 4092 instances in NFS:C db.vlt
	}
}
