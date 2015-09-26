using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using VLTEdit;

namespace VPak
{
	public class VPak
	{
		public const int VPAK_MAGIC = 0;

		public struct MainHeader : IBinReadWrite
		{
			public int magic;
			public int internalFileCount;
			public int fileTableLocation;
			public int fileTableLength;

			public void read( BinaryReader br )
			{
				this.magic = br.ReadInt32();
				this.internalFileCount = br.ReadInt32();
				this.fileTableLocation = br.ReadInt32();
				this.fileTableLength = br.ReadInt32();
				this.print();
			}

			public void print()
			{
				Console.WriteLine( "magic: " + magic.ToString() );
				Console.WriteLine( "internalFileCount: " + internalFileCount.ToString() );
				Console.WriteLine( "fileTableLocation: " + fileTableLocation.ToString() );
				Console.WriteLine( "fileTableLength: " + fileTableLength.ToString() );
			}

			public void write( BinaryWriter bw )
			{
				bw.Write( this.magic );
				bw.Write( this.internalFileCount );
				bw.Write( this.fileTableLocation );
				bw.Write( this.fileTableLength );
			}
		}

		public struct SubfileHeader : IBinReadWrite
		{
			public int fileNumber;
			public int binLength;
			public int vltLength;
			public int binLocation;
			public int vltLocation;
			public string internalName;

			public void read( BinaryReader br )
			{
				this.fileNumber = br.ReadInt32();
				this.binLength = br.ReadInt32();
				this.vltLength = br.ReadInt32();
				this.binLocation = br.ReadInt32();
				this.vltLocation = br.ReadInt32();
				this.print();
			}

			public void print()
			{
				Console.WriteLine( "fileNumber: " + fileNumber.ToString() );
				Console.WriteLine( "binLength: " + binLength.ToString() );
				Console.WriteLine( "vltLength: " + vltLength.ToString() );
				Console.WriteLine( "binLocation: " + binLocation.ToString() );
				Console.WriteLine( "vltLocation: " + vltLocation.ToString() );
			}

			public void write( BinaryWriter bw )
			{
				bw.Write( this.fileNumber );
				bw.Write( this.binLength );
				bw.Write( this.vltLength );
				bw.Write( this.binLocation );
				bw.Write( this.vltLocation );
			}
		}

		public static void packFile( string fileName )
		{
			Console.WriteLine( "Packing " + fileName + "..." );
			Console.WriteLine( "WARNING: This feature is new and is not guaranteed to work for all files!" );
			Console.WriteLine( "It should work with single-database VLT files (e.x. attributes.bin and FE_ATTRIB.bin are OK, gameplay.bin is not)." );
			Console.WriteLine( "Previous versions of VPak would output slightly incorrect .db and .vlt files; to properly pack a file, make sure it was unpacked with THIS version of VPak!" );

			/** PLAN:
			 *  .vls file contains line-separated list of InternalFile's internalName
			 *  the .vls file's name is the name of the original .bin file
			 * 
			 * PROBLEM #1: Currently we don't store the VPAK header
			 * SOLUTION #1: Store it at the end of the .vls file!
			 * 
			 * 
			 */

			FileStream fileStream = new FileStream( fileName, FileMode.Open, FileAccess.Read );
			FileStream fileStream2, fileStream3;

			StreamReader streamReader = new StreamReader( fileStream, System.Text.Encoding.ASCII );
			List<string> fileNames = new List<string>();
			string line;
			long headerOffest = 0L;
			while( ( line = streamReader.ReadLine() ) != null )
			{
				if( line[0] == 'V' && line[1] == 'P' && line[2] == 'A' && line[3] == 'K' )
				{
					// Reached end of filename list, found "VPAK" header magic!
					headerOffest = streamReader.BaseStream.Position - line.Length;
					break;
				}
				fileNames.Add( line );
			}

			Console.WriteLine( "headerOffset: " + headerOffest.ToString() );

			fileStream.Seek( headerOffest, SeekOrigin.Begin );
			BinaryReader binaryReader = new BinaryReader( fileStream );
			byte[] vpakHeader = binaryReader.ReadBytes( (int)(fileStream.Length - headerOffest) );
			binaryReader.Close();

			FileInfo fileInfo = new FileInfo( fileName );
			string binName = fileInfo.Name.Remove( fileInfo.Name.Length - fileInfo.Extension.Length, fileInfo.Extension.Length ) + ".bin";
			fileStream2 = new FileStream( binName, FileMode.Create, FileAccess.Write );

			fileStream2.Write( vpakHeader, 0, vpakHeader.Length );
			fileStream2.Seek( 0L, SeekOrigin.End );

			foreach( string binandvlt in fileNames )
			{
				for( int i = 0; i < 2; ++i )
				{
					fileStream3 = new FileStream( binandvlt + ( i == 0 ? ".bin" : ".vlt" ), FileMode.Open, FileAccess.Read );
					binaryReader = new BinaryReader( fileStream3 );
					byte[] data = binaryReader.ReadBytes( (int)fileStream3.Length );
					fileStream2.Write( data, 0, data.Length );
					fileStream2.Seek( 0L, SeekOrigin.End );
				}
			}

			fileStream2.Close();

			Console.WriteLine( "\tOutput: " + binName );
		}

		public static void unpackFile( string fileName )
		{
			Console.WriteLine( "Unpacking " + fileName + "..." );
			FileStream fileStream = new FileStream( fileName, FileMode.Open, FileAccess.Read );
			FileStream fileStream2;
			BinaryReader binaryReader = new BinaryReader( fileStream );

			MainHeader header = default( MainHeader );
			header.read( binaryReader );
			SubfileHeader[] files = new SubfileHeader[header.internalFileCount];
			int i;

			for( i = 0; i < header.internalFileCount; ++i )
			{
				files[i].read( binaryReader );
			}

			fileStream.Seek( header.fileTableLocation, SeekOrigin.Begin );
			byte[] fileTable = binaryReader.ReadBytes( header.fileTableLength );
			Hashtable hashtable = new Hashtable( header.internalFileCount );
			int num = 0;
			string text = "";

			for( i = 0; i < fileTable.Length; ++i )
			{
				if( fileTable[i] == 0 )
				{
					hashtable.Add( num, text );
					num = i + 1;
					text = "";
				}
				else
				{
					text += (char)fileTable[i];
				}
			}

			for( i = 0; i < header.internalFileCount; ++i )
			{
				files[i].internalName = ( hashtable[files[i].fileNumber] as string );
			}

			for( i = 0; i < header.internalFileCount; ++i )
			{
				Console.WriteLine( "\tOutput: " + files[i].internalName );

				fileStream.Seek( files[i].binLocation, SeekOrigin.Begin );
				byte[] array3 = binaryReader.ReadBytes( files[i].binLength );
				uint extraZeros = 0;
				try
				{
					while( binaryReader.ReadByte() == 0 )
					{
						++extraZeros;
					}
				}
				catch
				{
					// reached end of stream, stop here.
				}

				fileStream2 = new FileStream( files[i].internalName + ".bin", FileMode.Create, FileAccess.Write );
				fileStream2.Write( array3, 0, array3.Length );
				while( extraZeros > 0 )
				{
					fileStream2.WriteByte( 0 );
					--extraZeros;
				}
				fileStream2.Close();

				fileStream.Seek( files[i].vltLocation, SeekOrigin.Begin );
				byte[] array4 = binaryReader.ReadBytes( files[i].vltLength );
				uint extraZerosTwo = 0;
				try
				{
					while( binaryReader.ReadByte() == 0 )
					{
						++extraZerosTwo;
					}
				}
				catch
				{
					// reached end of stream, stop here.
				}

				fileStream2 = new FileStream( files[i].internalName + ".vlt", FileMode.Create, FileAccess.Write );
				fileStream2.Write( array4, 0, array4.Length );

				while( extraZerosTwo > 0 )
				{
					fileStream2.WriteByte( 0 );
					--extraZerosTwo;
				}

				fileStream2.Close();
			}

			FileInfo fileInfo = new FileInfo( fileName );
			string text2 = fileInfo.Name.Remove( fileInfo.Name.Length - fileInfo.Extension.Length, fileInfo.Extension.Length ) + ".vls";
			StreamWriter streamWriter = new StreamWriter( text2 );

			for( i = 0; i < files.Length; ++i )
			{
				streamWriter.WriteLine( files[i].internalName );
			}

			streamWriter.Close();

			// Store the VPAK header at the end of the .vls file!
			fileStream.Seek( 0L, SeekOrigin.Begin );
			byte[] vpakHeaderData = binaryReader.ReadBytes( files[0].binLocation );
			fileStream2 = new FileStream( text2, FileMode.Append, FileAccess.Write );
			fileStream2.Write( vpakHeaderData, 0, vpakHeaderData.Length );

			fileStream2.Close();
			fileStream.Close();
		}

		public static void printUsage()
		{
			Console.WriteLine( "Usage:" + Environment.NewLine + "\tUnpack: vpak [-u] filename.bin" + Environment.NewLine + "\tPack: vpak [-p] filename.vls" );
		}

		public static void Main( string[] args )
		{
			if( args.Length == 0 )
			{
				VPak.printUsage();
				return;
			}
			switch( args[0] )
			{
				case "-p": // Explicitly pack a given file, regardless of extension
					if( args[1] != null )
					{
						VPak.packFile( args[1] );
					}
					else
					{
						VPak.printUsage();
					}
					break;
				case "-u": // Explicitly unpack a given file, regardless of extension
					if( args[1] != null )
					{
						VPak.unpackFile( args[1] );
					}
					else
					{
						VPak.printUsage();
					}
					break;
				default:
					Console.WriteLine( "Determining unpack vs pack based on file extension..." );
					switch( args[0].Substring( Math.Max( 0, args[0].Length - 4 ) ).ToLower() ) // get (lowercase) file extension
					{
						case ".vls": // Index file created by us of an unpacked bin
							VPak.packFile( args[0] );
							break;
						case ".bin": // Game file (packed)
							VPak.unpackFile( args[0] );
							break;
						default:
							Console.WriteLine( "Invalid file extension; looking for: .bin (unpack), .vls (pack)!" );
							break;
					}
					break;
			}
		}
	}
}
