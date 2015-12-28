using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		}

		private void exit()
		{
			Application.Exit();
		}

		private void menuOpenClicked( object A_0, EventArgs A_1 )
		{
			//this.showOpenFileDialog();
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
	}
}
