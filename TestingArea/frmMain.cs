using System;
using System.IO;
using System.Windows.Forms;
using NFSTools.LibNFS.Compression;

namespace NFSTools.TestingArea
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnJDLZCompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "JDLZ Compress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Compressing..." );
						byte[] output = JDLZ.compress( input );
						using( FileStream outstream = new FileStream( fInfo.FullName + ".jdlz", FileMode.Create, FileAccess.Write ) )
						{
							this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + ".jdlz" );
							outstream.Write( output, 0, output.Length );
						}
						this.writeToConsole( LOG_TAG + "Done!" );
					}
				}
			}
		}

		private void btnJDLZDecompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "JDLZ Decompress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Decompressing..." );
						byte[] output = JDLZ.decompress( input );
						using( FileStream outstream = new FileStream( fInfo.FullName + ".decomp", FileMode.Create, FileAccess.Write ) )
						{
							this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + ".decomp" );
							outstream.Write( output, 0, output.Length );
						}
						this.writeToConsole( LOG_TAG + "Done!" );
					}
				}
			}
		}

		private void writeToConsole( string text )
		{
			if( this.txtConsole.Text != "" )
			{
				this.txtConsole.AppendText( Environment.NewLine );
			}
			this.txtConsole.AppendText( text );
			this.txtConsole.SelectionStart = this.txtConsole.Text.Length;
			this.txtConsole.Refresh();
		}

		private void btnRAWWCompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "RAWW Compress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Compressing..." );
						byte[] output = RAWW.compress( input );
						using( FileStream outstream = new FileStream( fInfo.FullName + ".raww", FileMode.Create, FileAccess.Write ) )
						{
							this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + ".raww" );
							outstream.Write( output, 0, output.Length );
						}
						this.writeToConsole( LOG_TAG + "Done!" );
					}
				}
			}
		}

		private void btnRAWWDecompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "RAWW Decompress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Decompressing..." );
						byte[] output = RAWW.decompress( input );
						using( FileStream outstream = new FileStream( fInfo.FullName + ".decomp", FileMode.Create, FileAccess.Write ) )
						{
							this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + ".decomp" );
							outstream.Write( output, 0, output.Length );
						}
						this.writeToConsole( LOG_TAG + "Done!" );
					}
				}
			}
		}

		private void btnAUTOCompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "AUTO Compress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Compressing..." );
						CompressType usedType;
						byte[] output = CompressUtils.compress_auto( input, out usedType );
						this.writeToConsole( "Compressed With: " + usedType.ToString() );
						using( FileStream outstream = new FileStream( fInfo.FullName + "." + usedType.ToString(), FileMode.Create, FileAccess.Write ) )
						{
							this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + "." + usedType.ToString() );
							outstream.Write( output, 0, output.Length );
						}
						this.writeToConsole( LOG_TAG + "Done!" );
					}
				}
			}
		}

		private void btnAUTODecompress_Click( object sender, EventArgs e )
		{
			const string LOG_TAG = "AUTO Decompress: ";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.ShowReadOnly = true;
			ofd.Title = "Open File...";
			ofd.Multiselect = false;
			if( ofd.ShowDialog() == DialogResult.OK )
			{
				string fileName = ofd.FileName;
				if( fileName != null && fileName != "" )
				{
					FileInfo fInfo = new FileInfo( fileName );
					this.writeToConsole( LOG_TAG + "Opening: " + fInfo.FullName );
					using( FileStream instream = new FileStream( fInfo.FullName, FileMode.Open, FileAccess.Read ) )
					{
						this.writeToConsole( LOG_TAG + "Reading file..." );
						byte[] input = new byte[instream.Length];
						instream.Read( input, 0, input.Length );
						this.writeToConsole( LOG_TAG + "Decompressing..." );
						CompressType usedType;
						byte[] output = CompressUtils.decompress_auto( input, out usedType );
						if( usedType != CompressType.NULL )
						{
							this.writeToConsole( "Detected Compression: " + usedType.ToString() );
							using( FileStream outstream = new FileStream( fInfo.FullName + ".de" + usedType.ToString(), FileMode.Create, FileAccess.Write ) )
							{
								this.writeToConsole( LOG_TAG + "Writing file: " + fInfo.FullName + ".de" + usedType.ToString() );
								outstream.Write( output, 0, output.Length );
							}
							this.writeToConsole( LOG_TAG + "Done!" );
						}
						else
						{
							this.writeToConsole( "Failed to detect valid compression format!" );
						}
					}
				}
			}
		}
	}
}
