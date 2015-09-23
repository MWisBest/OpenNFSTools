using System;

namespace VLTEdit
{
	public class DataValueAttribute : Attribute
	{
		private string name;
		private bool hex;

		public bool Hex
		{
			get { return this.hex; }
			set { this.hex = value; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public DataValueAttribute( string name )
		{
			this.name = name;
			this.hex = false;
		}
	}
}
