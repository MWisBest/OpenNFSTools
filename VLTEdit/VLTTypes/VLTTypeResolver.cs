using NFSTools.LibNFS.Crypto;
using System;
using System.Collections.Generic;

namespace NFSTools.VLTEdit.VLTTypes
{
	public class VLTTypeResolver
	{
		private Dictionary<uint, Type> _typeTable;
		public static VLTTypeResolver Resolver = new VLTTypeResolver();

		public VLTTypeResolver()
		{
			this._typeTable = new Dictionary<uint, Type>();
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Double" ), typeof( EADouble ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Float" ), typeof( EAFloat ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::UInt8" ), typeof( EAUInt8 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::UInt16" ), typeof( EAUInt16 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::UInt32" ), typeof( EAUInt32 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::UInt64" ), typeof( EAUInt64 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Int8" ), typeof( EAInt8 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Int16" ), typeof( EAInt16 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Int32" ), typeof( EAInt32 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Int64" ), typeof( EAInt64 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Bool" ), typeof( EABool ) );
			this._typeTable.Add( JenkinsHash.getHash32( "EA::Reflection::Text" ), typeof( EAText ) );

			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::Types::Matrix" ), typeof( AttribMatrix ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::Types::Vector2" ), typeof( AttribVector2 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::Types::Vector3" ), typeof( AttribVector3 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::Types::Vector4" ), typeof( AttribVector4 ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::StringKey" ), typeof( AttribStringKey ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::RefSpec" ), typeof( AttribRefSpec ) );
			this._typeTable.Add( JenkinsHash.getHash32( "Attrib::Blob" ), typeof( AttribBlob ) );

			this._typeTable.Add( JenkinsHash.getHash32( "AxlePair" ), typeof( AxlePair ) );
			this._typeTable.Add( JenkinsHash.getHash32( "CarBodyMotion" ), typeof( CarBodyMotion ) );
			this._typeTable.Add( JenkinsHash.getHash32( "GCollectionKey" ), typeof( GCollectionKey ) );
			this._typeTable.Add( JenkinsHash.getHash32( "JunkmanMod" ), typeof( JunkmanMod ) );
			this._typeTable.Add( JenkinsHash.getHash32( "UpgradeSpecs" ), typeof( UpgradeSpecs ) );
		}

		public Type Resolve( uint hash )
		{
			if( this._typeTable.ContainsKey( hash ) )
			{
				return this._typeTable[hash];
			}

			return null;
		}
	}
}
