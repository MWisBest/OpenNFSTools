using NFSTools.LibNFS.Crypto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using NFSTools.VLTEdit.VLTTypes;

namespace NFSTools.VLTEdit
{
	public class MainForm : System.Windows.Forms.Form
	{
		#region "Form Objects"

		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.DataGrid classGrid;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxType;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxName;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxLength;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxOffset;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxFlags;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxCount;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyleClass;
		private System.Windows.Forms.Panel pnlData;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyleData;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxDataName;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxDataValue;
		private System.Windows.Forms.TreeView tvFields;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.MenuItem mnuFileUnload;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.Panel pnlFieldInfo;
		private System.Windows.Forms.Label lblFieldType;
		private System.Windows.Forms.Label lblFieldOffset;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxAlignment;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.TextBox txtConsole;
		private System.Windows.Forms.TextBox txtConsoleInput;
		private System.Windows.Forms.Panel pnlBottom;

		#endregion
		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.ContextMenu cxtTV;
		private System.Windows.Forms.MenuItem mnuTVCopyNodeName;
		private System.Windows.Forms.MenuItem mnuTVCopyNodePath;
		private System.Windows.Forms.ContextMenu cxtFields;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuHelpNoHelp;
		private System.Windows.Forms.MenuItem mnuHelpSep1;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.MenuItem mnuFieldCopyName;
		private System.Windows.Forms.MenuItem mnuFieldDump;
		private System.Windows.Forms.MenuItem mnuFieldCopyOffset;
		private System.Windows.Forms.MenuItem mnuFieldCopyOffsetExt;
		private System.Windows.Forms.MenuItem mnuFieldSep1;
		private System.Windows.Forms.MenuItem menuItem1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			this.InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( this.components != null )
				{
					this.components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mnuMain = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFileOpen = new System.Windows.Forms.MenuItem();
			this.mnuFileUnload = new System.Windows.Forms.MenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelpNoHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelpSep1 = new System.Windows.Forms.MenuItem();
			this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
			this.tv = new System.Windows.Forms.TreeView();
			this.cxtTV = new System.Windows.Forms.ContextMenu();
			this.mnuTVCopyNodeName = new System.Windows.Forms.MenuItem();
			this.mnuTVCopyNodePath = new System.Windows.Forms.MenuItem();
			this.classGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyleClass = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxType = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxLength = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxOffset = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxCount = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxAlignment = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxFlags = new System.Windows.Forms.DataGridTextBoxColumn();
			this.pnlData = new System.Windows.Forms.Panel();
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyleData = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxDataName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxDataValue = new System.Windows.Forms.DataGridTextBoxColumn();
			this.pnlFieldInfo = new System.Windows.Forms.Panel();
			this.lblFieldOffset = new System.Windows.Forms.Label();
			this.lblFieldType = new System.Windows.Forms.Label();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.tvFields = new System.Windows.Forms.TreeView();
			this.cxtFields = new System.Windows.Forms.ContextMenu();
			this.mnuFieldCopyName = new System.Windows.Forms.MenuItem();
			this.mnuFieldCopyOffset = new System.Windows.Forms.MenuItem();
			this.mnuFieldCopyOffsetExt = new System.Windows.Forms.MenuItem();
			this.mnuFieldSep1 = new System.Windows.Forms.MenuItem();
			this.mnuFieldDump = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.txtConsole = new System.Windows.Forms.TextBox();
			this.txtConsoleInput = new System.Windows.Forms.TextBox();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			( (System.ComponentModel.ISupportInitialize)( this.classGrid ) ).BeginInit();
			this.pnlData.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.dataGrid ) ).BeginInit();
			this.pnlFieldInfo.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					this.mnuFile,
																					this.mnuHelp} );
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					this.mnuFileOpen,
																					this.mnuFileUnload,
																					this.mnuFileSep1,
																					this.mnuFileExit} );
			this.mnuFile.Text = "&File";
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Index = 0;
			this.mnuFileOpen.Text = "&Open";
			this.mnuFileOpen.Click += new System.EventHandler( this.mnuFileOpen_Click );
			// 
			// mnuFileUnload
			// 
			this.mnuFileUnload.Index = 1;
			this.mnuFileUnload.Text = "&Unload";
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Index = 2;
			this.mnuFileSep1.Text = "-";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 3;
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler( this.mnuFileExit_Click );
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 1;
			this.mnuHelp.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					this.mnuHelpNoHelp,
																					this.mnuHelpSep1,
																					this.mnuHelpAbout,
																					this.menuItem1} );
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelpNoHelp
			// 
			this.mnuHelpNoHelp.Index = 0;
			this.mnuHelpNoHelp.Text = "Sorry, no help here. :)";
			// 
			// mnuHelpSep1
			// 
			this.mnuHelpSep1.Index = 1;
			this.mnuHelpSep1.Text = "-";
			// 
			// mnuHelpAbout
			// 
			this.mnuHelpAbout.Index = 2;
			this.mnuHelpAbout.Text = "VLTEdit by Arushan";
			// 
			// tv
			// 
			this.tv.BackColor = System.Drawing.SystemColors.Window;
			this.tv.ContextMenu = this.cxtTV;
			this.tv.Dock = System.Windows.Forms.DockStyle.Left;
			this.tv.HideSelection = false;
			this.tv.ImageIndex = -1;
			this.tv.Location = new System.Drawing.Point( 0, 0 );
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = -1;
			this.tv.Size = new System.Drawing.Size( 200, 437 );
			this.tv.TabIndex = 1;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tv_AfterSelect );
			// 
			// cxtTV
			// 
			this.cxtTV.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																				  this.mnuTVCopyNodeName,
																				  this.mnuTVCopyNodePath} );
			// 
			// mnuTVCopyNodeName
			// 
			this.mnuTVCopyNodeName.Index = 0;
			this.mnuTVCopyNodeName.Text = "Copy Node Name";
			this.mnuTVCopyNodeName.Click += new System.EventHandler( this.mnuTVCopyNodeName_Click );
			// 
			// mnuTVCopyNodePath
			// 
			this.mnuTVCopyNodePath.Index = 1;
			this.mnuTVCopyNodePath.Text = "Copy Node Path";
			this.mnuTVCopyNodePath.Click += new System.EventHandler( this.mnuTVCopyNodePath_Click );
			// 
			// classGrid
			// 
			this.classGrid.CaptionBackColor = System.Drawing.SystemColors.Control;
			this.classGrid.CaptionVisible = false;
			this.classGrid.DataMember = "";
			this.classGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.classGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.classGrid.Location = new System.Drawing.Point( 204, 0 );
			this.classGrid.Name = "classGrid";
			this.classGrid.Size = new System.Drawing.Size( 648, 437 );
			this.classGrid.TabIndex = 2;
			this.classGrid.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
																								  this.dataGridTableStyleClass} );
			this.classGrid.Visible = false;
			// 
			// dataGridTableStyleClass
			// 
			this.dataGridTableStyleClass.DataGrid = this.classGrid;
			this.dataGridTableStyleClass.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
																													  this.dataGridTextBoxName,
																													  this.dataGridTextBoxType,
																													  this.dataGridTextBoxLength,
																													  this.dataGridTextBoxOffset,
																													  this.dataGridTextBoxCount,
																													  this.dataGridTextBoxAlignment,
																													  this.dataGridTextBoxFlags} );
			this.dataGridTableStyleClass.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyleClass.MappingName = "Fields";
			this.dataGridTableStyleClass.ReadOnly = true;
			// 
			// dataGridTextBoxName
			// 
			this.dataGridTextBoxName.Format = "";
			this.dataGridTextBoxName.FormatInfo = null;
			this.dataGridTextBoxName.HeaderText = "Name";
			this.dataGridTextBoxName.MappingName = "Name";
			this.dataGridTextBoxName.Width = 80;
			// 
			// dataGridTextBoxType
			// 
			this.dataGridTextBoxType.Format = "";
			this.dataGridTextBoxType.FormatInfo = null;
			this.dataGridTextBoxType.HeaderText = "Type";
			this.dataGridTextBoxType.MappingName = "Type";
			this.dataGridTextBoxType.Width = 150;
			// 
			// dataGridTextBoxLength
			// 
			this.dataGridTextBoxLength.Format = "";
			this.dataGridTextBoxLength.FormatInfo = null;
			this.dataGridTextBoxLength.HeaderText = "Length";
			this.dataGridTextBoxLength.MappingName = "Length";
			this.dataGridTextBoxLength.Width = 60;
			// 
			// dataGridTextBoxOffset
			// 
			this.dataGridTextBoxOffset.Format = "";
			this.dataGridTextBoxOffset.FormatInfo = null;
			this.dataGridTextBoxOffset.HeaderText = "Offset";
			this.dataGridTextBoxOffset.MappingName = "Offset";
			this.dataGridTextBoxOffset.Width = 60;
			// 
			// dataGridTextBoxCount
			// 
			this.dataGridTextBoxCount.Format = "";
			this.dataGridTextBoxCount.FormatInfo = null;
			this.dataGridTextBoxCount.HeaderText = "Count";
			this.dataGridTextBoxCount.MappingName = "Count";
			this.dataGridTextBoxCount.Width = 60;
			// 
			// dataGridTextBoxAlignment
			// 
			this.dataGridTextBoxAlignment.Format = "";
			this.dataGridTextBoxAlignment.FormatInfo = null;
			this.dataGridTextBoxAlignment.HeaderText = "Alignment";
			this.dataGridTextBoxAlignment.MappingName = "Alignment";
			this.dataGridTextBoxAlignment.Width = 60;
			// 
			// dataGridTextBoxFlags
			// 
			this.dataGridTextBoxFlags.Format = "";
			this.dataGridTextBoxFlags.FormatInfo = null;
			this.dataGridTextBoxFlags.HeaderText = "Flags";
			this.dataGridTextBoxFlags.MappingName = "Flags";
			this.dataGridTextBoxFlags.Width = 60;
			// 
			// pnlData
			// 
			this.pnlData.Controls.Add( this.dataGrid );
			this.pnlData.Controls.Add( this.pnlFieldInfo );
			this.pnlData.Controls.Add( this.splitter2 );
			this.pnlData.Controls.Add( this.tvFields );
			this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlData.Location = new System.Drawing.Point( 204, 0 );
			this.pnlData.Name = "pnlData";
			this.pnlData.Size = new System.Drawing.Size( 648, 437 );
			this.pnlData.TabIndex = 3;
			this.pnlData.Visible = false;
			// 
			// dataGrid
			// 
			this.dataGrid.CaptionVisible = false;
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point( 180, 0 );
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size( 468, 389 );
			this.dataGrid.TabIndex = 1;
			this.dataGrid.TableStyles.AddRange( new System.Windows.Forms.DataGridTableStyle[] {
																								 this.dataGridTableStyleData} );
			// 
			// dataGridTableStyleData
			// 
			this.dataGridTableStyleData.DataGrid = this.dataGrid;
			this.dataGridTableStyleData.GridColumnStyles.AddRange( new System.Windows.Forms.DataGridColumnStyle[] {
																													 this.dataGridTextBoxDataName,
																													 this.dataGridTextBoxDataValue} );
			this.dataGridTableStyleData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyleData.MappingName = "Values";
			this.dataGridTableStyleData.ReadOnly = true;
			// 
			// dataGridTextBoxDataName
			// 
			this.dataGridTextBoxDataName.Format = "";
			this.dataGridTextBoxDataName.FormatInfo = null;
			this.dataGridTextBoxDataName.HeaderText = "Name";
			this.dataGridTextBoxDataName.MappingName = "Name";
			this.dataGridTextBoxDataName.Width = 75;
			// 
			// dataGridTextBoxDataValue
			// 
			this.dataGridTextBoxDataValue.Format = "";
			this.dataGridTextBoxDataValue.FormatInfo = null;
			this.dataGridTextBoxDataValue.HeaderText = "Value";
			this.dataGridTextBoxDataValue.MappingName = "Value";
			this.dataGridTextBoxDataValue.Width = 300;
			// 
			// pnlFieldInfo
			// 
			this.pnlFieldInfo.Controls.Add( this.lblFieldOffset );
			this.pnlFieldInfo.Controls.Add( this.lblFieldType );
			this.pnlFieldInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlFieldInfo.Location = new System.Drawing.Point( 180, 389 );
			this.pnlFieldInfo.Name = "pnlFieldInfo";
			this.pnlFieldInfo.Size = new System.Drawing.Size( 468, 48 );
			this.pnlFieldInfo.TabIndex = 5;
			// 
			// lblFieldOffset
			// 
			this.lblFieldOffset.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
				| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.lblFieldOffset.Location = new System.Drawing.Point( 8, 28 );
			this.lblFieldOffset.Name = "lblFieldOffset";
			this.lblFieldOffset.Size = new System.Drawing.Size( 456, 16 );
			this.lblFieldOffset.TabIndex = 6;
			this.lblFieldOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFieldType
			// 
			this.lblFieldType.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
				| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.lblFieldType.Location = new System.Drawing.Point( 8, 8 );
			this.lblFieldType.Name = "lblFieldType";
			this.lblFieldType.Size = new System.Drawing.Size( 456, 16 );
			this.lblFieldType.TabIndex = 5;
			this.lblFieldType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point( 176, 0 );
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size( 4, 437 );
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			// 
			// tvFields
			// 
			this.tvFields.BackColor = System.Drawing.SystemColors.Window;
			this.tvFields.ContextMenu = this.cxtFields;
			this.tvFields.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvFields.FullRowSelect = true;
			this.tvFields.HideSelection = false;
			this.tvFields.ImageIndex = -1;
			this.tvFields.Location = new System.Drawing.Point( 0, 0 );
			this.tvFields.Name = "tvFields";
			this.tvFields.SelectedImageIndex = -1;
			this.tvFields.Size = new System.Drawing.Size( 176, 437 );
			this.tvFields.TabIndex = 0;
			this.tvFields.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvFields_AfterSelect );
			// 
			// cxtFields
			// 
			this.cxtFields.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
																					  this.mnuFieldCopyName,
																					  this.mnuFieldCopyOffset,
																					  this.mnuFieldCopyOffsetExt,
																					  this.mnuFieldSep1,
																					  this.mnuFieldDump} );
			// 
			// mnuFieldCopyName
			// 
			this.mnuFieldCopyName.Index = 0;
			this.mnuFieldCopyName.Text = "Copy Field Name";
			this.mnuFieldCopyName.Click += new System.EventHandler( this.mnuFieldCopyName_Click );
			// 
			// mnuFieldCopyOffset
			// 
			this.mnuFieldCopyOffset.Index = 1;
			this.mnuFieldCopyOffset.Text = "Copy Offset";
			this.mnuFieldCopyOffset.Click += new System.EventHandler( this.mnuFieldCopyOffset_Click );
			// 
			// mnuFieldCopyOffsetExt
			// 
			this.mnuFieldCopyOffsetExt.Index = 2;
			this.mnuFieldCopyOffsetExt.Text = "Copy Type:Offset";
			this.mnuFieldCopyOffsetExt.Click += new System.EventHandler( this.mnuFieldCopyOffsetExt_Click );
			// 
			// mnuFieldSep1
			// 
			this.mnuFieldSep1.Index = 3;
			this.mnuFieldSep1.Text = "-";
			// 
			// mnuFieldDump
			// 
			this.mnuFieldDump.Index = 4;
			this.mnuFieldDump.Text = "Dump Field";
			this.mnuFieldDump.Click += new System.EventHandler( this.mnuFieldDump_Click );
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point( 200, 0 );
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size( 4, 437 );
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add( this.txtConsole );
			this.pnlBottom.Controls.Add( this.txtConsoleInput );
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point( 0, 441 );
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size( 852, 152 );
			this.pnlBottom.TabIndex = 0;
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtConsole.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (System.Byte)( 0 ) ) );
			this.txtConsole.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtConsole.Location = new System.Drawing.Point( 0, 0 );
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size( 852, 132 );
			this.txtConsole.TabIndex = 1;
			this.txtConsole.Text = "";
			// 
			// txtConsoleInput
			// 
			this.txtConsoleInput.AcceptsReturn = true;
			this.txtConsoleInput.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtConsoleInput.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (System.Byte)( 0 ) ) );
			this.txtConsoleInput.Location = new System.Drawing.Point( 0, 132 );
			this.txtConsoleInput.Name = "txtConsoleInput";
			this.txtConsoleInput.Size = new System.Drawing.Size( 852, 20 );
			this.txtConsoleInput.TabIndex = 0;
			this.txtConsoleInput.Text = "";
			this.txtConsoleInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.txtConsoleInput_KeyPress );
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter3.Location = new System.Drawing.Point( 0, 437 );
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size( 852, 4 );
			this.splitter3.TabIndex = 5;
			this.splitter3.TabStop = false;
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "Contact: oneforaru@gmail.com";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 852, 593 );
			this.Controls.Add( this.pnlData );
			this.Controls.Add( this.classGrid );
			this.Controls.Add( this.splitter1 );
			this.Controls.Add( this.tv );
			this.Controls.Add( this.splitter3 );
			this.Controls.Add( this.pnlBottom );
			this.Menu = this.mnuMain;
			this.Name = "MainForm";
			this.Text = "VLTEdit";
			this.Load += new System.EventHandler( this.MainForm_Load );
			( (System.ComponentModel.ISupportInitialize)( this.classGrid ) ).EndInit();
			this.pnlData.ResumeLayout( false );
			( (System.ComponentModel.ISupportInitialize)( this.dataGrid ) ).EndInit();
			this.pnlFieldInfo.ResumeLayout( false );
			this.pnlBottom.ResumeLayout( false );
			this.ResumeLayout( false );

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run( new MainForm() );
		}

		//- Global Variables -------------------------------------//

		ArrayList _vltFiles = new ArrayList();
		VLTDatabase _db = null;

		//- Private Helper Functions -----------------------------//

		private void ExitApplication()
		{
			Application.Exit();
		}

		private bool LoadVLTFile( string filename, bool autoUpdateTV )
		{
			bool success = false;

			if( this._db == null )
			{
				this._db = new VLTDatabase();
			}

			VLTFile myVlt = new VLTFile();
			this.AddToConsole( "Loading: " + filename );
			myVlt.Open( filename );

			if( this.ParseVLT( myVlt ) )
			{
				VLTDependency myDep = myVlt.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string myName = myDep.GetName( VLTDependency.VltFile );

				MenuItem menuItem = new MenuItem( myName, new EventHandler( this.mnuFileUnloadSub_Click ) );
				this.mnuFileUnload.MenuItems.Add( menuItem );

				success = true;
			}

			if( autoUpdateTV )
			{
				this.PopulateTreeView();
			}

			return success;

		}

		private void UnloadVLTFile( string vltname )
		{
			foreach( VLTFile vlt in this._vltFiles )
			{
				VLTDependency dep = vlt.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string name = dep.GetName( VLTDependency.VltFile );
				if( name == vltname )
				{
					this.AddToConsole( "Unloading: " + name );
					foreach( MenuItem item in this.mnuFileUnload.MenuItems )
					{
						if( item.Text == vltname )
						{
							this.mnuFileUnload.MenuItems.Remove( item );
							break;
						}
					}

					this._vltFiles.Remove( vlt );
					this.ReparseAllVLT();
					this.PopulateTreeView();
					break;
				}
			}

		}

		private void ShowOpenDialog()
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
				bool updateTV = false;

				foreach( string filename in ofd.FileNames )
				{
					if( this.LoadVLTFile( filename, false ) )
					{
						updateTV = true;
					}
				}

				if( updateTV )
				{
					this.PopulateTreeView();
				}
			}
		}

		private void ReparseAllVLT()
		{
			this._db = new VLTDatabase();

			foreach( VLTFile vlt in this._vltFiles )
			{
				this.ReparseVLT( vlt );
			}
		}

		private bool ParseVLT( VLTFile vlt )
		{
			VLTDependency dep = vlt.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
			uint hash = dep.GetHash( VLTDependency.VltFile );
			foreach( VLTFile file in this._vltFiles )
			{
				VLTDependency fDep = file.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				if( fDep.GetHash( VLTDependency.VltFile ) == hash )
				{
					MessageBox.Show( "This VLT data file has already been loaded.", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error );
					return false;
				}
			}

			if( this.ReparseVLT( vlt ) )
			{
				this._vltFiles.Add( vlt );
				return true;
			}
			else
			{
				return false;
			}


		}

		private bool ReparseVLT( VLTFile vlt )
		{
			//try
			{
				VLTExpression exp = vlt.GetChunk( VLTChunkId.Expression ) as VLTExpression;
				for( int i = 0; i < exp.Count; i++ )
				{
					VLTExpressionBlock block = exp[i];
					switch( block.ExpressionType )
					{
						case VLTExpressionType.DatabaseLoadData:
							this._db.LoadDatabase( block.Data.AsDatabaseLoad(), vlt );
							break;
						case VLTExpressionType.ClassLoadData:
							this._db.LoadClass( block.Data.AsClassLoad(), vlt );
							break;
						case VLTExpressionType.CollectionLoadData:
							VLTDataCollectionLoad collLoad = block.Data.AsCollectionLoad();
							VLTClass vltClass = this._db[collLoad.ClassNameHash];
							vltClass.Data.Add( collLoad, vlt );
							break;
					}
				}
			}
			if( false )
			//catch
			{
				VLTDependency dep = vlt.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string name = dep.GetName( VLTDependency.VltFile );

				MessageBox.Show( "Could not load file. Did you forget to load a dependency? Unloading file: " + name,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );

				if( this._vltFiles.Contains( vlt ) )
				{
					this._vltFiles.Remove( vlt );
				}

				foreach( MenuItem menuItem in this.mnuFileUnload.MenuItems )
				{
					if( menuItem.Text == name )
					{
						this.mnuFileUnload.MenuItems.Remove( menuItem );
					}
				}

				return false;
			}

			return true;
		}

		private void ListLoadedFiles()
		{
			foreach( VLTFile file in this._vltFiles )
			{
				VLTDependency dep = file.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				this.AddToConsole( dep.GetName( 0 ) );
			}
		}

		private void ListLoadedItems( string vltname )
		{
			foreach( VLTFile vlt in this._vltFiles )
			{
				VLTDependency dep = vlt.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string name = dep.GetName( VLTDependency.VltFile );
				if( name == vltname )
				{
					this.AddToConsole( "Items in " + name );
					VLTExpression exp = vlt.GetChunk( VLTChunkId.Expression ) as VLTExpression;
					foreach( VLTExpressionBlock blk in exp )
					{
						switch( blk.ExpressionType )
						{
							case VLTExpressionType.DatabaseLoadData:
								this.AddToConsole( "- Database" );
								break;
							case VLTExpressionType.ClassLoadData:
								this.AddToConsole( "- Class: " + HashResolver.Resolve( blk.Id ) );
								break;
							case VLTExpressionType.CollectionLoadData:
								VLTDataCollectionLoad colLoad = blk.Data.AsCollectionLoad();
								this.AddToConsole( "- Row: " + HashResolver.Resolve( colLoad.ClassNameHash )
									+ "/" + this.TrackRow( this._db[colLoad.ClassNameHash].Data[colLoad.NameHash] ) );
								break;
						}
					}
					break;
				}
			}

		}

		private void PopulateTreeView()
		{

			bool showErrors = true;

			this.classGrid.Visible = false;
			this.pnlData.Visible = false;
			this.tv.Nodes.Clear();

			if( this._vltFiles.Count == 0 )
			{
				return;
			}

			this.tv.BeginUpdate();

			string nodeName, key;
			TreeNode node, classNode, dbNode;
			Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

			dbNode = this.tv.Nodes.Add( "Database" );
			dbNode.Tag = this._db;

			foreach( VLTClass vltClass in this._db )
			{
				//nodeName = "(" + string.Format("{0:x}", vltClass.ClassLoad.Expression.Id) + ") ";
				nodeName = HashResolver.Resolve( vltClass.ClassHash );
				dbNode.TreeView.Sorted = true;
				classNode = dbNode.Nodes.Add( nodeName );
				dbNode.TreeView.Sorted = false;
				classNode.Tag = vltClass;

				foreach( VLTDataRow row in vltClass.Data )
				{
					if( row.CollectionLoad.ParentHash == 0 )
					{
						node = classNode;
					}
					else
					{
						key = string.Format( "{0:x},{1:x}", vltClass.ClassHash, row.CollectionLoad.ParentHash );
						node = nodes[key] as TreeNode;
					}

					if( node == null )
					{
						if( showErrors )
						{
							DialogResult result = MessageBox.Show( "Could not find parent data row. Did you forget to load a dependency?\nThe hierarchy will be flattened.",
							"Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning );
							if( result == DialogResult.Cancel )
							{
								showErrors = false;
							}
						}

						node = classNode;
					}

					//nodeName = "(" + _hashResolver.Resolve(row.CollectionLoad.Expression.Id) + ") ";
					nodeName = HashResolver.Resolve( row.CollectionLoad.NameHash ) +
						" [" + vltClass.ClassLoad.RequiredFieldsCount + "+" +  // req'd
						row.CollectionLoad.CountOptional + "]";   // opt
					node = node.Nodes.Add( nodeName );
					node.Tag = row;

					key = string.Format( "{0:x},{1:x}", vltClass.ClassHash, row.CollectionLoad.NameHash );
					nodes.Add( key, node );
				}
			}

			this.tv.EndUpdate();
		}

		private void AddToConsole( string text )
		{
			if( this.txtConsole.Text != "" )
			{
				this.txtConsole.AppendText( "\r\n" );
			}

			this.txtConsole.AppendText( text );
			this.txtConsole.SelectionStart = this.txtConsole.Text.Length;
			this.txtConsole.Refresh();
		}

		private string TrackRow( VLTDataRow row )
		{
			VLTClass cls = row.VLTClass;
			string text = HashResolver.Resolve( row.CollectionLoad.NameHash );
			if( row.CollectionLoad.ParentHash == 0 )
			{
				return text;
			}
			else
			{
				return this.TrackRow( cls.Data[row.CollectionLoad.ParentHash] ) + "/" + text;
			}
		}

		private void HashSearch( string text, ref ArrayList found )
		{
			uint hash;
			if( text.StartsWith( "0x" ) )
			{
				hash = uint.Parse( text.Substring( 2 ), NumberStyles.HexNumber );
			}
			else
			{
				hash = JenkinsHash.getHash32( text );
			}

			string matchText;
			foreach( VLTClass vltClass in this._db )
			{
				if( vltClass.ClassHash == hash )
				{
					matchText = text + ": Found match for class: " + HashResolver.Resolve( vltClass.ClassHash );
					if( !found.Contains( matchText ) )
					{
						found.Add( matchText );
					}
				}
				foreach( VLTClass.ClassField field in vltClass )
				{
					if( field.NameHash == hash )
					{
						matchText = text + ": Found match for field: " + HashResolver.Resolve( vltClass.ClassHash ) + "/" + HashResolver.Resolve( field.NameHash );
						if( !found.Contains( matchText ) )
						{
							found.Add( matchText );
						}
					}
				}
				foreach( VLTDataRow row in vltClass.Data )
				{
					if( row.CollectionLoad.NameHash == hash )
					{
						matchText = text + ": Found match for row: " + HashResolver.Resolve( vltClass.ClassHash ) + "/" + this.TrackRow( row );
						if( !found.Contains( matchText ) )
						{
							found.Add( matchText );
						}
					}
				}
			}
		}

		private void HashSearchAll( string text )
		{
			ArrayList found = new ArrayList();

			// normal search
			string searchText = text;
			this.HashSearch( searchText, ref found );             // OMG_search_this
			this.HashSearch( searchText.ToLower(), ref found );   // omg_search_this
			this.HashSearch( searchText.ToUpper(), ref found ); // OMG_SEARCH_THIS

			// case based search
			bool lastUS = true;
			searchText = "";
			foreach( char c in text )
			{
				if( c == '_' )
				{
					lastUS = true;
				}
				else if( lastUS )
				{
					searchText += new string( c, 1 ).ToUpper();
					lastUS = false;
				}
				else
				{
					searchText += c;
				}
			}

			this.HashSearch( searchText, ref found );             // OmgSearchThis
			this.HashSearch( searchText.ToLower(), ref found );   // omgsearchthis
			this.HashSearch( searchText.ToUpper(), ref found ); // OMGSEARCHTHIS

			if( found.Count > 0 )
			{
				foreach( string str in found )
				{
					this.AddToConsole( str );
				}
			}
			else
			{
				this.AddToConsole( "No matches found." );
			}
		}

		private void DumpTree( VLTClass cls, string item )
		{
			this.AddToConsole( string.Format( "Dumping contents of class: {0}, field: {1}",
							HashResolver.Resolve( cls.ClassHash ), item ) );
			foreach( VLTDataRow row in cls.Data )
			{
				if( row.CollectionLoad.ParentHash == 0 )
				{
					this.DumpTree( row, item );
				}
			}
		}

		private void DumpTree( VLTDataRow row, string item )
		{
			uint hash;
			if( item.StartsWith( "0x" ) )
			{
				hash = uint.Parse( item.Substring( 2 ), NumberStyles.HexNumber );
			}
			else
			{
				hash = JenkinsHash.getHash32( item );
			}

			this.DumpTree( row, hash );
		}

		private void DumpTree( VLTDataRow row, uint hash )
		{
			VLTClass cls = row.VLTClass;

			int fieldId = cls.GetFieldIndex( hash );
			if( fieldId == -1 )
			{
				throw new Exception( "Not a valid field value." );
			}

			string name = this.TrackRow( row );
			object obj = row[fieldId];
			if( obj is VLTDataItemArray )
			{
				VLTDataItemArray array = obj as VLTDataItemArray;
				for( int i = 0; i < array.ValidCount; i++ )
				{
					obj = array[i];
					string value;
					if( obj == null )
					{
						value = "(non-existant)";
					}
					else
					{
						value = obj.ToString();
					}

					this.AddToConsole( string.Format( "{0}[{1}]: {2}", name, i, value ) );
				}
			}
			else
			{
				string value;
				if( obj == null )
				{
					value = "(non-existant)";
				}
				else
				{
					value = obj.ToString();
				}

				this.AddToConsole( string.Format( "{0}: {1}", name, value ) );
			}

			foreach( VLTDataRow clsrow in cls.Data )
			{
				if( clsrow.CollectionLoad.ParentHash == row.CollectionLoad.NameHash )
				{
					this.DumpTree( clsrow, hash );
				}
			}
		}

		private bool ClassDumpDoExport( string str )
		{
			if( str.StartsWith( "unk_" ) )
			{
				return false;
			}

			return true;
		}

		private string ClassDumpSafeName( string strVal )
		{
			string str = strVal;
			if( str.StartsWith( "0x" ) )
			{
				str = "unk_" + str;
			}

			if( str == "default" )
			{
				str = "_default";
			}

			if( str == "null" )
			{
				str = "_null";
			}

			if( str.EndsWith( "*" ) )
			{
				str = str.Substring( 0, str.Length - 1 );
			}

			if( str.ToCharArray()[0] >= '0' && str.ToCharArray()[0] <= '9' )
			{
				str = "_" + str;
			}

			return str;
		}

		private void ClassDumpAll()
		{
			StreamWriter sw = new StreamWriter( @"C:\temp.cs", false, Encoding.ASCII );
			sw.WriteLine( "using System;" );
			sw.WriteLine( "using mwperf;" );
			sw.WriteLine( "namespace mwperf.VLTTables {" );
			foreach( VLTClass cls in this._db )
			{
				string className = this.ClassDumpSafeName( HashResolver.Resolve( cls.ClassHash ) );
				if( !this.ClassDumpDoExport( className ) )
				{
					continue;
				}

				sw.WriteLine( "\tnamespace " + className + " {" );

				sw.WriteLine( "\t\tpublic abstract class " + this.ClassDumpSafeName( className + "_base" ) + " {" );
				foreach( VLTClass.ClassField field in cls )
				{
					string fieldName = this.ClassDumpSafeName( HashResolver.Resolve( field.NameHash ) );
					if( !this.ClassDumpDoExport( fieldName ) )
					{
						continue;
					}

					sw.WriteLine( "\t\t\tpublic static VLTOffsetData" + ( field.IsArray ? "[]" : "" ) + " " + fieldName + ";" );
				}
				sw.WriteLine( "\t\t}" );

				foreach( VLTDataRow row in cls.Data )
				{
					int index = 0;
					string rowName = this.ClassDumpSafeName( HashResolver.Resolve( row.CollectionLoad.NameHash ) );
					if( !this.ClassDumpDoExport( rowName ) )
					{
						continue;
					}

					sw.WriteLine( "\t\tpublic class " + rowName + " : " + className + "_base {" );
					sw.WriteLine( "\t\t\tstatic " + rowName + "() {" );
					foreach( VLTClass.ClassField field in cls )
					{
						VLTDataItem di = row[index++];
						if( field.IsOptional )
						{
							if( !row.IsDataLoaded( index - 1 ) )
							{
								continue;
							}
						}
						string fieldName = this.ClassDumpSafeName( HashResolver.Resolve( field.NameHash ) );
						if( !this.ClassDumpDoExport( fieldName ) )
						{
							continue;
						}

						sw.Write( "\t\t\t\t" + fieldName + " = " );
						if( field.IsArray )
						{
							VLTDataItemArray array = di as VLTDataItemArray;
							sw.WriteLine( "new VLTOffsetData[] {" );
							for( int i = 0; i < array.ValidCount; i++ )
							{
								di = array[i];
								sw.WriteLine( "\t\t\t\t\tnew VLTOffsetData(VLTOffsetType." + ( di.InlineData ? "Vlt" : "Bin" ) +
									", " + string.Format( "0x{0:x}", di.Offset ) + ")" + ( ( i != array.ValidCount - 1 ) ? "," : "" ) );
							}
							sw.WriteLine( "\t\t\t\t};" );
						}
						else
						{
							sw.WriteLine( "new VLTOffsetData(VLTOffsetType." + ( di.InlineData ? "Vlt" : "Bin" ) +
									", " + string.Format( "0x{0:x}", di.Offset ) + ");" );
						}
					}
					sw.WriteLine( "\t\t\t}" );
					sw.WriteLine( "\t\t}" );
				}
				sw.WriteLine( "\t}" );
			}
			sw.WriteLine( "}" );
			sw.Close();
		}

		private void ParseConsoleInput( string text )
		{
			string[] split = text.Split( new char[] { ' ' }, 2 );
			string command = split[0];
			bool noArgs = true;
			string args = null;
			string[] splitArgs = null;

			if( split.Length > 1 )
			{
				noArgs = false;
				args = split[1];
				splitArgs = args.Split( ' ' );
			}

			try
			{

				switch( command )
				{
					case "quit":
						goto case "exit";
					case "exit":
						this.ExitApplication();
						break;
					case "open":
						goto case "load";
					case "load":
						if( noArgs )
						{
							this.ShowOpenDialog();
						}
						else
						{
							FileInfo fi = new FileInfo( args );
							if( fi.Exists )
							{
								if( !this.LoadVLTFile( fi.FullName, true ) )
								{
									this.AddToConsole( "Failed to load file: " + fi.FullName );
								}
							}
							else
							{
								this.AddToConsole( "Non existant file: " + fi.FullName );
							}
						}
						break;
					case "unload":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.UnloadVLTFile( args );
						}

						break;
					case "reparse":
						this.AddToConsole( "Reparsing all VLTs..." );
						this.ReparseAllVLT();
						this.PopulateTreeView();
						break;
					case "cls":
						goto case "clear";
					case "clear":
						this.txtConsole.Text = "";
						break;
					case "hex":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.AddToConsole( string.Format( "hex({0})=0x{1:x}", ulong.Parse( args ), ulong.Parse( args ) ) );
						}

						break;
					case "hash":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.AddToConsole( string.Format( "hash({0})=0x{1:x}", args, JenkinsHash.getHash32( args ) ) );
						}

						break;
					case "hash64":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.AddToConsole( string.Format( "hash64({0})=0x{1:x}", args, JenkinsHash.getHash64( args ) ) );
						}

						break;
					case "hs":
						goto case "hsearch";
					case "hsearch":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.HashSearchAll( args );
						}

						if( command == "hs" )
						{
							this.txtConsoleInput.Text = "hs ";
							this.txtConsoleInput.SelectionStart = this.txtConsoleInput.Text.Length;
						}
						break;
					case "savehash":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							FileInfo fi = new FileInfo( args );
							HashResolver.SaveUsed( fi.FullName );
							this.AddToConsole( "Saved used hashes list to: " + fi.FullName );
						}
						break;
					case "pwd":
						if( noArgs )
						{
							goto case "cd";
						}
						else
						{
							this.AddToConsole( "Error in command." );
						}

						break;
					case "cd":
						if( noArgs )
						{
							this.AddToConsole( "Current directory: " + Directory.GetCurrentDirectory() );
						}
						else
						{
							DirectoryInfo di = new DirectoryInfo( args );
							if( di.Exists )
							{
								Directory.SetCurrentDirectory( di.FullName );
								this.AddToConsole( "Current directory: " + di.FullName );
							}
							else
							{
								this.AddToConsole( "Directory does not exist." );
							}
						}
						break;
					case "ls":
						goto case "dir";
					case "dir":
					{
						DirectoryInfo di = new DirectoryInfo( Directory.GetCurrentDirectory() );
						this.AddToConsole( "Current directory: " + di.FullName );
						if( noArgs )
						{
							foreach( DirectoryInfo dinf in di.GetDirectories() )
							{
								this.AddToConsole( "[d] " + dinf.Name );
							}

							foreach( FileInfo fi in di.GetFiles() )
							{
								this.AddToConsole( "    " + fi.Name );
							}
						}
						else
						{
							foreach( DirectoryInfo dinf in di.GetDirectories( args ) )
							{
								this.AddToConsole( "[d] " + dinf.Name );
							}

							foreach( FileInfo fi in di.GetFiles( args ) )
							{
								this.AddToConsole( "    " + fi.Name );
							}
						}
					}
					break;
					case "loadhash":
						if( File.Exists( args ) )
						{
							HashResolver.Open( args );
						}
						else
						{
							this.AddToConsole( "File does not exist." );
						}

						break;
					case "reloadhashes":
						HashResolver.Initialize();
						this.AddToConsole( "Hashes reloaded." );
						break;
					case "loadedfiles":
						this.ListLoadedFiles();
						break;
					case "listitems":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							this.ListLoadedItems( args );
						}

						break;
					case "dump":
						if( noArgs )
						{
							this.AddToConsole( "Error in command." );
						}
						else
						{
							TreeNode node = this.tv.SelectedNode;
							if( node.Tag is VLTDatabase )
							{
								this.AddToConsole( "You may only use this command on classes or data rows." );
							}
							else
							{
								try
								{
									if( node.Tag is VLTClass )
									{
										VLTClass cls = node.Tag as VLTClass;
										this.DumpTree( cls, args );
									}
									else
									{
										VLTDataRow row = node.Tag as VLTDataRow;
										this.DumpTree( row, args );
									}
								}
								catch( Exception e )
								{
									this.AddToConsole( e.Message );
								}
							}
						}
						break;
					case "classdump":
						this.ClassDumpAll();
						break;
					case "":
						break;
					default:
						this.AddToConsole( "Unknown command." );
						break;
				}

			}
			catch( Exception ex )
			{
				this.AddToConsole( "Exception: " + ex.ToString() );
				this.AddToConsole( "Error while executing: " + text );
			}


		}
		//- Menu Event Handlers ----------------------------------//


		private void mnuTVCopyNodeName_Click( object sender, System.EventArgs e )
		{
			if( this.tv.SelectedNode != null )
			{
				TreeNode node = this.tv.SelectedNode;
				if( node.Tag is VLTDatabase )
				{
					Clipboard.SetDataObject( "Database" );
				}
				else if( node.Tag is VLTClass )
				{
					VLTClass cls = node.Tag as VLTClass;
					Clipboard.SetDataObject( HashResolver.Resolve( cls.ClassHash ) );
				}
				else
				{
					VLTDataRow row = node.Tag as VLTDataRow;
					Clipboard.SetDataObject( HashResolver.Resolve( row.CollectionLoad.NameHash ) );
				}
			}
		}

		private void mnuTVCopyNodePath_Click( object sender, System.EventArgs e )
		{
			if( this.tv.SelectedNode != null )
			{
				TreeNode node = this.tv.SelectedNode;
				if( node.Tag is VLTDatabase )
				{
					Clipboard.SetDataObject( "" );
				}
				else if( node.Tag is VLTClass )
				{
					VLTClass cls = node.Tag as VLTClass;
					Clipboard.SetDataObject( HashResolver.Resolve( cls.ClassHash ) );
				}
				else
				{
					VLTDataRow row = node.Tag as VLTDataRow;
					VLTClass cls = row.VLTClass;
					Clipboard.SetDataObject( HashResolver.Resolve( cls.ClassHash ) + "/" +
										this.TrackRow( row ) );
				}
			}
		}

		private void mnuFieldCopyName_Click( object sender, System.EventArgs e )
		{

			if( this.tvFields.SelectedNode != null )
			{
				TreeNode node = this.tvFields.SelectedNode;
				VLTDataItem di = node.Tag as VLTDataItem;
				if( node.Parent != null )
				{
					VLTDataItem dip = node.Parent.Tag as VLTDataItemArray;
					Clipboard.SetDataObject( HashResolver.Resolve( dip.NameHash ) +
										"[" + di.ArrayIndex + "]" );
				}
				else
				{
					Clipboard.SetDataObject( HashResolver.Resolve( di.NameHash ) );
				}
			}

		}

		private void mnuFieldCopyOffset_Click( object sender, System.EventArgs e )
		{

			if( this.tvFields.SelectedNode != null )
			{
				TreeNode node = this.tvFields.SelectedNode;
				if( node.Tag is VLTDataItem && !( node.Tag is VLTDataItemArray ) )
				{
					VLTDataItem di = node.Tag as VLTDataItem;
					Clipboard.SetDataObject( string.Format( "0x{0}", di.Offset ) );
				}
			}

		}

		private void mnuFieldCopyOffsetExt_Click( object sender, System.EventArgs e )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode node = this.tvFields.SelectedNode;
				if( node.Tag is VLTDataItem && !( node.Tag is VLTDataItemArray ) )
				{
					VLTDataItem di = node.Tag as VLTDataItem;
					Clipboard.SetDataObject( string.Format( "{0}:0x{1}",
						di.InlineData ? "vlt" : "bin", di.Offset ) );
				}
			}
		}

		private void mnuFieldDump_Click( object sender, System.EventArgs e )
		{
			if( this.tvFields.SelectedNode != null )
			{
				TreeNode node = this.tvFields.SelectedNode;
				if( node.Parent != null )
				{
					node = node.Parent;
				}

				VLTDataItem di = node.Tag as VLTDataItem;
				this.DumpTree( di.DataRow, di.NameHash );

			}
		}

		private void mnuFileExit_Click( object sender, System.EventArgs e )
		{
			this.ExitApplication();
		}

		private void mnuFileOpen_Click( object sender, System.EventArgs e )
		{
			this.ShowOpenDialog();
		}

		private void mnuFileUnloadSub_Click( object sender, System.EventArgs e )
		{
			MenuItem menuItem = sender as MenuItem;
			this.UnloadVLTFile( menuItem.Text );
		}

		//- TreeView Event Handlers ------------------------------//

		private void tv_AfterSelect( object sender, System.Windows.Forms.TreeViewEventArgs e )
		{
			object tag = e.Node.Tag;
			if( tag is VLTClass )
			{
				this.classGrid.Visible = true;
				this.pnlData.Visible = false;
				VLTClass vltClass = tag as VLTClass;

				DataSet ds = new DataSet( "VLT" );
				DataTable dt = ds.Tables.Add( "Fields" );
				dt.Columns.Add( "Name", typeof( string ) );
				dt.Columns.Add( "Type", typeof( string ) );
				dt.Columns.Add( "Length", typeof( ushort ) );
#if SHOW_INTERNAL
				dt.Columns.Add( "Offset", typeof( string ) );
#endif
				dt.Columns.Add( "Count", typeof( short ) );
#if SHOW_INTERNAL
				dt.Columns.Add( "Alignment", typeof( string ) );
				dt.Columns.Add( "Flags", typeof( string ) );
#endif
				foreach( VLTClass.ClassField field in vltClass )
				{
					DataRow dr = dt.NewRow();
#if SHOW_INTERNAL
					dr[0] = HashResolver.Resolve( field.NameHash );
					dr[1] = HashResolver.Resolve( field.TypeHash );
					dr[2] = field.Length;
					dr[3] = string.Format( "0x{0:x}", field.Offset );
					dr[4] = field.Count;
					dr[5] = string.Format( "0x{0:x}", field.Alignment );
					dr[6] = string.Format( "0x{0:x}", field.Flags );
#else
					dr[0] = HashResolver.Resolve(field.NameHash);
					dr[1] = HashResolver.Resolve(field.TypeHash);
					dr[2] = field.Length;
					dr[3] = field.Count;
#endif
					dt.Rows.Add( dr );
				}
				this.classGrid.DataSource = ds;
				this.classGrid.DataMember = "Fields";

				CurrencyManager cm = (CurrencyManager)this.BindingContext[ds, "Fields"];
				( (DataView)cm.List ).AllowNew = false;
				( (DataView)cm.List ).AllowEdit = false;
				( (DataView)cm.List ).AllowDelete = false;

				VLTPointers pointers = vltClass.VLTFile.GetChunk( VLTChunkId.Pointers ) as VLTPointers;
				System.Diagnostics.Debug.WriteLine( string.Format( "Class offset: 0x{0:x}", pointers[vltClass.ClassLoad.Pointer].OffsetDest ) );

				this.classGrid.Update();
			}
			else if( tag is VLTDataRow )
			{
				this.lblFieldType.Text = "";
				this.lblFieldOffset.Text = "";

				this.dataGrid.DataSource = null;
				this.dataGrid.Update();

				this.classGrid.Visible = false;
				this.pnlData.Visible = true;

				string selNodeText = "";
				string selNodeIndex = "";
				TreeNode selNode = null;
				if( this.tvFields.SelectedNode != null )
				{
					if( this.tvFields.SelectedNode.Parent != null && this.tvFields.SelectedNode.Parent.Tag == null )
					{
						selNodeText = this.tvFields.SelectedNode.Parent.Text;
						selNodeIndex = this.tvFields.SelectedNode.Text;
					}
					else
					{
						selNodeText = this.tvFields.SelectedNode.Text;
					}
				}

				VLTDataRow dataRow = tag as VLTDataRow;
				VLTClass vltClass = dataRow.VLTClass;
				this.tvFields.BeginUpdate();
				this.tvFields.Nodes.Clear();
				int index = 0;
				foreach( VLTClass.ClassField field in vltClass )
				{
					VLTDataItem dataItem = dataRow[index++];

					if( field.IsOptional )
					{
						if( !dataRow.IsDataLoaded( index - 1 ) )
						{
							continue;
						}
					}

					if( field.IsArray )
					{
						VLTDataItemArray array = dataItem as VLTDataItemArray;

						string name = HashResolver.Resolve( field.NameHash ) + " [" +
										array.ValidCount + "/" + array.MaxCount + "]";
						TreeNode node = this.tvFields.Nodes.Add( name );
						node.Tag = dataItem;
						for( int i = 0; i < array.ValidCount; i++ )
						{
							TreeNode subNode = node.Nodes.Add( "[" + i + "]" );
							subNode.Tag = array[i];
							if( node.Text == selNodeText && subNode.Text == selNodeIndex )
							{
								selNode = subNode;
							}
						}
						if( node.Text == selNodeText && selNode == null )
						{
							selNode = node;
						}
					}
					else
					{
						TreeNode node = this.tvFields.Nodes.Add( HashResolver.Resolve( field.NameHash ) );
						node.Tag = dataItem;
						if( node.Text == selNodeText )
						{
							selNode = node;
						}
					}
				}
				if( this.tvFields.Nodes.Count > 0 )
				{
					if( selNode == null )
					{
						this.tvFields.SelectedNode = this.tvFields.Nodes[0];
					}
					else
					{
						this.tvFields.SelectedNode = selNode;
					}
				}
				this.tvFields.EndUpdate();

				/*
				VLTPointers pointers = dataRow.VLTFile.GetChunk(VLTChunkId.Pointers) as VLTPointers;
				System.Diagnostics.Debug.WriteLine(string.Format("CollLoad offset: 0x{0:x}", dataRow.CollectionLoad.Address));
				System.Diagnostics.Debug.WriteLine(string.Format("Data offset: 0x{0:x}", pointers[dataRow.CollectionLoad.Pointer].OffsetDest));
				*/

			}
			else
			{
				this.classGrid.Visible = false;
				this.pnlData.Visible = false;
			}
		}

		private void tvFields_AfterSelect( object sender, System.Windows.Forms.TreeViewEventArgs e )
		{
			object tag = e.Node.Tag;

			if( tag is VLTDataItem && !( tag is VLTDataItemArray ) )
			{
				VLTDataItem dataItem = tag as VLTDataItem;

				dataItem.LoadExtra();

				DataSet ds = new DataSet( "DataItem" );
				DataTable dt = ds.Tables.Add( "Values" );
				dt.Columns.Add( "Name", typeof( string ) );
				dt.Columns.Add( "Value", typeof( string ) );

				Type type = dataItem.GetType();
				FieldInfo[] fields = type.GetFields();
				foreach( FieldInfo field in fields )
				{
					object[] attributes = field.GetCustomAttributes( typeof( DataValueAttribute ), false );
					if( attributes != null && attributes.Length == 1 && attributes[0] is DataValueAttribute )
					{
						DataValueAttribute attrib = attributes[0] as DataValueAttribute;

						DataRow dr = dt.NewRow();
						dr[0] = attrib.Name;
						object value = field.GetValue( dataItem );
						if( value == null )
						{
							dr[1] = "(null)";
						}
						else
						{
							if( attrib.Hex )
							{
								dr[1] = string.Format( "0x{0:x}", value );
							}
							else
							{
								dr[1] = value.ToString();
							}
						}
						dt.Rows.Add( dr );
					}
				}
				this.dataGrid.DataSource = ds;
				this.dataGrid.DataMember = "Values";

				CurrencyManager cm = (CurrencyManager)this.BindingContext[ds, "Values"];
				( (DataView)cm.List ).AllowNew = false;
				( (DataView)cm.List ).AllowEdit = false;
				( (DataView)cm.List ).AllowDelete = false;

				this.lblFieldType.Text = "Type: " + HashResolver.Resolve( dataItem.TypeHash );

				VLTDependency dep = dataItem.DataRow.VLTFile.GetChunk( VLTChunkId.Dependency ) as VLTDependency;
				string name = dep.GetName( 0 );
				if( dataItem.Offset != 0 )
				{
					this.lblFieldOffset.Text = string.Format( "Offset: {0}:0x{1:x}  ({2})",
						( dataItem.InlineData ? "vlt" : "bin" ), dataItem.Offset, name );
				}
				else
				{
					this.lblFieldOffset.Text = "Null Data";
				}

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


		//- Other Handlers -------------------------------------//

		private void MainForm_Load( object sender, System.EventArgs e )
		{
			this.AddToConsole( "VLTEdit " + Application.ProductVersion );
			this.AddToConsole( "Copyright (C) 2005-2006, Arushan" );
#if CARBON
			this.AddToConsole( "Enabled Feature: Carbon Support" );
#endif
#if SHOW_INTERNAL
			this.AddToConsole( "Enabled Feature: Show Internals" );
#endif
			this.txtConsoleInput.Focus();
		}

		private void txtConsoleInput_KeyPress( object sender, System.Windows.Forms.KeyPressEventArgs e )
		{
			if( e.KeyChar == (char)13 )
			{
				string text = this.txtConsoleInput.Text;
				if( text != "" )
				{
					this.txtConsoleInput.Text = "";
					this.txtConsoleInput.Refresh();
					this.AddToConsole( "> " + text );
					this.ParseConsoleInput( text );
					this.txtConsoleInput.Focus();
					e.Handled = true;
				}
			}
		}


	}
}
