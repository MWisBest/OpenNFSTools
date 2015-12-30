using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VLTEdit.Table;
using VLTEdit.Types;

namespace VLTEdit
{
	public class frmMain : Form
	{
		private MenuItem miFile;
		private MenuItem miOpen;
		private MenuItem miExit;
		private TreeView tv;
		private DataGrid classGrid;
		private DataGridTextBoxColumn dgtbc1;
		private DataGridTextBoxColumn dgtbc2;
		private DataGridTextBoxColumn dgtbc3;
		private DataGridTextBoxColumn dgtbc4;
		private DataGridTextBoxColumn dgtbc5;
		private DataGridTextBoxColumn dgtbc6;
		private DataGridTableStyle dgts1;
		private Panel pnlData;
		private DataGrid dataGrid;
		private DataGridTableStyle dgts2;
		private DataGridTextBoxColumn dgtbc7;
		private DataGridTextBoxColumn dgtbc8;
		private TreeView tvFields;
		private Splitter splitter1;
		private MenuItem miUnload;
		private Splitter splitter2;
		private MenuItem miHelp;
		private Panel pnlFieldInfo;
		private Label lblFieldType;
		private Label lblFieldOffset;
		private DataGridTextBoxColumn dgtbc9;
		private Splitter splitter3;
		private TextBox txtConsole;
		private TextBox txtConsoleInput;
		private Panel pnlBottom;
		private MainMenu ae;
		private ContextMenu af;
		private MenuItem mi6;
		private MenuItem mi7;
		private ContextMenu ai;
		private MenuItem mi8;
		private MenuItem ak;
		private MenuItem an;
		private MenuItem ao;
		private MenuItem ap;
		private MenuItem aq;
		private MenuItem ar;
		private Container at = null;
		private List<UnknownB0> au = new List<UnknownB0>();
		private UnknownDE av = null;

		private Dictionary<string, Thread> bruteforceThreads = new Dictionary<string, Thread>();

		public frmMain()
		{
			this.g();
		}

		protected override void Dispose( bool A_0 )
		{
			if( A_0 && this.at != null )
			{
				this.at.Dispose();
			}
			base.Dispose( A_0 );
		}

		private void g()
		{
			this.ae = new MainMenu();
			this.miFile = new MenuItem();
			this.miOpen = new MenuItem();
			this.miUnload = new MenuItem();
			this.mi8 = new MenuItem();
			this.miExit = new MenuItem();
			this.miHelp = new MenuItem();
			this.ak = new MenuItem();
			this.tv = new TreeView();
			this.af = new ContextMenu();
			this.mi6 = new MenuItem();
			this.mi7 = new MenuItem();
			this.classGrid = new DataGrid();
			this.dgts1 = new DataGridTableStyle();
			this.dgtbc2 = new DataGridTextBoxColumn();
			this.dgtbc1 = new DataGridTextBoxColumn();
			this.dgtbc3 = new DataGridTextBoxColumn();
			this.dgtbc4 = new DataGridTextBoxColumn();
			this.dgtbc6 = new DataGridTextBoxColumn();
			this.dgtbc9 = new DataGridTextBoxColumn();
			this.dgtbc5 = new DataGridTextBoxColumn();
			this.pnlData = new Panel();
			this.dataGrid = new DataGrid();
			this.dgts2 = new DataGridTableStyle();
			this.dgtbc7 = new DataGridTextBoxColumn();
			this.dgtbc8 = new DataGridTextBoxColumn();
			this.pnlFieldInfo = new Panel();
			this.lblFieldOffset = new Label();
			this.lblFieldType = new Label();
			this.splitter2 = new Splitter();
			this.tvFields = new TreeView();
			this.ai = new ContextMenu();
			this.an = new MenuItem();
			this.ap = new MenuItem();
			this.aq = new MenuItem();
			this.ar = new MenuItem();
			this.ao = new MenuItem();
			this.splitter1 = new Splitter();
			this.pnlBottom = new Panel();
			this.txtConsole = new TextBox();
			this.txtConsoleInput = new TextBox();
			this.splitter3 = new Splitter();
			this.classGrid.BeginInit();
			this.pnlData.SuspendLayout();
			this.dataGrid.BeginInit();
			this.pnlFieldInfo.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			base.SuspendLayout();
			this.ae.MenuItems.AddRange( new MenuItem[]
			{
				this.miFile,
				this.miHelp
			} );
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange( new MenuItem[]
			{
				this.miOpen,
				this.miUnload,
				this.mi8,
				this.miExit
			} );
			this.miFile.Text = "&File";
			this.miOpen.Index = 0;
			this.miOpen.Text = "&Open";
			this.miOpen.Click += new EventHandler( this.menuOpenClicked );
			this.miUnload.Index = 1;
			this.miUnload.Text = "&Unload";
			this.mi8.Index = 2;
			this.mi8.Text = "-";
			this.miExit.Index = 3;
			this.miExit.Text = "E&xit";
			this.miExit.Click += new EventHandler( this.menuExitClicked );
			this.miHelp.Index = 1;
			this.miHelp.MenuItems.AddRange( new MenuItem[]
			{
				this.ak
			} );
			this.miHelp.Text = "&Help";
			this.ak.Index = 0;
			this.ak.Text = "Sorry, no help here! :)";
			this.tv.BackColor = SystemColors.Window;
			this.tv.ContextMenu = this.af;
			this.tv.Dock = DockStyle.Left;
			this.tv.HideSelection = false;
			this.tv.ImageIndex = -1;
			this.tv.Location = new Point( 0, 0 );
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = -1;
			this.tv.Size = new Size( 200, 437 );
			this.tv.TabIndex = 1;
			this.tv.AfterSelect += new TreeViewEventHandler( this.tv_AfterSelect );
			this.af.MenuItems.AddRange( new MenuItem[]
			{
				this.mi6,
				this.mi7
			} );
			this.mi6.Index = 0;
			this.mi6.Text = "Copy Node Name";
			this.mi6.Click += new EventHandler( this.j );
			this.mi7.Index = 1;
			this.mi7.Text = "Copy Node Path";
			this.mi7.Click += new EventHandler( this.i );
			this.classGrid.CaptionBackColor = SystemColors.Control;
			this.classGrid.CaptionVisible = false;
			this.classGrid.DataMember = "";
			this.classGrid.Dock = DockStyle.Fill;
			this.classGrid.HeaderForeColor = SystemColors.ControlText;
			this.classGrid.Location = new Point( 204, 0 );
			this.classGrid.Name = "classGrid";
			this.classGrid.Size = new Size( 648, 437 );
			this.classGrid.TabIndex = 2;
			this.classGrid.TableStyles.AddRange( new DataGridTableStyle[]
			{
				this.dgts1
			} );
			this.classGrid.Visible = false;
			this.dgts1.DataGrid = this.classGrid;
			this.dgts1.GridColumnStyles.AddRange( new DataGridColumnStyle[]
			{
				this.dgtbc2,
				this.dgtbc1,
				this.dgtbc3,
				this.dgtbc4,
				this.dgtbc6,
				this.dgtbc9,
				this.dgtbc5
			} );
			this.dgts1.HeaderForeColor = SystemColors.ControlText;
			this.dgts1.MappingName = "Fields";
			this.dgts1.ReadOnly = true;
			this.dgtbc2.Format = "";
			this.dgtbc2.FormatInfo = null;
			this.dgtbc2.HeaderText = "Name";
			this.dgtbc2.MappingName = "Name";
			this.dgtbc2.Width = 80;
			this.dgtbc1.Format = "";
			this.dgtbc1.FormatInfo = null;
			this.dgtbc1.HeaderText = "Type";
			this.dgtbc1.MappingName = "Type";
			this.dgtbc1.Width = 150;
			this.dgtbc3.Format = "";
			this.dgtbc3.FormatInfo = null;
			this.dgtbc3.HeaderText = "Length";
			this.dgtbc3.MappingName = "Length";
			this.dgtbc3.Width = 60;
			this.dgtbc4.Format = "";
			this.dgtbc4.FormatInfo = null;
			this.dgtbc4.HeaderText = "Offset";
			this.dgtbc4.MappingName = "Offset";
			this.dgtbc4.Width = 60;
			this.dgtbc6.Format = "";
			this.dgtbc6.FormatInfo = null;
			this.dgtbc6.HeaderText = "Count";
			this.dgtbc6.MappingName = "Count";
			this.dgtbc6.Width = 60;
			this.dgtbc9.Format = "";
			this.dgtbc9.FormatInfo = null;
			this.dgtbc9.HeaderText = "Alignment";
			this.dgtbc9.MappingName = "Alignment";
			this.dgtbc9.Width = 60;
			this.dgtbc5.Format = "";
			this.dgtbc5.FormatInfo = null;
			this.dgtbc5.HeaderText = "Flags";
			this.dgtbc5.MappingName = "Flags";
			this.dgtbc5.Width = 60;
			this.pnlData.Controls.Add( this.dataGrid );
			this.pnlData.Controls.Add( this.pnlFieldInfo );
			this.pnlData.Controls.Add( this.splitter2 );
			this.pnlData.Controls.Add( this.tvFields );
			this.pnlData.Dock = DockStyle.Fill;
			this.pnlData.Location = new Point( 204, 0 );
			this.pnlData.Name = "pnlData";
			this.pnlData.Size = new Size( 648, 437 );
			this.pnlData.TabIndex = 3;
			this.pnlData.Visible = false;
			this.dataGrid.CaptionVisible = false;
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = DockStyle.Fill;
			this.dataGrid.HeaderForeColor = SystemColors.ControlText;
			this.dataGrid.Location = new Point( 180, 0 );
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new Size( 468, 389 );
			this.dataGrid.TabIndex = 1;
			this.dataGrid.TableStyles.AddRange( new DataGridTableStyle[]
			{
				this.dgts2
			} );
			this.dgts2.DataGrid = this.dataGrid;
			this.dgts2.GridColumnStyles.AddRange( new DataGridColumnStyle[]
			{
				this.dgtbc7,
				this.dgtbc8
			} );
			this.dgts2.HeaderForeColor = SystemColors.ControlText;
			this.dgts2.MappingName = "Values";
			this.dgts2.ReadOnly = true;
			this.dgtbc7.Format = "";
			this.dgtbc7.FormatInfo = null;
			this.dgtbc7.HeaderText = "Name";
			this.dgtbc7.MappingName = "Name";
			this.dgtbc7.Width = 75;
			this.dgtbc8.Format = "";
			this.dgtbc8.FormatInfo = null;
			this.dgtbc8.HeaderText = "Value";
			this.dgtbc8.MappingName = "Value";
			this.dgtbc8.Width = 300;
			this.pnlFieldInfo.Controls.Add( this.lblFieldOffset );
			this.pnlFieldInfo.Controls.Add( this.lblFieldType );
			this.pnlFieldInfo.Dock = DockStyle.Bottom;
			this.pnlFieldInfo.Location = new Point( 180, 389 );
			this.pnlFieldInfo.Name = "pnlFieldInfo";
			this.pnlFieldInfo.Size = new Size( 468, 48 );
			this.pnlFieldInfo.TabIndex = 5;
			this.lblFieldOffset.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.lblFieldOffset.Location = new Point( 8, 28 );
			this.lblFieldOffset.Name = "lblFieldOffset";
			this.lblFieldOffset.Size = new Size( 456, 16 );
			this.lblFieldOffset.TabIndex = 6;
			this.lblFieldOffset.TextAlign = ContentAlignment.MiddleLeft;
			this.lblFieldType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.lblFieldType.Location = new Point( 8, 8 );
			this.lblFieldType.Name = "lblFieldType";
			this.lblFieldType.Size = new Size( 456, 16 );
			this.lblFieldType.TabIndex = 5;
			this.lblFieldType.TextAlign = ContentAlignment.MiddleLeft;
			this.splitter2.Location = new Point( 176, 0 );
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new Size( 4, 437 );
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			this.tvFields.BackColor = SystemColors.Window;
			this.tvFields.ContextMenu = this.ai;
			this.tvFields.Dock = DockStyle.Left;
			this.tvFields.FullRowSelect = true;
			this.tvFields.HideSelection = false;
			this.tvFields.ImageIndex = -1;
			this.tvFields.Location = new Point( 0, 0 );
			this.tvFields.Name = "tvFields";
			this.tvFields.SelectedImageIndex = -1;
			this.tvFields.Size = new Size( 176, 437 );
			this.tvFields.TabIndex = 0;
			this.tvFields.AfterSelect += new TreeViewEventHandler( this.tvFields_AfterSelect );
			this.ai.MenuItems.AddRange( new MenuItem[]
			{
				this.an,
				this.ap,
				this.aq,
				this.ar,
				this.ao
			} );
			this.an.Index = 0;
			this.an.Text = "Copy Field Name";
			this.an.Click += new EventHandler( this.h );
			this.ap.Index = 1;
			this.ap.Text = "Copy Offset";
			this.ap.Click += new EventHandler( this.g );
			this.aq.Index = 2;
			this.aq.Text = "Copy Type:Offset";
			this.aq.Click += new EventHandler( this.f );
			this.ar.Index = 3;
			this.ar.Text = "-";
			this.ao.Index = 4;
			this.ao.Text = "Dump Field";
			this.ao.Click += new EventHandler( this.e );
			this.splitter1.Location = new Point( 200, 0 );
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new Size( 4, 437 );
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			this.pnlBottom.Controls.Add( this.txtConsole );
			this.pnlBottom.Controls.Add( this.txtConsoleInput );
			this.pnlBottom.Dock = DockStyle.Bottom;
			this.pnlBottom.Location = new Point( 0, 441 );
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new Size( 852, 152 );
			this.pnlBottom.TabIndex = 0;
			this.txtConsole.BackColor = SystemColors.Window;
			this.txtConsole.Dock = DockStyle.Fill;
			this.txtConsole.Font = new Font( "Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0 );
			this.txtConsole.ForeColor = SystemColors.WindowText;
			this.txtConsole.Location = new Point( 0, 0 );
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = ScrollBars.Vertical;
			this.txtConsole.Size = new Size( 852, 132 );
			this.txtConsole.TabIndex = 1;
			this.txtConsole.Text = "";
			this.txtConsoleInput.AcceptsReturn = true;
			this.txtConsoleInput.Dock = DockStyle.Bottom;
			this.txtConsoleInput.Font = new Font( "Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0 );
			this.txtConsoleInput.Location = new Point( 0, 132 );
			this.txtConsoleInput.Name = "txtConsoleInput";
			this.txtConsoleInput.Size = new Size( 852, 20 );
			this.txtConsoleInput.TabIndex = 0;
			this.txtConsoleInput.Text = "";
			this.txtConsoleInput.KeyPress += new KeyPressEventHandler( this.onTextEntry );
			this.splitter3.Dock = DockStyle.Bottom;
			this.splitter3.Location = new Point( 0, 437 );
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new Size( 852, 4 );
			this.splitter3.TabIndex = 5;
			this.splitter3.TabStop = false;
			this.AutoScaleBaseSize = new Size( 5, 13 );
			base.ClientSize = new Size( 852, 593 );
			base.Controls.Add( this.pnlData );
			base.Controls.Add( this.classGrid );
			base.Controls.Add( this.splitter1 );
			base.Controls.Add( this.tv );
			base.Controls.Add( this.splitter3 );
			base.Controls.Add( this.pnlBottom );
			base.Menu = this.ae;
			base.Name = "MainForm";
			this.Text = "VLTEdit";
			base.Load += new EventHandler( this.onLoad );
			this.classGrid.EndInit();
			this.pnlData.ResumeLayout( false );
			this.dataGrid.EndInit();
			this.pnlFieldInfo.ResumeLayout( false );
			this.pnlBottom.ResumeLayout( false );
			base.ResumeLayout( false );
		}

		private void exit()
		{
			Application.Exit();
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

		private void showOpenFileDialog()
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
				foreach( string a_ in openFileDialog.FileNames )
				{
					if( this.loadFile( a_, false ) )
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

		private void d()
		{
			this.av = new UnknownDE();
			foreach( UnknownB0 b in this.au )
			{
				this.aTwo( b );
			}
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

		private void listLoadedFiles()
		{
			foreach( UnknownB0 b in this.au )
			{
				UnknownBA ba = b.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				this.writeToConsole( ba.sa1[0] );
			}
		}

		private void f( string A_0 )
		{
			foreach( UnknownB0 b in this.au )
			{
				UnknownBA ba = b.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[0];
				if( text == A_0 )
				{
					this.writeToConsole( "Items in " + text );
					UnknownDH dh = b.a( VLTOtherValue.TABLE_START ) as UnknownDH;
					foreach( UnknownAS @as in dh )
					{
						EntryType b2 = @as.b21;
						switch( @as.b21 )
						{
							case EntryType.CLASS:
								this.writeToConsole( "- Class: " + HashTracker.getValueForHash( @as.ui1 ) );
								break;
							case EntryType.ROW:
								RowEntry c = @as.di1.asRowEntry();
								this.writeToConsole( "- Row: " + HashTracker.getValueForHash( c.ui2 ) + "/" + this.a( this.av.genht2[c.ui2].dqb1.a( c.hash ) ) );
								break;
							case EntryType.ROOT:
								this.writeToConsole( "- Database" );
								break;
						}
					}
					break;
				}
			}
		}

		// TODO: Opt
		private void tvRefresh()
		{
			bool flag = true;
			this.classGrid.Visible = false;
			this.pnlData.Visible = false;
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

		private string a( UnknownDR A_0 )
		{
			VLTClass dq = A_0.dq1;
			string text = HashTracker.getValueForHash( A_0.c1.hash );
			if( A_0.c1.ui3 == 0u )
			{
				return text;
			}
			return this.a( dq.dqb1.a( A_0.c1.ui3 ) ) + "/" + text;
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
				num = HashUtil.getHash32( A_0 );
			}

			foreach( VLTClass dq in this.av )
			{
				if( dq.hash == num )
				{
					string text = A_0 + ": Found match for class: " + HashTracker.getValueForHash( dq.hash );
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
									HashTracker.getValueForHash(dq.hash),
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
									HashTracker.getValueForHash(dq.hash),
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

		private void a( VLTClass A_0, string A_1 )
		{
			this.writeToConsole( string.Format( "Dumping contents of class: {0}, field: {1}", HashTracker.getValueForHash( A_0.hash ), A_1 ) );
			foreach( UnknownDR dr in A_0.dqb1 )
			{
				if( dr.c1.ui3 == 0u )
				{
					this.a( dr, A_1 );
				}
			}
		}

		private void a( UnknownDR A_0, string A_1 )
		{
			uint a_;
			if( A_1.StartsWith( "0x" ) )
			{
				a_ = uint.Parse( A_1.Substring( 2 ), NumberStyles.AllowHexSpecifier | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite );
			}
			else
			{
				a_ = HashUtil.getHash32( A_1 );
			}
			this.a( A_0, a_ );
		}

		private void a( UnknownDR A_0, uint A_1 )
		{
			VLTClass dq = A_0.dq1;
			int num = dq.a( A_1 );
			if( num == -1 )
			{
				throw new Exception( "Not a valid field value." );
			}
			string text = this.a( A_0 );
			object obj = A_0.bba1[num];
			if( obj is EAArray )
			{
				EAArray m = obj as EAArray;
				for( int i = 0; i < m.getMaxEntryCount(); ++i )
				{
					obj = m.genlist[i];
					string text2;
					if( obj == null )
					{
						text2 = "(non-existant)";
					}
					else
					{
						text2 = obj.ToString();
					}
					this.writeToConsole( string.Format( "{0}[{1}]: {2}", text, i, text2 ) );
				}
			}
			else
			{
				string text3;
				if( obj == null )
				{
					text3 = "(non-existant)";
				}
				else
				{
					text3 = obj.ToString();
				}
				this.writeToConsole( string.Format( "{0}: {1}", text, text3 ) );
			}
			foreach( UnknownDR dr in dq.dqb1 )
			{
				if( dr.c1.ui3 == A_0.c1.hash )
				{
					this.a( dr, A_1 );
				}
			}
		}

		private bool c( string A_0 )
		{
			return !A_0.StartsWith( "unk_" );
		}

		private string bThree( string text )
		{
			if( text.EndsWith( "*" ) )
			{
				text = text.Substring( 0, text.Length - 1 );
			}
			if( text.StartsWith( "0x" ) )
			{
				return "unk_" + text;
			}
			else if( text == "default" || text == "null" || ( text.ToCharArray()[0] >= '0' && text.ToCharArray()[0] <= '9' ) )
			{
				return "_" + text;
			}

			return text;
		}

		private void classdump()
		{
			using( StreamWriter streamWriter = new StreamWriter( ( new FileInfo( Application.ExecutablePath ) ).Directory.FullName + "\\temp.cs", false, Encoding.ASCII ) )
			{
				streamWriter.WriteLine( "using System;" );
				streamWriter.WriteLine( "using mwperf;" );
				streamWriter.WriteLine( "namespace mwperf.VLTTables {" );

				foreach( VLTClass dq in this.av )
				{
					string text = this.bThree( HashTracker.getValueForHash( dq.hash ) );
					if( this.c( text ) )
					{
						streamWriter.WriteLine( "\tnamespace " + text + " {" );
						streamWriter.WriteLine( "\t\tpublic abstract class " + this.bThree( text + "_base" ) + " {" );

						foreach( VLTClass.aclz1 a in dq )
						{
							string text2 = this.bThree( HashTracker.getValueForHash( a.hash ) );
							if( this.c( text2 ) )
							{
								streamWriter.WriteLine( string.Concat( new string[]
								{
										"\t\t\tpublic static VLTOffsetData",
										a.isArray() ? "[]" : "",
										" ",
										text2,
										";"
								} ) );
							}
						}

						streamWriter.WriteLine( "\t\t}" );

						foreach( UnknownDR dr in dq.dqb1 )
						{
							int num = 0;
							string text3 = this.bThree( HashTracker.getValueForHash( dr.c1.hash ) );
							if( this.c( text3 ) )
							{
								streamWriter.WriteLine( string.Concat( new string[]
								{
										"\t\tpublic class ",
										text3,
										" : ",
										text,
										"_base {"
								} ) );
								streamWriter.WriteLine( "\t\t\tstatic " + text3 + "() {" );

								foreach( VLTClass.aclz1 a2 in dq )
								{
									EABaseType bb = dr.bba1[num++];
									if( !a2.c() || dr.booa1[num - 1] )
									{
										string text4 = this.bThree( HashTracker.getValueForHash( a2.hash ) );
										if( this.c( text4 ) )
										{
											streamWriter.Write( "\t\t\t\t" + text4 + " = " );
											if( a2.isArray() )
											{
												EAArray m = bb as EAArray;
												streamWriter.WriteLine( "new VLTOffsetData[] {" );
												for( int i = 0; i < m.getMaxEntryCount(); ++i )
												{
													bb = m.genlist[i];
													streamWriter.WriteLine( string.Concat( new string[]
													{
																"\t\t\t\t\tnew VLTOffsetData(VLTOffsetType.",
																bb.boo1 ? "Vlt" : "Bin",
																", ",
																string.Format("0x{0:x}", bb.ui1),
																")",
																(i != (int)(m.getMaxEntryCount() - 1)) ? "," : ""
													} ) );
												}
												streamWriter.WriteLine( "\t\t\t\t};" );
											}
											else
											{
												streamWriter.WriteLine( string.Concat( new string[]
												{
															"new VLTOffsetData(VLTOffsetType.",
															bb.boo1 ? "Vlt" : "Bin",
															", ",
															string.Format("0x{0:x}", bb.ui1),
															");"
												} ) );
											}
										}
									}
								}

								streamWriter.WriteLine( "\t\t\t}" );
								streamWriter.WriteLine( "\t\t}" );
							}
						}

						streamWriter.WriteLine( "\t}" );
					}
				}

				streamWriter.WriteLine( "}" );
			}
		}

		private void consoleCommandHandler( string A_0 )
		{
			string[] array = A_0.Split( new char[] { ' ' }, 2 );
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
							this.showOpenFileDialog();
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
						this.unloadFile( text2 );
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
					case "bf":
					case "bf32":
					case "bruteforce":
					case "bruteforce32":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						if( bruteforceThreads.ContainsKey( text2 ) )
						{
							this.writeToConsole( "Thread already running." );
							break;
						}
						Thread t = new Thread( HashUtil.bruteforce32 );
						t.Priority = ThreadPriority.Highest;
						t.IsBackground = true;
						t.Start( text2 );
						bruteforceThreads.Add( text2, t );
						break;
					case "bfstop":
					case "bfstop32":
					case "stopbf":
					case "stopbf32":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						if( !bruteforceThreads.ContainsKey( text2 ) )
						{
							this.writeToConsole( "No such thread." );
							break;
						}
						bruteforceThreads[text2].Abort();
						bruteforceThreads.Remove( text2 );
						this.writeToConsole( "Killed thread." );
						break;
					case "hash":
					case "hash32":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeToConsole( string.Format( "hash({0})=0x{1:x}", text2, HashUtil.getHash32( text2 ) ) );
						break;
					case "hash64":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeToConsole( string.Format( "hash64({0})=0x{1:x}", text2, HashUtil.getHash64( text2 ) ) );
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
					case "pwd":
						if( !noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.writeCurrentDirectory();
						break;
					case "cd":
						if( noArgs )
						{
							this.writeCurrentDirectory();
							break;
						}
						DirectoryInfo directoryInfo4 = new DirectoryInfo( text2 );
						if( directoryInfo4.Exists )
						{
							Directory.SetCurrentDirectory( directoryInfo4.FullName );
							this.writeToConsole( "Current directory: " + directoryInfo4.FullName );
							break;
						}
						this.writeToConsole( "Directory does not exist." );
						break;
					case "ls":
					case "dir":
						this.writeCurrentDirectory();
						DirectoryInfo directoryInfo = new DirectoryInfo( Directory.GetCurrentDirectory() );
						DirectoryInfo[] directories;
						FileInfo[] files;
						if( noArgs )
						{
							directories = directoryInfo.GetDirectories();
							foreach( DirectoryInfo directoryInfo2 in directories )
							{
								this.writeToConsole( "[d] " + directoryInfo2.Name );
							}
							files = directoryInfo.GetFiles();
							foreach( FileInfo fileInfo3 in files )
							{
								this.writeToConsole( "    " + fileInfo3.Name );
							}
							break;
						}
						directories = directoryInfo.GetDirectories( text2 );
						foreach( DirectoryInfo directoryInfo3 in directories )
						{
							this.writeToConsole( "[d] " + directoryInfo3.Name );
						}
						files = directoryInfo.GetFiles( text2 );
						foreach( FileInfo fileInfo4 in files )
						{
							this.writeToConsole( "    " + fileInfo4.Name );
						}
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
					case "loadedfiles":
						this.listLoadedFiles();
						break;
					case "listitems":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						this.f( text2 );
						break;
					case "dump":
						if( noArgs )
						{
							this.writeToConsole( "Error in command." );
							break;
						}
						TreeNode selectedNode = this.tv.SelectedNode;
						if( selectedNode.Tag is UnknownDE )
						{
							this.writeToConsole( "You may only use this command on classes or data rows." );
							break;
						}
						try
						{
							if( selectedNode.Tag is VLTClass )
							{
								VLTClass a_ = selectedNode.Tag as VLTClass;
								this.a( a_, text2 );
							}
							else
							{
								UnknownDR a_2 = selectedNode.Tag as UnknownDR;
								this.a( a_2, text2 );
							}
						}
						catch( Exception ex )
						{
							this.writeToConsole( ex.Message );
						}
						break;
					case "classdump":
						this.classdump();
						break;
					case "help": // MW: Add help command
						this.writeToConsole( "Common commands:" );
						this.writeToConsole( "\thash, hash64 <string>: returns the hash(64) of the given string." );
						this.writeToConsole( "\ths, hsearch <string/0xHASH>: searches for VLT entries of the given string or hash." );
						this.writeToConsole( "\thex <int>: returns the hexadecimal representation of the given decimal." );
						this.writeToConsole( "\tcls, clear: clear the console." );
						break;
					case "debug":
						frmDesigner destest = new frmDesigner();
						destest.Show();
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
				this.writeToConsole( "Error while executing: " + A_0 );
			}
		}

		private void j( object A_0, EventArgs A_1 )
		{
			if( this.tv.SelectedNode != null )
			{
				TreeNode selectedNode = this.tv.SelectedNode;
				if( selectedNode.Tag is UnknownDE )
				{
					Clipboard.SetDataObject( "Database" );
				}
				else if( selectedNode.Tag is VLTClass )
				{
					VLTClass dq = selectedNode.Tag as VLTClass;
					Clipboard.SetDataObject( HashTracker.getValueForHash( dq.hash ) );
				}
				else
				{
					UnknownDR dr = selectedNode.Tag as UnknownDR;
					Clipboard.SetDataObject( HashTracker.getValueForHash( dr.c1.hash ) );
				}
			}
		}

		private void i( object A_0, EventArgs A_1 )
		{
			if( this.tv.SelectedNode != null )
			{
				TreeNode selectedNode = this.tv.SelectedNode;
				if( selectedNode.Tag is UnknownDE )
				{
					// Right-clicked "Database", selected "Copy Node Path"
					Clipboard.SetDataObject( "" );
				}
				else if( selectedNode.Tag is VLTClass )
				{
					VLTClass dq = selectedNode.Tag as VLTClass;
					Clipboard.SetDataObject( HashTracker.getValueForHash( dq.hash ) );
				}
				else
				{
					UnknownDR dr = selectedNode.Tag as UnknownDR;
					VLTClass dq2 = dr.dq1;
					Clipboard.SetDataObject( HashTracker.getValueForHash( dq2.hash ) + "/" + this.a( dr ) );
				}
			}
		}

		private void h( object A_0, EventArgs A_1 )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode selectedNode = this.tvFields.SelectedNode;
				EABaseType bb = selectedNode.Tag as EABaseType;
				if( selectedNode.Parent != null )
				{
					EABaseType bb2 = selectedNode.Parent.Tag as EAArray;
					Clipboard.SetDataObject( string.Concat( new object[]
					{
						HashTracker.getValueForHash(bb2.ui3),
						"[",
						bb.i1,
						"]"
					} ) );
					return;
				}
				Clipboard.SetDataObject( HashTracker.getValueForHash( bb.ui3 ) );
			}
		}

		private void g( object A_0, EventArgs A_1 )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode selectedNode = this.tvFields.SelectedNode;
				if( selectedNode.Tag is EABaseType && !( selectedNode.Tag is EAArray ) )
				{
					EABaseType bb = selectedNode.Tag as EABaseType;
					Clipboard.SetDataObject( string.Format( "0x{0}", bb.ui1 ) );
				}
			}
		}

		private void f( object A_0, EventArgs A_1 )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode selectedNode = this.tvFields.SelectedNode;
				if( selectedNode.Tag is EABaseType && !( selectedNode.Tag is EAArray ) )
				{
					EABaseType bb = selectedNode.Tag as EABaseType;
					Clipboard.SetDataObject( string.Format( "{0}:0x{1}", bb.boo1 ? "vlt" : "bin", bb.ui1 ) );
				}
			}
		}

		private void e( object A_0, EventArgs A_1 )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode treeNode = this.tvFields.SelectedNode;
				if( treeNode.Parent != null )
				{
					treeNode = treeNode.Parent;
				}
				EABaseType bb = treeNode.Tag as EABaseType;
				this.a( bb.dr1, bb.ui3 );
			}
		}

		private void menuExitClicked( object A_0, EventArgs A_1 )
		{
			this.exit();
		}

		private void menuOpenClicked( object A_0, EventArgs A_1 )
		{
			this.showOpenFileDialog();
		}

		// TODO: opt
		private void bFour( object A_0, EventArgs A_1 )
		{
			MenuItem menuItem = A_0 as MenuItem;
			this.unloadFile( menuItem.Text );
		}

		// TODO: opt
		private void tv_AfterSelect( object A_0, TreeViewEventArgs A_1 )
		{
			object tag = A_1.Node.Tag;
			if( tag is VLTClass )
			{
				this.classGrid.Visible = true;
				this.pnlData.Visible = false;
				VLTClass dq = tag as VLTClass;
				DataSet dataSet = new DataSet( "VLT" );
				DataTable dataTable = dataSet.Tables.Add( "Fields" );
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

				this.classGrid.DataSource = dataSet;
				this.classGrid.DataMember = "Fields";

				// This gets rid of the extra row in the table that appears when viewing a root node (e.x. junkman, pvehicle)
				CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[dataSet, "Fields"];
				( (DataView)currencyManager.List ).AllowNew = false;
				( (DataView)currencyManager.List ).AllowEdit = false;
				( (DataView)currencyManager.List ).AllowDelete = false;

				UnknownA8 a2 = dq.b01.a( VLTOtherValue.TABLE_END ) as UnknownA8;
				this.classGrid.Update();
			}
			else if( tag is UnknownDR )
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
				UnknownA8 a4 = dr.b01.a( VLTOtherValue.TABLE_END ) as UnknownA8;
			}
			else
			{
				this.classGrid.Visible = false;
				this.pnlData.Visible = false;
			}
		}

		private void tvFields_AfterSelect( object A_0, TreeViewEventArgs A_1 )
		{
			object tag = A_1.Node.Tag;
			if( tag is EABaseType && !( tag is EAArray ) )
			{
				EABaseType bb = tag as EABaseType;
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

				// This gets rid of the extra row in the table that appears when viewing a sub-node (e.x. junkman --> default)
				CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[dataSet, "Values"];
				( (DataView)currencyManager.List ).AllowNew = false;
				( (DataView)currencyManager.List ).AllowEdit = false;
				( (DataView)currencyManager.List ).AllowDelete = false;

				this.lblFieldType.Text = "Type: " + HashTracker.getValueForHash( bb.ui2 );
				if( BuildConfig.DEBUG )
				{
					this.writeToConsole( "bb.GetType(): " + type.ToString() ); // Here, we're getting the proper type! Great!
					this.writeToConsole( "bb.j(): " + string.Format( "0x{0:x}", bb.ui2 ) ); // Here, we're derping! OMG!
				}
				UnknownBA ba = bb.dr1.b01.a( VLTOtherValue.VLTMAGIC ) as UnknownBA;
				string text = ba.sa1[0];
				this.lblFieldOffset.Text = string.Format( "Offset: {0}:0x{1:x}  ({2})", bb.boo1 ? "vlt" : "bin", bb.ui1, text );
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

		private void onLoad( object A_0, EventArgs A_1 )
		{
			this.writeToConsole( "VLTEdit " + Application.ProductVersion );
			this.writeToConsole( "Copyright (C) 2005-2006, Arushan" );
			this.writeToConsole( "Copyright (C) 2015, Kyle \"MWisBest\" Repinski" );
			if( BuildConfig.DEBUG )
			{
				this.writeTestInfo();
			}
			this.txtConsoleInput.Focus();
		}

		private void writeTestInfo()
		{
			this.writeToConsole( "typeof( EAInt32 ) == " + typeof( EAInt32 ).ToString() );
		}

		private void writeCurrentDirectory()
		{
			this.writeToConsole( "Current directory: " + (new DirectoryInfo( Directory.GetCurrentDirectory() )).FullName );
		}

		private void onTextEntry( object A_0, KeyPressEventArgs A_1 )
		{
			if( A_1.KeyChar == '\r' )
			{
				string text = this.txtConsoleInput.Text;
				if( text != "" )
				{
					this.txtConsoleInput.Text = "";
					this.txtConsoleInput.Refresh();
					this.writeToConsole( "> " + text );
					this.consoleCommandHandler( text );
					this.txtConsoleInput.Focus();
					A_1.Handled = true;
				}
			}
		}
	}
}
