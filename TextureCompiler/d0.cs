using System;
using System.Collections;
using System.IO;
using System.Reflection;

internal class d0
{
	private class subclassA : IComparable // obf: "a"
	{
		public uint uintA; // obf: "a"

		public uint uintB; // obf: "b"

		public uint uintC; // obf: "c"

		public uint uintD; // obf: "d"

		public byte[] byteArrayE; // obf: "e"

		public int CompareTo( object obj ) // obf: "a(object A_0)"
		{
			d0.subclassA a = obj as d0.subclassA;
			return this.uintA.CompareTo( a.uintA );
		}
	}

	private static uint a( string A_0 )
	{
		uint num = 4294967295u;
		for( int i = 0; i < A_0.Length; i++ )
		{
			num *= 33u;
			num += A_0[i];
		}
		return num;
	}

	private static void b()
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		AssemblyName name = executingAssembly.GetName();
		Console.WriteLine( "NFS:MW Texture Compiler (mwtc) " + name.Version.ToString() );
		Console.WriteLine( "Copyright(C) 2005 - 2006, AruTec Inc. (Arushan), All Rights Reserved." );
		Console.WriteLine( "Contact: oneforaru at gmail dot com (bug reports only)" );
		Console.WriteLine();
		Console.WriteLine( "Disclaimer: This program is provided as is without any warranties of any kind." );
		Console.WriteLine();
	}

	private static void writeUsage()
	{
		Console.WriteLine( "Usage:   mwtc <texture-definition.txt>" );
		Console.WriteLine();
	}

	[STAThread]
	private static int Main( string[] args )
	{
		d0.b();
		if( args.Length < 1 )
		{
			d0.writeUsage();
			return 1;
		}
		string text = args[0];
		dv dv = new dv();
		dv.a( text, new string[]
		{
			"texture"
		} );
		FileInfo fileInfo = new FileInfo( text );
		Directory.SetCurrentDirectory( fileInfo.DirectoryName );
		string text2 = dv.a( "tpk", "xname" );
		ArrayList arrayList = new ArrayList();
		int num = dv.b( "texture" );
		uint num2 = 0u;
		for( int i = 0; i < num; i++ )
		{
			bw bw = new bw();
			string a_ = dv.a( "texture", i, "file" );
			bw.a( a_ );
			if( ( bw.g & bw.enumA.b ) == (bw.enumA)0 || ( bw.h != bw.enumC.b && bw.h != bw.enumC.c ) )
			{
				throw new Exception( "Currently only DXT1 and DXT3 textures are supported." );
			}
			bool flag = false;
			if( bw.h == bw.enumC.c )
			{
				flag = true;
			}
			bg bg = new bg();
			if( text2 == null )
			{
				bg.stringD = dv.a( "texture", i, "name" );
			}
			else
			{
				bg.stringD = text2 + "_" + dv.a( "texture", i, "name" );
			}
			bg.uintE = d0.a( bg.stringD );
			string text3 = dv.a( "texture", i, "usage" );
			if( text3 != null )
			{
				text3 = text3.ToLower();
			}
			if( text3 == "type1" )
			{
				bg.intF = 461498288;
			}
			else if( text3 == "type2" )
			{
				bg.intF = 497469606;
			}
			else
			{
				bg.intF = 1741775;
			}
			bool flag2 = dv.a( "texture", i, "flaga" ) == "1";
			bg.uintH = num2;
			bg.uintJ = (uint)bw.intC;
			bg.uintI = bg.uintH + bg.uintJ;
			bg.uintK = 0u;
			bg.intL = bw.intC;
			bg.ushortM = (ushort)bw.intB;
			bg.ushortN = (ushort)bw.intA;
			bg.intO = 2228224;
			bg.intO += (int)Math.Log( (double)bg.ushortN, 2.0 ) << 8;
			bg.intO += (int)Math.Log( (double)bg.ushortM, 2.0 );
			bg.intP = 65536;
			bg.intQ = ( flag ? 1280 : 0 );
			bg.intR = ( flag ? ( 66049 - ( flag2 ? 1 : 0 ) ) : ( 16777216 + ( flag2 ? 33554432 : 0 ) ) );
			bg.intS = 256;
			bg.intT = 0;
			bg.intU = 16777216;
			bg.intV = 256;
			bg.intZ = ( flag ? 1 : 0 );
			bg.intAA = 5;
			bg.intAB = 6;
			bg.intAC = (int)bw.h;
			MemoryStream memoryStream = new MemoryStream( bw.intC + 156 + 16 );
			BinaryWriter binaryWriter = new BinaryWriter( memoryStream );
			binaryWriter.Write( 1465336146 );
			binaryWriter.Write( 4097 );
			binaryWriter.Write( bw.intC + 156 );
			binaryWriter.Write( bw.intC + 156 + 16 );
			binaryWriter.Write( bw.byteArrayArrayS[0] );
			bg.a( binaryWriter );
			byte[] buffer = memoryStream.GetBuffer();
			memoryStream.Close();
			arrayList.Add( new d0.subclassA
			{
				uintA = bg.uintE,
				uintD = (uint)( buffer.Length - 16 ),
				uintC = (uint)buffer.Length,
				byteArrayE = buffer
			} );
			num2 += bg.uintJ + bg.uintK;
		}
		arrayList.Sort();
		int num3 = 220 + arrayList.Count * 32;
		int num4 = 128 - ( num3 + 8 ) % 128;
		int num5 = num3 + 8 + num4 + 256;
		uint num6 = (uint)num5;
		IEnumerator enumerator = arrayList.GetEnumerator();
		try
		{
			while( enumerator.MoveNext() )
			{
				d0.subclassA a = (d0.subclassA)enumerator.Current;
				a.uintB = num6;
				num6 += a.uintC;
				if( num6 % 64u != 0u )
				{
					num6 += 64u - num6 % 64u;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if( disposable != null )
			{
				disposable.Dispose();
			}
		}
		uint num7 = num6;
		co co = new co();
		co.a( new cx( 48 ) );
		cd cd = new cd( a9.e );
		u u = new u();
		u.stringC = dv.a( "tpk", "pipelinepath" );
		if( u.stringC == null )
		{
			u.stringC = "";
		}
		u.uintD = d0.a( u.stringC );
		u.stringB = dv.a( "tpk", "identifier" );
		if( u.stringB == null )
		{
			u.stringB = "";
		}
		u.intA = 5;
		cd.a( u );
		@do @do = new @do();
		@do.a( arrayList.Count );
		for( int j = 0; j < arrayList.Count; j++ )
		{
			@do.a( j, ( arrayList[j] as d0.subclassA ).uintA );
		}
		cd.a( @do );
		cg cg = new cg();
		for( int k = 0; k < arrayList.Count; k++ )
		{
			d0.subclassA a2 = arrayList[k] as d0.subclassA;
			cg.subclassA a3 = new cg.subclassA();
			a3.intE = 256;
			a3.uintA = a2.uintA;
			a3.uintC = a2.uintC;
			a3.uintD = a2.uintD;
			a3.uintB = a2.uintB;
			cg.a( a2.uintA, a3 );
		}
		cd.a( cg );
		co.a( cd );
		co.a( new cx( num4 ) );
		cd cd2 = new cd( a9.k );
		cd2.a( new c2
		{
			intC = 1,
			uintD = u.uintD
		} );
		cd2.a( new cx( 80 ) );
		cy cy = new cy();
		MemoryStream memoryStream2 = new MemoryStream( (int)( num7 - (uint)num5 + 120u ) );
		BinaryWriter binaryWriter2 = new BinaryWriter( memoryStream2 );
		for( int l = 0; l < arrayList.Count; l++ )
		{
			d0.subclassA a4 = arrayList[l] as d0.subclassA;
			binaryWriter2.Seek( (int)( a4.uintB - (uint)num5 + 120u ), 0 );
			binaryWriter2.Write( a4.byteArrayE );
		}
		cy.a( memoryStream2.GetBuffer() );
		memoryStream2.Close();
		cd2.a( cy );
		co.a( cd2 );
		string text4 = dv.a( "tpk", "output" );
		if( text4 == null )
		{
			text4 = "TEXTURES.BIN";
		}
		co.b( text4 );
		return 0;
	}
}
