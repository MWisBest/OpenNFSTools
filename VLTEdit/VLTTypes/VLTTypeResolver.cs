using System;
using System.Collections;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class VLTTypeResolver
	{
		private Hashtable _typeTable;
		public static VLTTypeResolver Resolver = new VLTTypeResolver();

		public VLTTypeResolver()
		{
			this._typeTable = new Hashtable();
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Double" ), typeof( EADouble ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Float" ), typeof( EAFloat ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt8" ), typeof( EAUInt8 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt16" ), typeof( EAUInt16 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt32" ), typeof( EAUInt32 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::UInt64" ), typeof( EAUInt64 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int8" ), typeof( EAInt8 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int16" ), typeof( EAInt16 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int32" ), typeof( EAInt32 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Int64" ), typeof( EAInt64 ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Bool" ), typeof( EABool ) );
			this._typeTable.Add( VLTHasher.Hash( "EA::Reflection::Text" ), typeof( EAText ) );

			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Matrix" ), typeof( AttribMatrix ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector2" ), typeof( AttribVector2 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector3" ), typeof( AttribVector3 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Types::Vector4" ), typeof( AttribVector4 ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::StringKey" ), typeof( AttribStringKey ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::RefSpec" ), typeof( AttribRefSpec ) );
			this._typeTable.Add( VLTHasher.Hash( "Attrib::Blob" ), typeof( AttribBlob ) );

			this._typeTable.Add( VLTHasher.Hash( "AxlePair" ), typeof( AxlePair ) );
			this._typeTable.Add( VLTHasher.Hash( "CarBodyMotion" ), typeof( CarBodyMotion ) );
			this._typeTable.Add( VLTHasher.Hash( "GCollectionKey" ), typeof( GCollectionKey ) );
			this._typeTable.Add( VLTHasher.Hash( "JunkmanMod" ), typeof( JunkmanMod ) );
			this._typeTable.Add( VLTHasher.Hash( "UpgradeSpecs" ), typeof( UpgradeSpecs ) );
		}

		public Type Resolve( uint hash )
		{
			if( this._typeTable.ContainsKey( hash ) )
			{
				return this._typeTable[hash] as Type;
			}

			return null;
		}
	}
}
