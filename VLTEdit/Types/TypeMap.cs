using System;
using System.Collections.Generic;
using NFSTools.NFSLib.Crypto;

namespace NFSTools.VLTEdit.Types
{
	public class TypeMap
	{
		private Dictionary<uint, Type> typeDictionary;
		public static TypeMap instance = new TypeMap();

		public TypeMap()
		{
			if( !BuildConfig.CARBON )
			{
				this.typeDictionary = new Dictionary<uint, Type>( 28 );

				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int8" ), typeof( EAInt8 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int16" ), typeof( EAInt16 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int32" ), typeof( EAInt32 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int64" ), typeof( EAInt64 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt8" ), typeof( EAUInt8 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt16" ), typeof( EAUInt16 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt32" ), typeof( EAUInt32 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt64" ), typeof( EAUInt64 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Bool" ), typeof( EABool ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Float" ), typeof( EAFloat ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Double" ), typeof( EADouble ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Text" ), typeof( EAText ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::RefSpec" ), typeof( AttribRefSpec ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::StringKey" ), typeof( AttribStringKey ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Matrix" ), typeof( AttribMatrix ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector2" ), typeof( AttribVector2 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector3" ), typeof( AttribVector3 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector4" ), typeof( AttribVector4 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Blob" ), typeof( AttribBlob ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "AirSupport" ), typeof( AirSupport ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "AxlePair" ), typeof( AxlePair ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "CarBodyMotion" ), typeof( CarBodyMotion ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "CopCountRecord" ), typeof( CopCountRecord ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "GCollectionKey" ), typeof( GCollectionKey ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "HeavySupport" ), typeof( HeavySupport ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "JunkmanMod" ), typeof( JunkmanMod ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "TrafficPatternRecord" ), typeof( TrafficPatternRecord ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "UpgradeSpecs" ), typeof( UpgradeSpecs ) );
			}
			else
			{
				this.typeDictionary = new Dictionary<uint, Type>();

				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int8" ), typeof( EAInt8 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int16" ), typeof( EAInt16 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int32" ), typeof( EAInt32 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Int64" ), typeof( EAInt64 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt8" ), typeof( EAUInt8 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt16" ), typeof( EAUInt16 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt32" ), typeof( EAUInt32 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::UInt64" ), typeof( EAUInt64 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Bool" ), typeof( EABool ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Float" ), typeof( EAFloat ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Double" ), typeof( EADouble ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "EA::Reflection::Text" ), typeof( EAText ) );

				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::RefSpec" ), typeof( AttribRefSpec ) );
				// Carbon axed the Hash64 for Attrib::StringKey.
				// TODO: Refactor Types to allow all classes to know their actual length, to adjust for this stuff dynamically.
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::StringKey" ), typeof( AttribStringKeyCarbon ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Matrix" ), typeof( AttribMatrix ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector2" ), typeof( AttribVector2 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector3" ), typeof( AttribVector3 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Types::Vector4" ), typeof( AttribVector4 ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "Attrib::Blob" ), typeof( AttribBlob ) );

				this.typeDictionary.Add( JenkinsHash.getHash32( "AxlePair" ), typeof( AxlePair ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "CarBodyMotion" ), typeof( CarBodyMotion ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "JunkmanMod" ), typeof( JunkmanMod ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "TrafficPatternRecord" ), typeof( TrafficPatternRecord ) );
				this.typeDictionary.Add( JenkinsHash.getHash32( "UpgradeSpecs" ), typeof( UpgradeSpecs ) );
			}

		}

		public Type getTypeForKey( uint typeHash )
		{
			if( this.typeDictionary.ContainsKey( typeHash ) )
			{
				return this.typeDictionary[typeHash];
			}
			return null;
		}
	}
}
