using System;
using System.IO;

namespace NFSTools.TextureCompiler
{
	public class co : cd
	{
		private string stringA; // obf: "a"

		public co() : base( a9.c )
		{
		}

		public void a( string A_0 )
		{
			try
			{
				FileStream fileStream = new FileStream( A_0, FileMode.Open, FileAccess.Read );
				this.stringA = A_0;
				BinaryReader a_ = new BinaryReader( fileStream );
				this.a( a_ );
				fileStream.Close();
			}
			catch( Exception ex )
			{
				throw new Exception( "Could not open texture file. " + ex.Message );
			}
		}

		public void a()
		{
			this.b( this.stringA );
		}

		public void b( string A_0 )
		{
			try
			{
				FileStream fileStream = new FileStream( A_0, FileMode.Create, FileAccess.Write );
				BinaryWriter a_ = new BinaryWriter( fileStream );
				this.b( a_ );
				fileStream.Close();
			}
			catch( Exception ex )
			{
				throw new Exception( "Could not save texture file. " + ex.Message );
			}
		}

		protected new void a( BinaryReader A_0 )
		{
			A_0.BaseStream.Seek( 0L, SeekOrigin.Begin );
			this.jB = default( j );
			this.jB.uintB = (uint)A_0.BaseStream.Length;
			base.a( A_0 );
		}

		protected new void b( BinaryWriter A_0 )
		{
			base.b( A_0 );
		}
	}
}
