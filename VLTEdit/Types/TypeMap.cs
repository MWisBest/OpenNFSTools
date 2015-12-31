using System;
using System.Collections.Generic;

namespace VLTEdit.Types
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

				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Int8" ), typeof( EAInt8 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Int16" ), typeof( EAInt16 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Int32" ), typeof( EAInt32 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Int64" ), typeof( EAInt64 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::UInt8" ), typeof( EAUInt8 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::UInt16" ), typeof( EAUInt16 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::UInt32" ), typeof( EAUInt32 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::UInt64" ), typeof( EAUInt64 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Bool" ), typeof( EABool ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Float" ), typeof( EAFloat ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Double" ), typeof( EADouble ) );
				this.typeDictionary.Add( HashUtil.getHash32( "EA::Reflection::Text" ), typeof( EAText ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::RefSpec" ), typeof( AttribRefSpec ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::StringKey" ), typeof( AttribStringKey ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Matrix" ), typeof( AttribMatrix ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector2" ), typeof( AttribVector2 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector3" ), typeof( AttribVector3 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector4" ), typeof( AttribVector4 ) );
				this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Blob" ), typeof( AttribBlob ) );
				this.typeDictionary.Add( HashUtil.getHash32( "AirSupport" ), typeof( AirSupport ) );
				this.typeDictionary.Add( HashUtil.getHash32( "AxlePair" ), typeof( AxlePair ) );
				this.typeDictionary.Add( HashUtil.getHash32( "CarBodyMotion" ), typeof( CarBodyMotion ) );
				this.typeDictionary.Add( HashUtil.getHash32( "CopCountRecord" ), typeof( CopCountRecord ) );
				this.typeDictionary.Add( HashUtil.getHash32( "GCollectionKey" ), typeof( GCollectionKey ) );
				this.typeDictionary.Add( HashUtil.getHash32( "HeavySupport" ), typeof( HeavySupport ) );
				this.typeDictionary.Add( HashUtil.getHash32( "JunkmanMod" ), typeof( JunkmanMod ) );
				this.typeDictionary.Add( HashUtil.getHash32( "TrafficPatternRecord" ), typeof( TrafficPatternRecord ) );
				this.typeDictionary.Add( HashUtil.getHash32( "UpgradeSpecs" ), typeof( UpgradeSpecs ) );
			}
			else
			{
				this.typeDictionary = new Dictionary<uint, Type>();

				// Need a dummy type here at least
				this.typeDictionary.Add( HashUtil.getHash32( "LolJunkNotReal" ), typeof( EAUInt32 ) );
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
