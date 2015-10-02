using System;
using System.Collections.Generic;
using System.IO;

namespace VLTEdit
{
	public class UnknownB0 : ITrimmable
	{
		private string directory;
		private List<UnknownC0> genc0list;
		public MemoryStream ms1;
		public MemoryStream ms2;

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
				VLTOtherValue ce = e.ce1;
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

				c.e1 = e;
				c.read( A_0 );
				e.b( A_0.BaseStream );
				return c;
			}

			return null;
		}

		public UnknownC0 a( VLTOtherValue A_0 )
		{
			IEnumerator<UnknownC0> enumerator = this.genc0list.GetEnumerator();
			try
			{
				while( enumerator.MoveNext() )
				{
					UnknownC0 c = enumerator.Current;
					if( c.e1.ce1 == A_0 )
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
			this.directory = ( new FileInfo( A_0 ) ).Directory.FullName;
			FileStream fileStream = new FileStream( A_0, FileMode.Open, FileAccess.Read );
			this.a( fileStream, null );
			fileStream.Close();
		}

		public void a( Stream A_0, Stream A_1 )
		{
			byte[] array = new byte[A_0.Length];
			A_0.Read( array, 0, array.Length ); // array.Length --> .vlt FileSize
			this.ms2 = new MemoryStream( array );
			this.genc0list = new List<UnknownC0>();
			BinaryReader a_ = new BinaryReader( this.ms2 );
			UnknownC0 c;
			while( ( c = this.a( a_ ) ) != null )
			{
				this.genc0list.Add( c );
			}

			this.Trim();

			UnknownDH dh = this.a( VLTOtherValue.TABLE_START ) as UnknownDH;
			for( int i = 0; i < dh.asa1.Length; ++i )
			{
				dh.asa1[i].b( a_ ); // TODO read vs b? b, DEFINITELY b
			}
			if( A_1 == null )
			{
				DirectoryInfo directoryInfo = new DirectoryInfo( this.directory );
				UnknownBA ba = this.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[1];
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
			c.e1.a( this.ms1 );
			if( c.e1.ce1 == VLTOtherValue.BINMAGIC )
			{
				long num = this.ms1.Position + c.e1.a();
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

		public void Trim()
		{
			if( BuildConfig.TRIMMING_ENABLED )
			{
				this.genc0list.TrimExcess();
			}
		}
	}
}
