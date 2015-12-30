using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using VLTEdit.Table;
using VLTEdit.Types;

namespace VLTEdit
{
	public partial class frmDesigner : Form
	{
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
			this.miOpen.Click += new EventHandler( this.menuOpenClicked );
			this.miUnload.Index = 1;
			this.miUnload.Text = "&Unload";
			this.miDivider.Index = 2;
			this.miDivider.Text = "-";
			this.miExit.Index = 3;
			this.miExit.Text = "E&xit";
			this.miExit.Click += new EventHandler( this.menuExitClicked );

			// Setup "Help" properties.
			this.miHelp.Index = 1;
			this.miHelp.MenuItems.Add( this.miHelpText );
			this.miHelp.Text = "&Help";

			// Setup "Help" individual item properties.
			this.miHelpText.Index = 0;
			this.miHelpText.Text = "Sorry, no help here! :)";

			this.classGrid.DataSource = this.classGridDataSet;
		}

		private void exit()
		{
			Application.Exit();
		}

		private void menuOpenClicked( object A_0, EventArgs A_1 )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.AddExtension = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.DefaultExt = "vlt";
			openFileDialog.ShowReadOnly = false;
			openFileDialog.Title = "Open VLT";
			openFileDialog.Filter = "VLT Files (*.vlt)|*.vlt";
			openFileDialog.Multiselect = true;
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				bool flag = false;
				foreach( string filename in openFileDialog.FileNames )
				{
					if( this.loadFile( filename, false ) )
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

		private bool loadFile( string fileName, bool fromConsole )
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
				UnknownAS @as = dh.asa1[i];
				switch( @as.b21 )
				{
					case EntryType.ROOT:
						this.av.am( @as.di1.asRootEntry(), A_0 );
						break;
					case EntryType.CLASS:
						this.av.a( @as.di1.asClassEntry(), A_0 );
						break;
					case EntryType.ROW:
						RowEntry c = @as.di1.asRowEntry();
						VLTClass dq = this.av.genht2[c.ui2];
						dq.dqb1.a( c, A_0 );
						if( i == dh.asa1.Length - 1 )
						{
							dq.dqb1.Trim();
						}
						break;
				}
			}
		}

		private void bFour( object A_0, EventArgs A_1 )
		{
			this.unloadFile( ( A_0 as MenuItem ).Text );
		}

		private void tvRefresh()
		{
			bool flag = true;
			this.classGrid.Visible = false;
			//this.pnlData.Visible = false;
			this.tv.Nodes.Clear();
			if( this.au.Count == 0 )
			{
				return;
			}
			this.tv.BeginUpdate();
			Dictionary<string, TreeNode> dict = new Dictionary<string, TreeNode>();
			TreeNode treeNode = this.tv.Nodes.Add( "Database" );
			treeNode.Tag = this.av;

			foreach( VLTClass dq in this.av )
			{
				string text = HashTracker.getValueForHash( dq.hash );
				treeNode.TreeView.Sorted = true;
				TreeNode treeNode2 = treeNode.Nodes.Add( text );
				treeNode.TreeView.Sorted = false;
				treeNode2.Tag = dq;
				foreach( UnknownDR dr in dq.dqb1 )
				{
					TreeNode treeNode3;
					string text2;
					if( dr.c1.ui3 == 0u )
					{
						treeNode3 = treeNode2;
					}
					else
					{
						text2 = string.Format( "{0:x},{1:x}", dq.hash, dr.c1.ui3 );
						treeNode3 = dict[text2];
					}
					if( treeNode3 == null )
					{
						if( flag )
						{
							DialogResult dialogResult = MessageBox.Show( "Could not find parent data row. Did you forget to load a dependency?\nThe hierarchy will be flattened.", "Warning", MessageBoxButtons.OK ); // TODO: , 48);
							if( dialogResult == DialogResult.Cancel )
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
						dq.c61.i6,
						"+",
						dr.c1.i1,
						"]"
					} );
					treeNode3 = treeNode3.Nodes.Add( text );
					treeNode3.Tag = dr;
					text2 = string.Format( "{0:x},{1:x}", dq.hash, dr.c1.hash );
					dict[text2] = treeNode3;
				}
			}

			this.tv.EndUpdate();
		}

		private void unloadFile( string A_0 )
		{
			foreach( UnknownB0 b in this.au )
			{
				UnknownBA ba = b.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[0];
				if( text == A_0 )
				{
					this.writeToConsole( "Unloading: " + text );
					foreach( MenuItem menuItem in this.miUnload.MenuItems )
					{
						if( menuItem.Text == A_0 )
						{
							this.miUnload.MenuItems.Remove( menuItem );
							break;
						}
					}
					this.au.Remove( b );
					this.d();
					this.tvRefresh();
					break;
				}
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

		private void menuExitClicked( object A_0, EventArgs A_1 )
		{
			this.exit();
		}

		private void onLoad( object sender, EventArgs e )
		{
			this.writeToConsole( "VLTEdit " + Application.ProductVersion );
			this.writeToConsole( "Copyright (C) 2005-2006, Arushan" );
			this.writeToConsole( "Copyright (C) 2015, Kyle \"MWisBest\" Repinski" );
			if( BuildConfig.DEBUG )
			{
				//this.writeTestInfo();
			}
			this.txtConsoleInput.Focus();
		}

		public void writeToConsole( string A_0 )
		{
			if( this.txtConsole.Text != "" )
			{
				this.txtConsole.AppendText( "\r\n" );
			}
			this.txtConsole.AppendText( A_0 );
			this.txtConsole.SelectionStart = this.txtConsole.Text.Length;
			this.txtConsole.Refresh();
		}

		private void consoleCommandHandler( string A_0 )
		{
			// TODO: impl
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
				//this.pnlData.Visible = false;
				VLTClass dq = tag as VLTClass;

				if( !this.classGridDataSet.Tables.Contains( dq.hash.ToString() ) )
				{
					DataTable dataTable = this.classGridDataSet.Tables.Add( dq.hash.ToString() );
					dataTable.Columns.Add( "Name", typeof( string ) );
					dataTable.Columns.Add( "Type", typeof( string ) );
					dataTable.Columns.Add( "Length", typeof( ushort ) );
					dataTable.Columns.Add( "Count", typeof( short ) );

					foreach( VLTClass.aclz1 a in dq )
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = HashTracker.getValueForHash( a.hash );
						dataRow[1] = HashTracker.getValueForHash( a.ui2 );
						dataRow[2] = a.len;
						dataRow[3] = a.count;
						dataTable.Rows.Add( dataRow );
					}
				}

				this.classGrid.DataMember = dq.hash.ToString();
				//this.classGrid.Columns["Name"].Width = 80;
				//this.classGrid.Columns["Type"].Width = 150;
				//this.classGrid.Columns["Length"].Width = 60;
				//this.classGrid.Columns["Count"].Width = 60;

				// TODO: what was this ever for?!
				//UnknownA8 a2 = dq.b01.a( VLTOtherValue.TABLE_END ) as UnknownA8;
				this.classGrid.Update();
			}
			else if( tag is UnknownDR && false ) // TODO: impl, remove false
			{
				//this.lblFieldType.Text = "";
				//this.lblFieldOffset.Text = "";
				//this.dataGrid.DataSource = null;
				//this.dataGrid.Update();
				this.classGrid.Visible = false;
				//this.pnlData.Visible = true;
				string text = "";
				string text2 = "";
				TreeNode treeNode = null;
				//if( this.tvFields.SelectedNode != null )
				{
					//if( this.tvFields.SelectedNode.Parent != null && this.tvFields.SelectedNode.Parent.Tag == null )
					{
						//text = this.tvFields.SelectedNode.Parent.Text;
						//text2 = this.tvFields.SelectedNode.Text;
					}
					//else
					{
						//text = this.tvFields.SelectedNode.Text;
					}
				}
				UnknownDR dr = tag as UnknownDR;
				VLTClass dq2 = dr.dq1;
				//this.tvFields.BeginUpdate();
				//this.tvFields.Nodes.Clear();
				int num = 0;

				foreach( VLTClass.aclz1 a3 in dq2 )
				{
					EABaseType bb = dr.bba1[num++];
					if( !a3.c() || dr.booa1[num - 1] )
					{
						if( a3.isArray() )
						{
							EAArray m = bb as EAArray;
							string text3 = string.Concat( new object[]
							{
									HashTracker.getValueForHash(a3.hash),
									" [",
									m.getMaxEntryCount(),
									"/",
									m.getCurrentEntryCount(),
									"]"
							} );
							TreeNode treeNode2 = null;// this.tvFields.Nodes.Add( text3 );
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
							TreeNode treeNode4 = null;// this.tvFields.Nodes.Add( HashTracker.getValueForHash( a3.hash ) );
							treeNode4.Tag = bb;
							if( treeNode4.Text == text )
							{
								treeNode = treeNode4;
							}
						}
					}
				}

				//if( this.tvFields.Nodes.Count > 0 )
				{
					if( treeNode == null )
					{
						//this.tvFields.SelectedNode = this.tvFields.Nodes[0];
					}
					else
					{
						//this.tvFields.SelectedNode = treeNode;
					}
				}
				//this.tvFields.EndUpdate();
				UnknownA8 a4 = dr.b01.a( VLTOtherValue.TABLE_END ) as UnknownA8;
			}
			else
			{
				this.classGrid.Visible = false;
				//this.pnlData.Visible = false;
			}
		}

		// TODO: remove on frmMain takeover
		private void frmDesigner_FormClosed( object sender, FormClosedEventArgs e )
		{
			Application.Exit();
		}
	}
}
