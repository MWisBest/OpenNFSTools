using NFSTools.LibNFS.Crypto;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace NFSTools.VLTEdit
{
	public class HashResolver
	{
		static Hashtable _loadTable;
		static Hashtable _manualTable;
		static Hashtable _autoTable;
		static Hashtable _useTable;

		static HashResolver()
		{
			_autoTable = new Hashtable();
			_useTable = new Hashtable();
			Initialize();
		}

		public static void Initialize()
		{
			_loadTable = new Hashtable();
			_manualTable = new Hashtable();

			FileInfo fi = new FileInfo( Application.ExecutablePath );
			string filename = fi.Directory.FullName + "\\hashes.txt";
			if( File.Exists( filename ) )
			{
				Open( filename );
			}
		}

		public static string Resolve( uint hash )
		{
			//string key = string.Format("{0:x}", hash);
			uint key = hash;
			if( _autoTable.ContainsKey( key ) )
			{
				return (string)_autoTable[key];
			}
			else if( _loadTable.ContainsKey( key ) )
			{
				string value = (string)_loadTable[key];
				if( !_useTable.ContainsKey( key ) )
				{
					if( !_autoTable.ContainsKey( key ) )
					{
						_useTable.Add( key, value );
					}
				}
				return value;
			}
			else if( _manualTable.ContainsKey( key ) )
			{
				string value = (string)_manualTable[key];
				if( !_useTable.ContainsKey( key ) )
				{
					if( !_autoTable.ContainsKey( key ) )
					{
						_useTable.Add( key, value );
					}
				}

				return value;
			}
			else
			{
				return string.Format( "0x{0:x8}", hash );
			}
		}

		public static void Open( string filename )
		{
			StreamReader sr = new StreamReader( filename );
			string line;
			do
			{
				line = sr.ReadLine();
				if( line != null )
				{
					line = line.Trim();
				}

				if( line != null && line != "" && !line.StartsWith( "#" ) )
				{
					string[] split = line.Split( '\t' );
					uint key;
					if( split.Length > 1 )
					{
						if( split[1].StartsWith( "0x" ) )
						{
							key = uint.Parse( split[1].Substring( 2 ), NumberStyles.HexNumber );
						}
						else
						{
							key = uint.Parse( split[1], NumberStyles.HexNumber );
						}

						if( !_manualTable.ContainsKey( key ) )
						{
							_manualTable.Add( key, split[2] );
						}
					}
					else
					{
						key = JenkinsHash.getHash32( line );
						if( !_loadTable.ContainsKey( key ) )
						{
							_loadTable.Add( key, line );
						}
					}
				}
			} while( line != null );
			sr.Close();
		}

		public static void AddAuto( string value )
		{
			uint key = JenkinsHash.getHash32( value );
			if( !_autoTable.ContainsKey( key ) )
			{
				_autoTable.Add( key, value );
				if( _loadTable.ContainsKey( key ) )
				{
					_loadTable.Remove( key );
				}
			}
		}

		public static void SaveUsed( string filename )
		{
			StreamWriter sw = new StreamWriter( filename );
			uint[] hashes = new uint[_useTable.Count];
			_useTable.Keys.CopyTo( hashes, 0 );
			foreach( uint hash in hashes )
			{
				if( _manualTable.ContainsKey( hash ) )
				{
					sw.WriteLine( string.Format( "dump\t{0:x}\t{1}", hash, _useTable[hash].ToString() ) );
				}
			}
			foreach( uint hash in hashes )
			{
				if( !_manualTable.ContainsKey( hash ) )
				{
					sw.WriteLine( _useTable[hash].ToString() );
				}
			}
			sw.Close();
		}
	}
}
