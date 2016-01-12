using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

[DefaultMember( "Item" )]
internal class DefinitionParser
{
	//private Hashtable htA; // obf: "a"
	private Dictionary<string, object> dictB;

	private object a( string A_0 )
	{
		if( this.dictB.ContainsKey( A_0 ) )
		{
			return this.dictB[A_0];
		}
		return null;
	}

	public string a( string A_0, string A_1 )
	{
		Dictionary<string, string> stringDict = this.a( A_0.ToLower() ) as Dictionary<string, string>;
		if( stringDict == null || !stringDict.ContainsKey( A_1.ToLower() ) )
		{
			return null;
		}
		return stringDict[A_1.ToLower()];
	}

	public string a( string A_0, int A_1, string A_2 )
	{
		List<Dictionary<string, string>> listDictLol = this.a( A_0.ToLower() ) as List<Dictionary<string, string>>;
		if( listDictLol == null )
		{
			return null;
		}
		Dictionary<string, string> stringDict = listDictLol[A_1];
		if( stringDict == null || !stringDict.ContainsKey( A_2.ToLower() ) )
		{
			return null;
		}
		return stringDict[A_2.ToLower()];
	}

	public int b( string A_0 )
	{
		List<Dictionary<string, string>> listDictLol = this.a( A_0 ) as List<Dictionary<string, string>>;
		if( listDictLol == null )
		{
			return -1;
		}
		return listDictLol.Count;
	}

	public void a( string A_0, string[] A_1 )
	{
		using( StreamReader streamReader = new StreamReader( A_0, Encoding.Default, true ) )
		{
			List<string> stringList = new List<string>( A_1 );
			Dictionary<string, string> stringDict = null;
			this.dictB = new Dictionary<string, object>();
			string text = null;
			while( ( text = streamReader.ReadLine() ) != null )
			{
				text = text.Trim();
				if( !text.StartsWith( "#" ) && !text.StartsWith( ";" ) && !( text == "" ) )
				{
					if( text.StartsWith( "[" ) && text.EndsWith( "]" ) )
					{
						string text2 = "";
						if( text != "[]" )
						{
							text2 = text.Substring( 1, text.Length - 2 ).ToLower();
						}
						if( this.dictB.ContainsKey( text2 ) )
						{
							if( stringList.Contains( text2 ) )
							{
								stringDict = new Dictionary<string, string>();
								List<Dictionary<string, string>> listDictLol = this.a( text2 ) as List<Dictionary<string, string>>;
								listDictLol.Add( stringDict );
							}
							else
							{
								stringDict = ( this.a( text2 ) as Dictionary<string, string> );
							}
						}
						else
						{
							stringDict = new Dictionary<string, string>();
							if( stringList.Contains( text2 ) )
							{
								List<Dictionary<string, string>> listDictLol2 = new List<Dictionary<string, string>>();
								listDictLol2.Add( stringDict );
								this.dictB.Add( text2, listDictLol2 );
							}
							else
							{
								this.dictB.Add( text2, stringDict );
							}
						}
					}
					else if( text.IndexOf( "=" ) > -1 )
					{
						string[] array = text.Split( new char[]
						{
							'='
						}, 2 );
						string text3 = array[0].Trim();
						string text4 = "";
						if( array.Length > 1 )
						{
							text4 = array[1].Trim();
						}
						stringDict.Add( text3.ToLower(), text4 );
					}
				}
			}
		}
	}
}
