using System;
using System.IO;
using System.Reflection;

namespace VLTEdit.Types
{
	public abstract class VLTBaseType : IBinReadWrite // OBF: bb.cs
	{
		public uint ui1; // address in file
		public bool boo1; // true if in VLT file, false if in BIN file
		public uint ui2;
		public uint ui3;
		public int i1;
		public UnknownDR dr1;

		public static VLTBaseType a( Type A_0 )
		{
			ConstructorInfo constructor = A_0.GetConstructor( Type.EmptyTypes );
			return constructor.Invoke( null ) as VLTBaseType;
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
