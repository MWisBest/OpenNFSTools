using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using NFSTools.LibNFS.Crypto;

namespace NFSTools.VLTEditLegacy
{
	public class HashTracker // OBF: bq.cs
	{
		// MW: Change from Hashtable to Dictionary<uint, string>
		// NOTE: This is initialized oddly, but I think it's because of how it keeps track of used hashes?
		private static Dictionary<uint, string> hashesFromTextFile;
		private static Dictionary<uint, string> guessesFromTextFile;
		private static Dictionary<uint, string> hashesFromVLTFile;
		private static Dictionary<uint, string> hashesInUse;

		static HashTracker()
		{
			HashTracker.hashesFromVLTFile = new Dictionary<uint, string>();
			HashTracker.hashesInUse = new Dictionary<uint, string>();
			HashTracker.init();
		}

		public static void init()
		{
			HashTracker.hashesFromTextFile = new Dictionary<uint, string>();
			HashTracker.guessesFromTextFile = new Dictionary<uint, string>();
			string hashFile = Path.Combine( ( new FileInfo( Application.ExecutablePath ) ).Directory.FullName, "hashes.txt" );
			if( File.Exists( hashFile ) )
			{
				HashTracker.loadHashes( hashFile );
			}
		}

		public static string getValueForHash( uint hash )
		{
			if( HashTracker.hashesFromVLTFile.ContainsKey( hash ) )
			{
				return HashTracker.hashesFromVLTFile[hash];
			}
			else if( HashTracker.hashesFromTextFile.ContainsKey( hash ) )
			{
				string text = HashTracker.hashesFromTextFile[hash];
				if( !HashTracker.hashesInUse.ContainsKey( hash ) && !HashTracker.hashesFromVLTFile.ContainsKey( hash ) )
				{
					HashTracker.hashesInUse[hash] = text;
				}
				return text;
			}
			else if( HashTracker.guessesFromTextFile.ContainsKey( hash ) )
			{
				string text2 = HashTracker.guessesFromTextFile[hash];
				if( !HashTracker.hashesInUse.ContainsKey( hash ) && !HashTracker.hashesFromVLTFile.ContainsKey( hash ) )
				{
					HashTracker.hashesInUse[hash] = text2;
				}
				return text2;
			}
			return string.Format( "0x{0:x8}", hash );
		}

		public static void loadHashes( string fileName )
		{
			const string expressionPrefix = "EXPR\t";
			using( StreamReader streamReader = new StreamReader( fileName ) )
			{
				string text;
				while( ( text = streamReader.ReadLine() ) != null )
				{
					text = text.Trim();
					if( text != "" && text[0] != '#' )
					{
						if( text.StartsWith( expressionPrefix ) )
						{
							// Basically, the idea with expressions is that there's plenty of hashes which have e.g. a common prefix or postfix.
							// By using some simple syntax to indicate that we can drastically reduce space wasted in the hashes file.
							text = text.Substring( expressionPrefix.Length );
							bool isStringExpr = ( text.IndexOf( '{' ) != -1 && ( text.LastIndexOf( '}' ) > text.IndexOf( '{' ) ) );
							bool isNumberExpr = ( text.IndexOf( '[' ) != -1 && ( text.LastIndexOf( ']' ) > text.IndexOf( '[' ) ) );

							if( isStringExpr && isNumberExpr )
							{
								throw new NotImplementedException( "String and Number in 1 expression not yet supported." );
							}
							else if( !isStringExpr && !isNumberExpr )
							{
								throw new FormatException( "Why do you have an expression that's not an expression?!" );
							}
							else if( ( text.IndexOf( '{' ) != text.LastIndexOf( '{' ) ) ||
								     ( text.IndexOf( '}' ) != text.LastIndexOf( '}' ) ) ||
								     ( text.IndexOf( '[' ) != text.LastIndexOf( '[' ) ) ||
								     ( text.IndexOf( ']' ) != text.LastIndexOf( ']' ) ) )
							{
								throw new NotImplementedException( "Only one expression at a time please!" );
							}

							if( isStringExpr )
							{
								string prefix = text.Split( '{' )[0];
								string postfix = text.Split( '}' )[1];
								string middle = text.Substring( text.IndexOf( '{' ) + 1, ( text.IndexOf( '}' ) - text.IndexOf( '{' ) - 1 ) );
								string[] entries = middle.Split( ',' );

								foreach( string entry in entries )
								{
									uint hash = JenkinsHash.getHash32( prefix + entry + postfix );
									if( !HashTracker.hashesFromTextFile.ContainsKey( hash ) )
									{
										HashTracker.hashesFromTextFile[hash] = prefix + entry + postfix;
									}
								}
							}
							else// if( isNumberExpr )
							{
								string prefix = text.Split( '[' )[0];
								string postfix = text.Split( ']' )[1];
								string middle = text.Substring( text.IndexOf( '[' ) + 1, ( text.IndexOf( ']' ) - text.IndexOf( '[' ) - 1 ) );

								// string.Split with a string is convoluted, so make it work with a character instead lol
								middle = middle.Replace( "..", "," );
								string[] entries = middle.Split( ',' );

								if( entries.Length != 2 )
								{
									throw new FormatException( "Invalid number entry format!" );
								}

								uint min, max;

								if( !uint.TryParse( entries[0], out min ) || !uint.TryParse( entries[1], out max ) )
								{
									throw new FormatException( "Failed to parse numbers!" );
								}

								if( max < min )
								{
									uint temp = max;
									max = min;
									min = temp;
								}

								for( uint i = min; i <= max; ++i )
								{
									uint hash = JenkinsHash.getHash32( prefix + i + postfix );
									if( !HashTracker.hashesFromTextFile.ContainsKey( hash ) )
									{
										HashTracker.hashesFromTextFile[hash] = prefix + i + postfix;
									}
								}
							}
						}
						else
						{
							uint hash;
							string[] array = text.Split( new char[] { '\t' } );
							if( array.Length > 1 ) // basically, if this is a guess.
							{
								int sub = ( array[1].StartsWith( "0x" ) ? 2 : 0 );
								hash = uint.Parse( array[1].Substring( sub ), NumberStyles.AllowHexSpecifier | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite );
								if( HashTracker.hashesFromTextFile.ContainsKey( hash ) )
								{
									frmDesigner.instance.writeToConsole( "NOTICE: Hash guess '" + array[2] + "' is already resolved as '" + HashTracker.hashesFromTextFile[hash] + "'" );
								}
								else if( HashTracker.hashesFromVLTFile.ContainsKey( hash ) )
								{
									frmDesigner.instance.writeToConsole( "NOTICE: Hash guess '" + array[2] + "' is already resolved as '" + HashTracker.hashesFromVLTFile[hash] + "'" );
								}
								if( !HashTracker.guessesFromTextFile.ContainsKey( hash ) )
								{
									HashTracker.guessesFromTextFile[hash] = array[2];
								}
							}
							else
							{
								hash = JenkinsHash.getHash32( text );
								if( !HashTracker.hashesFromTextFile.ContainsKey( hash ) )
								{
									HashTracker.hashesFromTextFile[hash] = text;
								}
								else
								{
									frmDesigner.instance.writeToConsole( "NOTICE: Found duplicate hash entry: '" + text + "'" );
								}
							}
						}
					}
				}
			}
		}

		public static void addHashFromVLTDB( string A_0 )
		{
			uint hash = JenkinsHash.getHash32( A_0 );
			if( !HashTracker.hashesFromVLTFile.ContainsKey( hash ) )
			{
				HashTracker.hashesFromVLTFile[hash] = A_0;
				if( HashTracker.hashesFromTextFile.ContainsKey( hash ) )
				{
					HashTracker.hashesFromTextFile.Remove( hash );
				}
			}
		}

		public static void dumpUsedHashes( string filename )
		{
			using( StreamWriter streamWriter = new StreamWriter( filename ) )
			{
				uint[] array = new uint[HashTracker.hashesInUse.Count];
				HashTracker.hashesInUse.Keys.CopyTo( array, 0 );
				uint[] array2 = array;
				for( int i = 0; i < array2.Length; ++i )
				{
					uint num = array2[i];
					if( HashTracker.guessesFromTextFile.ContainsKey( num ) )
					{
						streamWriter.WriteLine( string.Format( "dump\t{0:x}\t{1}", num, HashTracker.hashesInUse[num].ToString() ) );
					}
				}
				array2 = array;
				for( int i = 0; i < array2.Length; ++i )
				{
					uint num2 = array2[i];
					if( !HashTracker.guessesFromTextFile.ContainsKey( num2 ) )
					{
						streamWriter.WriteLine( HashTracker.hashesInUse[num2].ToString() );
					}
				}
			}
		}
	}
}
