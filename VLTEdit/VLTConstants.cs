using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLTEdit
{
	public static class VLTConstants
	{
		// This value often appears in the MW VLT, but never in Carbon.
		// It appears to have been meant as some sort of separator or terminator.
		// Presumably cut in Carbon due to it being unnecessary and taking up extra space.
		public static uint MW_DEADBEEF = 4026452701u; // 0xEFFECADD // DD CA FE EF
	}
}
