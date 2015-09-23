using System;
using System.Collections.Generic;

namespace VLTEdit
{
	public class TypeMap
	{
		private Dictionary<uint, Type> typeDictionary;
		public static TypeMap instance = new TypeMap();

		public TypeMap()
		{
			this.typeDictionary = new Dictionary<uint, Type>( 24 );
			
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
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::RefSpec" ), typeof( EARefSpec ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::StringKey" ), typeof( EAStringKey ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Matrix" ), typeof( EAMatrix ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector2" ), typeof( EAVector2 ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector3" ), typeof( EAVector3 ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Types::Vector4" ), typeof( EAVector4 ) );
			this.typeDictionary.Add( HashUtil.getHash32( "Attrib::Blob" ), typeof( EABlob ) );
			this.typeDictionary.Add( HashUtil.getHash32( "AxlePair" ), typeof( EAAxlePair ) );
			this.typeDictionary.Add( HashUtil.getHash32( "CarBodyMotion" ), typeof( EACarBodyMotion ) );
			this.typeDictionary.Add( HashUtil.getHash32( "GCollectionKey" ), typeof( EAGCollectionKey ) );
			this.typeDictionary.Add( HashUtil.getHash32( "JunkmanMod" ), typeof( EAJunkmanMod ) );
			this.typeDictionary.Add( HashUtil.getHash32( "UpgradeSpecs" ), typeof( EAUpgradeSpecs ) );
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
