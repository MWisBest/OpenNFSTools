using System;
using System.Windows.Forms;

namespace NFSTools.TestingArea
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new frmMain() );
		}
	}
}
