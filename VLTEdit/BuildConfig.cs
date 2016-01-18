namespace NFSTools.VLTEdit
{
	public static class BuildConfig
	{
		public static bool DEBUG = false;
		public static bool CARBON = false;

		/**
		 * When true, Collections such as Lists will have their maximum sizes
		 * trimmed to their current size when they are no longer expected to grow.
		 * This can very slightly reduce memory overhead but has a cost of O(n) on
		 * every trim, which may be undesirable.
		 */
		public static bool TRIMMING_ENABLED = false;
	}
}
