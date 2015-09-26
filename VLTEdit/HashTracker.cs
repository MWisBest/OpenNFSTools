using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace VLTEdit
{
	public class HashTracker // OBF: bq.cs
	{
		// MW: Change from Hashtable to Dictionary<uint, string>
		// NOTE: This is initialized oddly, but I think it's because of how it keeps track of used hashes?
		private static Dictionary<uint, string> knownActualHashes;
		private static Dictionary<uint, string> hashGuesses;
		private static Dictionary<uint, string> ht3;
		private static Dictionary<uint, string> usedHashes;

		static HashTracker()
		{
			HashTracker.ht3 = new Dictionary<uint, string>();
			HashTracker.usedHashes = new Dictionary<uint, string>();
			HashTracker.init();
		}

		public static void init()
		{
			HashTracker.knownActualHashes = new Dictionary<uint, string>();
			HashTracker.hashGuesses = new Dictionary<uint, string>();
			string hashFile = ( new FileInfo( Application.ExecutablePath ) ).Directory.FullName + "\\hashes.txt";
			if( File.Exists( hashFile ) )
			{
				HashTracker.loadHashes( hashFile );
			}
		}

		public static string getValueForHash( uint hash )
		{
			if( HashTracker.ht3.ContainsKey( hash ) )
			{
				return HashTracker.ht3[hash];
			}
			if( HashTracker.knownActualHashes.ContainsKey( hash ) )
			{
				string text = HashTracker.knownActualHashes[hash];
				if( !HashTracker.usedHashes.ContainsKey( hash ) && !HashTracker.ht3.ContainsKey( hash ) )
				{
					HashTracker.usedHashes[hash] = text;
				}
				return text;
			}
			if( HashTracker.hashGuesses.ContainsKey( hash ) )
			{
				string text2 = HashTracker.hashGuesses[hash];
				if( !HashTracker.usedHashes.ContainsKey( hash ) && !HashTracker.ht3.ContainsKey( hash ) )
				{
					HashTracker.usedHashes[hash] = text2;
				}
				return text2;
			}
			return string.Format( "0x{0:x8}", hash );
		}

		public static void loadHashes( string fileName )
		{
			StreamReader streamReader = new StreamReader( fileName );
			string text;
			while( ( text = streamReader.ReadLine() ) != null )
			{
				text = text.Trim();
				if( text != "" && !text.StartsWith( "#" ) )
				{
					string[] array = text.Split( new char[] { '\t' } );
					uint num;
					if( array.Length > 1 )
					{
						if( array[1].StartsWith( "0x" ) )
						{
							num = uint.Parse( array[1].Substring( 2 ), NumberStyles.AllowHexSpecifier | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite );
						}
						else
						{
							num = uint.Parse( array[1], NumberStyles.AllowHexSpecifier | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite );
						}
						if( !HashTracker.hashGuesses.ContainsKey( num ) )
						{
							HashTracker.hashGuesses[num] = array[2];
						}
					}
					else
					{
						num = HashUtil.getHash32( text );
						if( !HashTracker.knownActualHashes.ContainsKey( num ) )
						{
							HashTracker.knownActualHashes[num] = text;
						}
					}
				}
			}
			streamReader.Close();
		}

		public static void b( string A_0 )
		{
			uint num = HashUtil.getHash32( A_0 );
			if( !HashTracker.ht3.ContainsKey( num ) )
			{
				HashTracker.ht3[num] = A_0;
				if( HashTracker.knownActualHashes.ContainsKey( num ) )
				{
					HashTracker.knownActualHashes.Remove( num );
				}
			}
		}

		public static void dumpUsedHashes( string filename )
		{
			StreamWriter streamWriter = new StreamWriter( filename );
			uint[] array = new uint[HashTracker.usedHashes.Count];
			HashTracker.usedHashes.Keys.CopyTo( array, 0 );
			uint[] array2 = array;
			for( int i = 0; i < array2.Length; ++i )
			{
				uint num = array2[i];
				if( HashTracker.hashGuesses.ContainsKey( num ) )
				{
					streamWriter.WriteLine( string.Format( "dump\t{0:x}\t{1}", num, HashTracker.usedHashes[num].ToString() ) );
				}
			}
			array2 = array;
			for( int i = 0; i < array2.Length; ++i )
			{
				uint num2 = array2[i];
				if( !HashTracker.hashGuesses.ContainsKey( num2 ) )
				{
					streamWriter.WriteLine( HashTracker.usedHashes[num2].ToString() );
				}
			}
			streamWriter.Close();
		}
	}
}
