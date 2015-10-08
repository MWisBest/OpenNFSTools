using System;

namespace VLTEdit.Types
{
	public class DataValueAttribute : Attribute
	{
		public bool Hex { get; set; }
		public string Name { get; private set; }

		public DataValueAttribute( string name )
		{
			this.Name = name;
			this.Hex = false;
		}
	}
}
