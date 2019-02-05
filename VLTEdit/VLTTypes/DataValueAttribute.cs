using System;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class DataValueAttribute : Attribute
	{
		public DataValueAttribute( string name )
		{
			this.Name = name;
			this.Hex = false;
		}

		public bool Hex
		{
			get; set;
		}

		public string Name
		{
			get;
		}
	}
}
