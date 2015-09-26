using System;
using System.Collections;
using System.IO;

namespace VLTEdit
{
	public class UnknownB0
	{
		private string s1;
		private string s2;
		private ArrayList al1;
		private MemoryStream ms1;
		private MemoryStream ms2;

		private void a( BinaryWriter A_0, UnknownC0 A_1 )
		{
			long position = A_0.BaseStream.Position;
			A_0.BaseStream.Seek( 8L, SeekOrigin.Current );
			A_1.write( A_0 );
			long num = A_0.BaseStream.Position;
			if( num % 16L != 0L )
			{
				num += 16L - num % 16L;
			}
			A_1.c().a( (int)( num - position ) );
			A_0.BaseStream.Seek( position, SeekOrigin.Begin );
			A_1.c().write( A_0 );
			A_0.BaseStream.Seek( num, SeekOrigin.Begin );
		}

		private UnknownC0 a( BinaryReader A_0 )
		{
			if( A_0.BaseStream.Position == A_0.BaseStream.Length )
			{
				return null;
			}

			UnknownE e = new UnknownE();
			e.read( A_0 );
			if( e.c() )
			{
				VLTOtherValue ce = e.d();
				UnknownC0 c;

				switch( ce )
				{
					case VLTOtherValue.VLTMAGIC:
						c = new UnknownBA();
						break;
					case VLTOtherValue.TABLE_START:
						c = new UnknownDH();
						break;
					case VLTOtherValue.TABLE_END:
						c = new UnknownA8();
						break;
					case VLTOtherValue.d:
					case VLTOtherValue.e:
					default:
						c = new UnknownA4();
						break;
				}

				c.a( e );
				c.read( A_0 );
				e.b( A_0.BaseStream );
				return c;
			}

			return null;
		}

		public Stream a()
		{
			return this.ms1;
		}

		public Stream b()
		{
			return this.ms2;
		}

		public UnknownC0 a( VLTOtherValue A_0 )
		{
			IEnumerator enumerator = this.al1.GetEnumerator();
			try
			{
				while( enumerator.MoveNext() )
				{
					UnknownC0 c = (UnknownC0)enumerator.Current;
					if( c.c().d() == A_0 )
					{
						return c;
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
			return null;
		}

		public void a( string A_0 )
		{
			FileInfo fileInfo = new FileInfo( A_0 );
			this.s2 = fileInfo.Directory.FullName;
			this.s1 = A_0;
			FileStream fileStream = new FileStream( A_0, FileMode.Open, FileAccess.Read );
			this.a( fileStream, null );
			fileStream.Close();
		}

		public void a( Stream A_0, Stream A_1 )
		{
			byte[] array = new byte[A_0.Length];
			A_0.Read( array, 0, array.Length ); // array.Length --> .vlt FileSize
			this.ms2 = new MemoryStream( array );
			this.al1 = new ArrayList();
			BinaryReader a_ = new BinaryReader( this.ms2 );
			UnknownC0 c;
			int y = 0, z = 0;
			do
			{
				y++;
				c = this.a( a_ );
				if( c != null )
				{
					this.al1.Add( c );
				}
			}
			while( c != null );
			//Console.WriteLine( "B0: y: " + y ); // NFS:MW AND NFS:C --> 6
			UnknownDH dh = this.a( VLTOtherValue.TABLE_START ) as UnknownDH;
			Console.WriteLine( "B0: dh.a(): " + dh.a() ); // NFS:C --> 4173, NFS:MW --> 2637
			for( int i = 0; i < dh.a(); i++ )
			{
				if( BuildConfig.CARBON )
				{
					Console.WriteLine( "B0: i: " + i ); // NFS:C gets to 1, then fails, HARD
				}
				z++;
				UnknownAS asclz = dh.a( i );
				asclz.b( a_ ); // TODO read vs b? b, DEFINITELY b
			}
			Console.WriteLine( "B0: z: " + z ); // NFS:C does not get here, NFS:MW 2367
			if( A_1 == null )
			{
				DirectoryInfo directoryInfo = new DirectoryInfo( this.s2 );
				UnknownBA ba = this.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.a( 1 );
				FileInfo[] files = directoryInfo.GetFiles( text );
				if( files.Length == 0 )
				{
					throw new Exception( "Required file " + text + " was not found." );
				}
				A_1 = new FileStream( files[0].FullName, FileMode.Open, FileAccess.ReadWrite );
				array = new byte[A_1.Length];
				A_1.Read( array, 0, array.Length );
				A_1.Close();
			}
			else
			{
				array = new byte[A_1.Length];
				A_1.Read( array, 0, array.Length );
			}
			this.ms1 = new MemoryStream( array );
			a_ = new BinaryReader( this.ms1 );
			this.ms1.Seek( 0L, SeekOrigin.Begin );
			c = this.a( a_ );
			c.c().a( this.ms1 );
			if( c.c().d() == VLTOtherValue.BINMAGIC )
			{
				int num = (int)this.ms1.Position + c.c().a();
				while( this.ms1.Position < num )
				{
					string text2 = UnknownAP.a( a_ );
					if( text2 != "" )
					{
						HashTracker.b( text2 );
					}
				}
			}
			UnknownA8 a = this.a( VLTOtherValue.TABLE_END ) as UnknownA8;
			a.a( this.ms1 );
		}
	}
}
