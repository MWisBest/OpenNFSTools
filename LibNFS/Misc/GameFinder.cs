using Microsoft.Win32;
using System;
using System.IO;

namespace NFSTools.LibNFS.Misc
{
	/// <summary>
	/// The purpose of this class is to find game installation directories using their registry entries.
	/// </summary>
	public static class GameFinder
	{
		public const string KEY_ROOT_32 = "SOFTWARE\\";
		public const string KEY_ROOT_64 = "SOFTWARE\\Wow6432Node\\";
		public const string SUB_UG_TO_MW = "EA GAMES\\";
		public const string SUB_CARBON_UP = "Electronic Arts\\";
		public const string ENTRY_UG = SUB_UG_TO_MW + "Need For Speed Underground\\";
		public const string ENTRY_UG2 = SUB_UG_TO_MW + "Need for Speed Underground 2\\";
		public const string ENTRY_MW = SUB_UG_TO_MW + "Need for Speed Most Wanted\\";
		public const string ENTRY_CARBON = SUB_CARBON_UP + "Need for Speed Carbon\\";
		public const string DIR_KEY = "Install Dir";

		/// <summary>
		/// Finds the installation directory for a NFS game.
		/// </summary>
		/// <param name="game">The game to look for.</param>
		/// <returns>DirectoryInfo if found, or null if not.</returns>
		public static DirectoryInfo findInstallDir( Game game )
		{
			string installdir = null;
			object installdirkv32 = null, installdirkv64 = null;
			RegistryKey subkey = null;

			switch( game )
			{
				case Game.UG:
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_32 + ENTRY_UG, false );
					installdirkv32 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_64 + ENTRY_UG, false );
					installdirkv64 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					break;
				case Game.UG2:
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_32 + ENTRY_UG2, false );
					installdirkv32 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_64 + ENTRY_UG2, false );
					installdirkv64 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					break;
				case Game.MW:
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_32 + ENTRY_MW, false );
					installdirkv32 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_64 + ENTRY_MW, false );
					installdirkv64 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					break;
				case Game.CARBON:
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_32 + ENTRY_CARBON, false );
					installdirkv32 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					subkey = Registry.LocalMachine.OpenSubKey( KEY_ROOT_64 + ENTRY_CARBON, false );
					installdirkv64 = subkey?.GetValue( DIR_KEY );
					subkey?.Close();
					break;
				default:
					throw new ArgumentException( "Unkown Game requested!", nameof( game ) );
			}

			if( installdirkv64 != null && installdirkv64 is string )
			{
				installdir = installdirkv64 as string;
			}
			else if( installdirkv32 != null && installdirkv32 is string )
			{
				installdir = installdirkv32 as string;
			}

			if( !string.IsNullOrWhiteSpace( installdir ) && Directory.Exists( installdir ) )
			{
				return new DirectoryInfo( installdir );
			}

			return null;
		}

		public enum Game
		{
			UG,
			UG2,
			MW,
			CARBON
		}
	}
}
