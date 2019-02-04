using System.IO;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class RuntimeLinkage : VLTDataItem
	{
		[DataValue( "Info" )]
		public string Value;

		public RuntimeLinkage() : base()
		{
			this.Value = "Runtime Linkage";
		}

		public override void Read( BinaryReader br )
		{
			// do nothing
		}

		public override void Write( BinaryWriter bw )
		{
			// do nothing
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}
