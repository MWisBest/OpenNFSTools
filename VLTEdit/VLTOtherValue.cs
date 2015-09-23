using System;

namespace VLTEdit
{
	public enum VLTOtherValue : uint
	{
		// This is the beginning of the VLT file.
		VLTMAGIC = 1147498574u, // 0x4465704E // 4E 70 65 44

		// This is the beginning of the BIN file.
		BINMAGIC = 1400140357u, // 0x53747245 // 45 72 74 53

		d = 1400140366u, // 0x5374724E // 4E 72 74 53 // 0x00000030 in db.vlt
		e = 1147237454u, // 0x4461744E // 4E 74 61 44 // 0x00000040 in db.vlt

		TABLE_START = 1165520974u, // 0x4578704E // 4E 70 78 45
		TABLE_END = 1349808718u // 0x5074724E // 4E 72 74 50
	}
}
