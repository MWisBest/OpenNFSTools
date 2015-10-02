namespace VLTEdit
{
	public static class BuildConfig
	{
		public static bool DEBUG = false;
		public static bool CARBON = false;
		public const string MAJOR_VER = "1";
		public const string MINOR_VER = "1";
		public const string PATCH_VER = "0";

		/**
		 * When true, Collections such as Lists will have their maximum sizes
		 * trimmed to their current size when they are no longer expected to grow.
		 * This can very slightly reduce memory overhead but has a cost of O(n) on
		 * every trim, which may be undesirable.
		 */
		public static bool TRIMMING_ENABLED = false;
	}
}
