using System;
using System.IO;
using System.Reflection;

namespace VLTEdit
{
	public abstract class EABaseType : IBinReadWrite // OBF: bb.cs
	{
		private uint ui1; // address in file
		private bool boo1; // true if in VLT file, false if in BIN file
		private uint ui2;
		private uint ui3;
		private int i1;
		private UnknownDR dr1;

		public UnknownDR m()
		{
			return this.dr1;
		}

		public void a( UnknownDR A_0 )
		{
			this.dr1 = A_0;
		}

		public int k()
		{
			return this.i1;
		}

		public void b( int A_0 )
		{
			this.i1 = A_0;
		}

		public uint j()
		{
			return this.ui2;
		}

		public void setUITwo( uint A_0 )
		{
			this.ui2 = A_0;
		}

		public uint h()
		{
			return this.ui3;
		}

		public void c( uint A_0 )
		{
			this.ui3 = A_0;
		}

		public uint i()
		{
			return this.ui1;
		}

		public void b( uint A_0 )
		{
			this.ui1 = A_0;
		}

		public bool g()
		{
			return this.boo1;
		}

		public void a( bool A_0 )
		{
			this.boo1 = A_0;
		}

		public static EABaseType a( Type A_0 )
		{
			ConstructorInfo constructor = A_0.GetConstructor( Type.EmptyTypes );
			return constructor.Invoke( null ) as EABaseType;
		}

		public abstract void read( BinaryReader br );

		public abstract void write( BinaryWriter bw );

		// MW: TODO: What is l() supposed to be?
		/*
		public virtual void l()
		{
		}
		*/

		public override string ToString()
		{
			return "Cannot dump this type.";
		}
	}
}
