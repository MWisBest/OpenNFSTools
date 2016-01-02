namespace VLTEdit
{
	public enum VLTOtherValue : uint
	{
		// This is the beginning of the VLT file.
		VLTMAGIC = 0x4465704Eu, // 4E 70 65 44

		// This is the beginning of the BIN file.
		BINMAGIC = 0x53747245u, // 45 72 74 53

		d = 0x5374724Eu, // 4E 72 74 53 // 0x00000030 in db.vlt
		e = 0x4461744Eu, // 4E 74 61 44 // 0x00000040 in db.vlt

		TABLE_START = 0x4578704Eu, // 4E 70 78 45
		TABLE_END = 0x5074724Eu, // 4E 72 74 50

		CARBON_VLT_HEAD = 0x56657273u // 73 72 65 56 // is this a problem?
	}
}
