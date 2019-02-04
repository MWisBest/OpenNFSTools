namespace NFSTools.VLTEditLegacy
{
	public static class VLTConstants
	{
		// This value often appears in the MW VLT, but never in Carbon.
		// It appears to have been meant as some sort of separator or terminator.
		// Presumably cut in Carbon due to it being unnecessary and taking up extra space.
		public static uint MW_DEADBEEF = 0xEFFECADDu; // DD CA FE EF

		public static uint CARBON_SPACER = 0x00000000u;
	}
}
