using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NFSTools.LibNFS.Crypto;
using NFSTools.VLTEdit.Table;
using NFSTools.VLTEdit.Types;

namespace NFSTools.VLTEdit
{
	public partial class frmDesigner : Form
	{
		public static frmDesigner instance;

		private MenuItem miFile;
		private MenuItem miHelp;

		private MenuItem miOpen;
		private MenuItem miUnload;
		private MenuItem miDivider;
		private MenuItem miExit;
		private MenuItem miHelpText;

		private List<UnknownB0> au = new List<UnknownB0>();
		private UnknownDE av = null;

		private DataSet classGridDataSet = new DataSet( "VLT" );

		public frmDesigner()
		{
			InitializeComponent();
			// TODO: Replace 'Menu' with modern alternative(s).
			this.Menu = new MainMenu(); // Create root menu.

			// Create top level tabs.
			this.miFile = new MenuItem();
			this.miHelp = new MenuItem();

			// Create "File" items.
			this.miOpen = new MenuItem();
			this.miUnload = new MenuItem();
			this.miDivider = new MenuItem();
			this.miExit = new MenuItem();

			// Create "Help" items.
			this.miHelpText = new MenuItem();

			// Add top level tabs to main Menu.
			this.Menu.MenuItems.Add( this.miFile );
			this.Menu.MenuItems.Add( this.miHelp );

			// Setup "File" properties.
			this.miFile.Index = 0;
			this.miFile.MenuItems.Add( this.miOpen );
			this.miFile.MenuItems.Add( this.miUnload );
			this.miFile.MenuItems.Add( this.miDivider );
			this.miFile.MenuItems.Add( this.miExit );
			this.miFile.Text = "&File";

			// Setup "File" individual item properties.
			this.miOpen.Index = 0;
			this.miOpen.Text = "&Open";
			this.miOpen.Click += new EventHandler( this.openFile );
			this.miUnload.Index = 1;
			this.miUnload.Text = "&Unload";
			this.miDivider.Index = 2;
			this.miDivider.Text = "-";
			this.miExit.Index = 3;
			this.miExit.Text = "E&xit";
			this.miExit.Click += new EventHandler( this.exit );

			// Setup "Help" properties.
			this.miHelp.Index = 1;
			this.miHelp.MenuItems.Add( this.miHelpText );
			this.miHelp.Text = "&Help";

			// Setup "Help" individual item properties.
			this.miHelpText.Index = 0;
			this.miHelpText.Text = "Sorry, no help here! :)";

			this.classGrid.DataSource = this.classGridDataSet;

			instance = this;
		}

		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new frmDesigner() );
		}

		private void exit( object A_0 = null, EventArgs A_1 = null )
		{
			Application.Exit();
		}

		private void openFile( object A_0 = null, EventArgs A_1 = null )
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.DefaultExt = "vlt";
			ofd.ShowReadOnly = false;
			ofd.Title = "Open VLT";
			ofd.Filter = "VLT Files (*.vlt)|*.vlt";
			ofd.Multiselect = true;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				bool flag = false;
				foreach( string filename in ofd.FileNames )
				{
					if( this.loadFile( filename ) )
					{
						flag = true;
					}
				}
				if( flag )
				{
					this.tvRefresh();
				}
			}
		}

		private bool loadFile( string fileName, bool fromConsole = false )
		{
			bool result = false;
			if( this.av == null )
			{
				this.av = new UnknownDE();
			}
			UnknownB0 b = new UnknownB0();
			this.writeToConsole( "Loading: " + fileName );
			b.a( fileName );
			if( this.bOne( b ) )
			{
				UnknownBA ba = b.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[0];

				MenuItem menuItem = new MenuItem( text, new EventHandler( this.bFour ) );

				// Setting the Name property allows us to use the convenient Key-based methods on the MenuItems collection.
				menuItem.Name = text;

				this.miUnload.MenuItems.Add( menuItem );
				result = true;
			}
			if( fromConsole )
			{
				this.tvRefresh();
			}
			return result;
		}

		// TODO: opt
		private bool bOne( UnknownB0 A_0 )
		{
			UnknownBA ba = A_0.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
			uint num = ba.uia1[0];
			foreach( UnknownB0 b in this.au )
			{
				UnknownBA ba2 = b.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				if( ba2.uia1[0] == num )
				{
					MessageBox.Show( "This VLT data file has already been loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand );
					return false;
				}
			}
			this.aTwo( A_0 );
			this.au.Add( A_0 );
			return true;
		}

		private void aTwo( UnknownB0 A_0 )
		{
			UnknownDH dh = A_0.a( VLTOtherValue.TABLE_START ) as UnknownDH;
			for( int i = 0; i < dh.asa1.Length; ++i )
			{
				TableEntry @as = dh.asa1[i];
				switch( @as.entryType )
				{
					case EntryType.ROOT:
						this.av.am( @as.di1.asRootEntry(), A_0 );
						break;
					case EntryType.CLASS:
						this.av.a( @as.di1.asClassEntry(), A_0 );
						break;
					case EntryType.ROW:
						RowRecord c = @as.di1.asRowEntry();
						VLTClass dq = this.av.genht2[c.ui2];
						dq.dqb1.a( c, A_0 );
						break;
				}
			}
		}

		private void bFour( object A_0, EventArgs A_1 )
		{
			string[] file = { ( A_0 as MenuItem ).Text };
			this.unloadFiles( new List<string>( file ) );
		}

		private void tvRefresh()
		{
			bool flag = true;
			this.classGrid.Visible = false;

			// Clear out the class DataSet, just in case something in the cache changes somehow upon loading a file.
			this.classGridDataSet = new DataSet( "VLT" );
			this.classGrid.DataSource = this.classGridDataSet;

			this.pnlData.Visible = false;
			this.tv.Nodes.Clear();
			if( this.au.Count == 0 )
			{
				return;
			}
			this.tv.BeginUpdate();
			Dictionary<string, TreeNode> dict = new Dictionary<string, TreeNode>();

			foreach( VLTClass dq in this.av )
			{
				string text = HashTracker.getValueForHash( dq.classHash );
				TreeNode treeNode2 = this.tv.Nodes.Add( text );
				treeNode2.Tag = dq;

				foreach( UnknownDR dr in dq.dqb1 )
				{
					TreeNode treeNode3 = null;
					string text2;
					if( dr.c1.ui3 == 0u )
					{
						treeNode3 = treeNode2;
					}
					else
					{
						// This failure seems to be caused by our underlying problem with the VLTClass RowEntry failure.
						text2 = string.Format( "{0:x},{1:x}", dq.classHash, dr.c1.ui3 );
						if( dict.ContainsKey( text2 ) )
						{
							treeNode3 = dict[text2];
						}
					}

					if( treeNode3 == null )
					{
						if( flag )
						{
							DialogResult dialogResult = MessageBox.Show( "Could not find parent data row. Did you forget to load a dependency?\nThe hierarchy will be flattened.", "Warning", MessageBoxButtons.OK ); // TODO: , 48);
							if( dialogResult == DialogResult.OK )
							{
								flag = false;
							}
						}
						treeNode3 = treeNode2;
					}
					text = string.Concat( new object[]
					{
						HashTracker.getValueForHash(dr.c1.hash),
						" [",
						dq.classRecord.i6,
						"+",
						dr.c1.i1,
						"]"
					} );
					treeNode3 = treeNode3.Nodes.Add( text );
					treeNode3.Tag = dr;
					text2 = string.Format( "{0:x},{1:x}", dq.classHash, dr.c1.hash );
					dict[text2] = treeNode3;
				}
			}

			this.tv.Sort();
			this.tv.EndUpdate();
		}

		private void unloadFiles( List<string> fileNames = null )
		{
			bool changesMade = false;

			for( int i = this.au.Count - 1; i >= 0; --i )
			{
				string text = ( this.au[i].a( VLTOtherValue.VLTMAGIC ) as UnknownBA ).sa1[0];

				bool remove = false;
				if( fileNames == null )
				{
					remove = true;
				}
				else if( fileNames.Contains( text ) )
				{
					remove = true;
					fileNames.Remove( text );
				}

				if( remove )
				{
					if( this.miUnload.MenuItems.IndexOfKey( text ) != this.miUnload.MenuItems.Count - 1 )
					{
						this.writeToConsole( "ERROR: Cannot unload \"" + text + "\"; it may be a parent file! Unload last file first." );
					}
					else
					{
						this.writeToConsole( "Unloading: " + text );
						this.miUnload.MenuItems.RemoveByKey( text );
						this.au.RemoveAt( i );
						changesMade = true;
					}
				}

				if( fileNames != null && fileNames.Count == 0 )
				{
					break;
				}
			}

			if( changesMade )
			{
				this.d();
				this.tvRefresh();
			}
		}

		private void d()
		{
			this.av = new UnknownDE();
			foreach( UnknownB0 b in this.au )
			{
				this.aTwo( b );
			}
		}

		private void onLoad( object sender, EventArgs e )
		{
			this.writeToConsole( "VLTEdit " + Application.ProductVersion );
			this.writeToConsole( "Copyright (C) 2005-2006, Arushan" );
			this.writeToConsole( "Copyright (C) 2015-2016, Kyle \"MWisBest\" Repinski" );
			if( BuildConfig.DEBUG )
			{
				this.writeTestInfo();
			}
			this.txtConsoleInput.Focus();
		}

		public void writeTestInfo()
		{
		}

		public void writeToConsole( string A_0 )
		{
			if( this.txtConsole.Text != "" )
			{
				this.txtConsole.AppendText( Environment.NewLine );
			}
			this.txtConsole.AppendText( A_0 );
			this.txtConsole.SelectionStart = this.txtConsole.Text.Length;
			this.txtConsole.Refresh();
		}

		private string a( UnknownDR A_0 )
		{
			try
			{
				VLTClass dq = A_0.dq1;
				string text = HashTracker.getValueForHash( A_0.c1.hash );
				if( A_0.c1.ui3 == 0u )
				{
					return text;
				}
				return this.a( dq.dqb1.a( A_0.c1.ui3 ) ) + "/" + text;
			}
			catch
			{
				return "";
			}
		}

		/**
		 * Searches open files for the specified entry name/hash; stores results in the given List
		 */
		private void search( string A_0, ref List<string> A_1 )
		{
			uint num;
			if( A_0.StartsWith( "0x" ) )
			{
				num = uint.Parse( A_0.Substring( 2 ), NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite );
			}
			else
			{
				num = JenkinsHash.getHash32( A_0 );
			}

			foreach( VLTClass dq in this.av )
			{
				if( dq.classHash == num )
				{
					string text = A_0 + ": Found match for class: " + HashTracker.getValueForHash( dq.classHash );
					if( !A_1.Contains( text ) )
					{
						A_1.Add( text );
					}
				}

				foreach( VLTClass.aclz1 a in dq )
				{
					if( a.hash == num )
					{
						string text = string.Concat( new string[]
						{
							A_0,
							": Found match for field: ",
							HashTracker.getValueForHash(dq.classHash),
							"/",
							HashTracker.getValueForHash(a.hash)
						} );
						if( !A_1.Contains( text ) )
						{
							A_1.Add( text );
						}
					}
				}

				foreach( UnknownDR dr in dq.dqb1 )
				{
					if( dr.c1.hash == num )
					{
						string text = string.Concat( new string[]
						{
							A_0,
							": Found match for row: ",
							HashTracker.getValueForHash(dq.classHash),
							"/",
							this.a(dr)
						} );
						if( !A_1.Contains( text ) )
						{
							A_1.Add( text );
						}
					}
				}
			}
		}

		/**
		 * Searches open files for entries of the specified hash
		 */
		private void search( string A_0 )
		{
			List<string> strList = new List<string>();
			this.search( A_0, ref strList ); // TODO: Check that files are open!
			this.search( A_0.ToLower(), ref strList );
			this.search( A_0.ToUpper(), ref strList );
			/* This was previously meant for being able to use "_" for inverted case searches or something,
			 * but we actually have hashes with "_" in them, so this is too problematic. Try "_default", for example...
			bool flag = true;
			string text = "";
			foreach( char c in A_0.ToCharArray() )
			{
				if( c == '_' )
				{
					flag = true;
				}
				else
				{
					if( flag )
					{
						text += new string( c, 1 ).ToUpper();
						flag = false;
					}
					else
					{
						text += c;
					}
				}
			}
			this.search( text, ref strList );
			this.search( text.ToLower(), ref strList );
			this.search( text.ToUpper(), ref strList );
			*/
			if( strList.Count > 0 )
			{
				foreach( string str in strList )
				{
					this.writeToConsole( str );
				}
			}
			else
			{
				this.writeToConsole( "No matches found." );
			}
		}

		private void consoleCommandHandler( string cmd )
		{
			// TODO: impl
			string[] array = cmd.Split( new char[] { ' ' }, 2 );
			string text = array[0];
			bool noArgs = true;
			string text2 = null;
			if( array.Length > 1 )
			{
				noArgs = false;
				text2 = array[1];
				text2.Split( new char[] { ' ' } );
			}

			try
			{
				switch( text )
				{
					case "quit":
					case "exit":
						this.exit();
						break;
					case "open":
					case "load":
						if( noArgs )
						{
							this.openFile();
							break;
						}
						FileInfo fileInfo = new FileInfo( text2 );
						if( !fileInfo.Exists )
						{
							this.writeToConsole( "Non existant file: " + fileInfo.FullName );
							break;
						}
						if( !this.loadFile( fileInfo.FullName, true ) )
						{
							this.writeToConsole( "Failed to load file: " + fileInfo.FullName );
						}
						break;
					case "unload":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						string[] filesToUnload = { text2 };
						this.unloadFiles( new List<string>( filesToUnload ) );
						break;
					case "unloadall":
						if( !noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.unloadFiles();
						break;
					case "reparse":
						this.writeToConsole( "Reparsing all VLTs..." );
						this.d();
						this.tvRefresh();
						break;
					case "cls":
					case "clear":
						this.txtConsole.Text = "";
						break;
					case "hex":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeToConsole( string.Format( "hex({0})=0x{1:x}", ulong.Parse( text2 ), ulong.Parse( text2 ) ) );
						break;
					case "hash":
					case "hash32":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeToConsole( string.Format( "hash({0})=0x{1:x}", text2, JenkinsHash.getHash32( text2 ) ) );
						break;
					case "hash64":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeToConsole( string.Format( "hash64({0})=0x{1:x}", text2, JenkinsHash.getHash64( text2 ) ) );
						break;
					case "hs":
					case "hsearch":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
						}
						else if( this.au.Count <= 0 ) // No loaded files
						{
							this.writeToConsole( "No files loaded to search!" );
							break;
						}
						else
						{
							this.search( text2 );
						}
						if( text == "hs" )
						{
							this.txtConsoleInput.Text = "hs ";
							this.txtConsoleInput.SelectionStart = this.txtConsoleInput.Text.Length;
						}
						break;
					case "savehash":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						FileInfo fileInfo2 = new FileInfo( text2 );
						HashTracker.dumpUsedHashes( fileInfo2.FullName );
						this.writeToConsole( "Saved used hashes list to: " + fileInfo2.FullName );
						break;
					case "loadhash":
						if( File.Exists( text2 ) )
						{
							HashTracker.loadHashes( text2 );
							break;
						}
						this.writeToConsole( "File does not exist." );
						break;
					case "reloadhashes":
						HashTracker.init();
						this.writeToConsole( "Hashes reloaded." );
						break;
					case "help":
						this.writeToConsole( "Common commands:" );
						this.writeToConsole( "\thash, hash64 <string>: returns the hash(64) of the given string." );
						this.writeToConsole( "\ths, hsearch <string/0xHASH>: searches for VLT entries of the given string or hash." );
						this.writeToConsole( "\thex <int>: returns the hexadecimal representation of the given decimal." );
						this.writeToConsole( "\tcls, clear: clear the console." );
						break;
					case "debug":
						frmMain maintest = new frmMain();
						maintest.Show();
						this.WindowState = FormWindowState.Minimized;
						break;
					case "":
						break;
					default:
						this.writeToConsole( "Unknown command." );
						break;
				}
			}
			catch( Exception ex2 )
			{
				this.writeToConsole( "Exception: " + ex2.ToString() );
				this.writeToConsole( "Error while executing: " + cmd );
			}
		}

		private void txtConsoleInput_KeyPress( object sender, KeyPressEventArgs e )
		{
			if( e.KeyChar == '\r' )
			{
				string text = this.txtConsoleInput.Text;
				if( text != "" )
				{
					this.txtConsoleInput.Text = "";
					this.txtConsoleInput.Refresh();
					this.writeToConsole( "> " + text );
					this.consoleCommandHandler( text );
					this.txtConsoleInput.Focus();
					e.Handled = true;
				}
			}
		}

		private void tv_AfterSelect( object sender, TreeViewEventArgs e )
		{
			object tag = e.Node.Tag;
			if( tag is VLTClass )
			{
				this.classGrid.Visible = true;
				this.pnlData.Visible = false;
				VLTClass dq = tag as VLTClass;

				if( !this.classGridDataSet.Tables.Contains( dq.classHash.ToString() ) )
				{
					DataTable dataTable = this.classGridDataSet.Tables.Add( dq.classHash.ToString() );
					dataTable.Columns.Add( "Name", typeof( string ) );
					dataTable.Columns.Add( "Type", typeof( string ) );
					dataTable.Columns.Add( "Length", typeof( ushort ) );
					dataTable.Columns.Add( "Count", typeof( short ) );

					foreach( VLTClass.aclz1 a in dq )
					{
						/*
						object[] rowData =
						{
							HashTracker.getValueForHash( a.hash ),
							HashTracker.getValueForHash( a.ui2 ),
							a.len,
							a.count
						};
						dataTable.Rows.Add( rowData );*/
						dataTable.Rows.Add( new object[] {
							HashTracker.getValueForHash( a.hash ),
							HashTracker.getValueForHash( a.ui2 ),
							a.len,
							a.count
						} );
						/*
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = HashTracker.getValueForHash( a.hash );
						dataRow[1] = HashTracker.getValueForHash( a.ui2 );
						dataRow[2] = a.len;
						dataRow[3] = a.count;
						dataTable.Rows.Add( dataRow );*/
					}
				}

				this.classGrid.DataMember = dq.classHash.ToString();
				//this.classGrid.Columns["Name"].Width = 80;
				//this.classGrid.Columns["Type"].Width = 150;
				//this.classGrid.Columns["Length"].Width = 60;
				//this.classGrid.Columns["Count"].Width = 60;

				this.classGrid.Update();
			}
			else if( tag is UnknownDR ) // TODO
			{
				this.lblFieldType.Text = "";
				this.lblFieldOffset.Text = "";
				this.dataGrid.DataSource = null;
				this.dataGrid.Update();
				this.classGrid.Visible = false;
				this.pnlData.Visible = true;
				string text = "";
				string text2 = "";
				TreeNode treeNode = null;
				if( this.tvFields.SelectedNode != null )
				{
					if( this.tvFields.SelectedNode.Parent != null && this.tvFields.SelectedNode.Parent.Tag == null )
					{
						text = this.tvFields.SelectedNode.Parent.Text;
						text2 = this.tvFields.SelectedNode.Text;
					}
					else
					{
						text = this.tvFields.SelectedNode.Text;
					}
				}
				UnknownDR dr = tag as UnknownDR;
				VLTClass dq2 = dr.dq1;
				this.tvFields.BeginUpdate();
				this.tvFields.Nodes.Clear();
				int num = 0;

				foreach( VLTClass.aclz1 a3 in dq2 )
				{
					VLTBaseType bb = dr.bba1[num++];
					if( !a3.c() || dr.booa1[num - 1] )
					{
						if( a3.isArray() )
						{
							VLTArrayType m = bb as VLTArrayType;
							string text3 = string.Concat( new object[]
							{
									HashTracker.getValueForHash(a3.hash),
									" [",
									m.getMaxEntryCount(),
									"/",
									m.getCurrentEntryCount(),
									"]"
							} );
							TreeNode treeNode2 = this.tvFields.Nodes.Add( text3 );
							treeNode2.Tag = bb;
							for( int i = 0; i < m.getMaxEntryCount(); ++i )
							{
								TreeNode treeNode3 = treeNode2.Nodes.Add( "[" + i + "]" );
								treeNode3.Tag = m.genlist[i];
								if( treeNode2.Text == text && treeNode3.Text == text2 )
								{
									treeNode = treeNode3;
								}
							}
							if( treeNode2.Text == text && treeNode == null )
							{
								treeNode = treeNode2;
							}
						}
						else
						{
							TreeNode treeNode4 = this.tvFields.Nodes.Add( HashTracker.getValueForHash( a3.hash ) );
							treeNode4.Tag = bb;
							if( treeNode4.Text == text )
							{
								treeNode = treeNode4;
							}
						}
					}
				}

				if( this.tvFields.Nodes.Count > 0 )
				{
					if( treeNode == null )
					{
						this.tvFields.SelectedNode = this.tvFields.Nodes[0];
					}
					else
					{
						this.tvFields.SelectedNode = treeNode;
					}
				}
				this.tvFields.EndUpdate();
			}
			else
			{
				this.classGrid.Visible = false;
				this.pnlData.Visible = false;
			}
		}

		// TODO: remove on frmMain takeover
		private void frmDesigner_FormClosed( object sender, FormClosedEventArgs e )
		{
			Application.Exit();
		}

		private void tvFields_AfterSelect( object sender, TreeViewEventArgs e )
		{
			object tag = e.Node.Tag;
			if( tag is VLTBaseType && !( tag is VLTArrayType ) )
			{
				VLTBaseType bb = tag as VLTBaseType;
				//bb.l(); // MW: TODO: What is l() supposed to be?
				DataSet dataSet = new DataSet( "DataItem" );
				DataTable dataTable = dataSet.Tables.Add( "Values" );
				dataTable.Columns.Add( "Name", typeof( string ) );
				dataTable.Columns.Add( "Value", typeof( string ) );
				Type type = bb.GetType();
				FieldInfo[] fields = type.GetFields();
				foreach( FieldInfo fieldInfo in fields )
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes( typeof( DataValueAttribute ), false );
					if( customAttributes != null && customAttributes.Length == 1 && customAttributes[0] is DataValueAttribute )
					{
						DataValueAttribute dataValueAttribute = customAttributes[0] as DataValueAttribute;
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = dataValueAttribute.Name;
						object value = fieldInfo.GetValue( bb );
						if( value == null )
						{
							dataRow[1] = "(null)";
						}
						else
						{
							if( dataValueAttribute.Hex )
							{
								dataRow[1] = string.Format( "0x{0:x}", value );
							}
							else
							{
								dataRow[1] = value.ToString();
							}
						}
						dataTable.Rows.Add( dataRow );
					}
				}
				this.dataGrid.DataSource = dataSet;
				this.dataGrid.DataMember = "Values";

				this.lblFieldType.Text = "Type: " + HashTracker.getValueForHash( bb.typeHash );
				/*
				if( BuildConfig.DEBUG )
				{
					this.writeToConsole( "bb.GetType(): " + type.ToString() ); // Here, we're getting the proper type! Great!
					this.writeToConsole( "bb.j(): " + string.Format( "0x{0:x}", bb.ui2 ) ); // Here, we're derping! OMG!
				}*/
				UnknownBA ba = bb.dr1.b01.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[0];
				this.lblFieldOffset.Text = string.Format( "Offset: {0}:0x{1:x}  ({2})", bb.isVltOffset ? "vlt" : "bin", bb.ui1, text );
				this.dataGrid.Update();
			}
			else
			{
				this.lblFieldType.Text = "";
				this.lblFieldOffset.Text = "";
				this.dataGrid.DataSource = null;
				this.dataGrid.Update();
			}
		}
	}
}
