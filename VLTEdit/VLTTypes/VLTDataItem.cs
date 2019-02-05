using System;
using System.IO;
using System.Reflection;

namespace NFSTools.VLTEdit.VLTTypes
{
	public abstract class VLTDataItem : IFileAccess
	{
		public VLTDataRow DataRow
		{
			get; set;
		}

		public int ArrayIndex
		{
			get; set;
		}

		public uint TypeHash
		{
			get; set;
		}

		public uint NameHash
		{
			get; set;
		}

		public uint Offset
		{
			get; set;
		}

		public bool InlineData
		{
			get; set;
		}

		public static VLTDataItem Instantiate( Type type )
		{
			ConstructorInfo mi = type.GetConstructor( Type.EmptyTypes );
			return mi.Invoke( null ) as VLTDataItem;
		}

		public abstract void Read( BinaryReader br );
		public abstract void Write( BinaryWriter bw );

		public virtual void LoadExtra()
		{
		}

		public override string ToString()
		{
			return "Cannot dump this type.";
		}
	}
}
