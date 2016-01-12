using System.IO;

namespace NFSTools.VLTEdit.Types
{
	public class GCollectionKey : VLTBaseType // OBF: al.cs
	{
		[DataValue( "Hash", Hex = true )]
		public uint hash;
		[DataValue( "Value" )]
		public string value;

		public override void read( BinaryReader br )
		{
			this.hash = br.ReadUInt32();
			this.value = HashTracker.getValueForHash( this.hash );
		}

		public override void write( BinaryWriter bw )
		{
			// TODO: This doesn't make much sense, what if we got a "0x"-based hash from HashTracker?
			// Replace with writing stored hash for now
			//A_0.Write( HashUtil.getHash32( this.value ) );
			bw.Write( this.hash );
		}

		public override string ToString()
		{
			return this.value;
		}
	}
}
