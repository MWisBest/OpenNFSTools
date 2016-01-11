using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

[DefaultMember( "Item" )]
internal class dv
{
	private Hashtable htA; // obf: "a"

	private object a( string A_0 )
	{
		if( this.htA.ContainsKey( A_0 ) )
		{
			return this.htA[A_0];
		}
		return null;
	}

	public string a( string A_0, string A_1 )
	{
		Hashtable hashtable = this.a( A_0.ToLower() ) as Hashtable;
		if( hashtable == null )
		{
			return null;
		}
		return hashtable[A_1.ToLower()] as string;
	}

	public string a( string A_0, int A_1, string A_2 )
	{
		ArrayList arrayList = this.a( A_0.ToLower() ) as ArrayList;
		if( arrayList == null )
		{
			return null;
		}
		Hashtable hashtable = arrayList[A_1] as Hashtable;
		if( hashtable == null )
		{
			return null;
		}
		return hashtable[A_2.ToLower()] as string;
	}

	public int b( string A_0 )
	{
		ArrayList arrayList = this.a( A_0 ) as ArrayList;
		if( arrayList == null )
		{
			return -1;
		}
		return arrayList.Count;
	}

	public void a( string A_0, string[] A_1 )
	{
		ArrayList arrayList = new ArrayList( A_1 );
		StreamReader streamReader = new StreamReader( A_0, Encoding.Default, true );
		Hashtable hashtable = null;
		this.htA = new Hashtable();
		while( true )
		{
			string text = streamReader.ReadLine();
			if( text == null )
			{
				break;
			}
			text = text.Trim();
			if( !text.StartsWith( "#" ) && !text.StartsWith( ";" ) && !( text == "" ) )
			{
				if( text.StartsWith( "[" ) && text.EndsWith( "]" ) )
				{
					string text2;
					if( text != "[]" )
					{
						text2 = text.Substring( 1, text.Length - 2 ).ToLower();
					}
					else
					{
						text2 = "";
					}
					if( this.htA.ContainsKey( text2 ) )
					{
						if( arrayList.Contains( text2 ) )
						{
							hashtable = new Hashtable();
							ArrayList arrayList2 = this.a( text2 ) as ArrayList;
							arrayList2.Add( hashtable );
						}
						else
						{
							hashtable = ( this.a( text2 ) as Hashtable );
						}
					}
					else
					{
						hashtable = new Hashtable();
						if( arrayList.Contains( text2 ) )
						{
							ArrayList arrayList3 = new ArrayList();
							arrayList3.Add( hashtable );
							this.htA.Add( text2, arrayList3 );
						}
						else
						{
							this.htA.Add( text2, hashtable );
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
					hashtable.Add( text3.ToLower(), text4 );
				}
			}
		}
		streamReader.Close();
	}
}
