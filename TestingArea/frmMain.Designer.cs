namespace NFSTools.TestingArea
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
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
			this.btnJDLZCompress = new System.Windows.Forms.Button();
			this.btnJDLZDecompress = new System.Windows.Forms.Button();
			this.txtConsole = new System.Windows.Forms.TextBox();
			this.btnRAWWCompress = new System.Windows.Forms.Button();
			this.btnRAWWDecompress = new System.Windows.Forms.Button();
			this.btnAUTOCompress = new System.Windows.Forms.Button();
			this.btnAUTODecompress = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnJDLZCompress
			// 
			this.btnJDLZCompress.Location = new System.Drawing.Point(12, 12);
			this.btnJDLZCompress.Name = "btnJDLZCompress";
			this.btnJDLZCompress.Size = new System.Drawing.Size(128, 23);
			this.btnJDLZCompress.TabIndex = 0;
			this.btnJDLZCompress.Text = "JDLZ Compress...";
			this.btnJDLZCompress.UseVisualStyleBackColor = true;
			this.btnJDLZCompress.Click += new System.EventHandler(this.btnJDLZCompress_Click);
			// 
			// btnJDLZDecompress
			// 
			this.btnJDLZDecompress.Location = new System.Drawing.Point(12, 41);
			this.btnJDLZDecompress.Name = "btnJDLZDecompress";
			this.btnJDLZDecompress.Size = new System.Drawing.Size(128, 23);
			this.btnJDLZDecompress.TabIndex = 1;
			this.btnJDLZDecompress.Text = "JDLZ Decompress...";
			this.btnJDLZDecompress.UseVisualStyleBackColor = true;
			this.btnJDLZDecompress.Click += new System.EventHandler(this.btnJDLZDecompress_Click);
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtConsole.Location = new System.Drawing.Point(12, 70);
			this.txtConsole.MaxLength = 65535;
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size(553, 250);
			this.txtConsole.TabIndex = 2;
			// 
			// btnRAWWCompress
			// 
			this.btnRAWWCompress.Location = new System.Drawing.Point(146, 12);
			this.btnRAWWCompress.Name = "btnRAWWCompress";
			this.btnRAWWCompress.Size = new System.Drawing.Size(128, 23);
			this.btnRAWWCompress.TabIndex = 3;
			this.btnRAWWCompress.Text = "RAWW Compress...";
			this.btnRAWWCompress.UseVisualStyleBackColor = true;
			this.btnRAWWCompress.Click += new System.EventHandler(this.btnRAWWCompress_Click);
			// 
			// btnRAWWDecompress
			// 
			this.btnRAWWDecompress.Location = new System.Drawing.Point(146, 41);
			this.btnRAWWDecompress.Name = "btnRAWWDecompress";
			this.btnRAWWDecompress.Size = new System.Drawing.Size(128, 23);
			this.btnRAWWDecompress.TabIndex = 4;
			this.btnRAWWDecompress.Text = "RAWW Decompress...";
			this.btnRAWWDecompress.UseVisualStyleBackColor = true;
			this.btnRAWWDecompress.Click += new System.EventHandler(this.btnRAWWDecompress_Click);
			// 
			// btnAUTOCompress
			// 
			this.btnAUTOCompress.Location = new System.Drawing.Point(280, 12);
			this.btnAUTOCompress.Name = "btnAUTOCompress";
			this.btnAUTOCompress.Size = new System.Drawing.Size(128, 23);
			this.btnAUTOCompress.TabIndex = 5;
			this.btnAUTOCompress.Text = "AUTO Compress...";
			this.btnAUTOCompress.UseVisualStyleBackColor = true;
			this.btnAUTOCompress.Click += new System.EventHandler(this.btnAUTOCompress_Click);
			// 
			// btnAUTODecompress
			// 
			this.btnAUTODecompress.Location = new System.Drawing.Point(280, 41);
			this.btnAUTODecompress.Name = "btnAUTODecompress";
			this.btnAUTODecompress.Size = new System.Drawing.Size(128, 23);
			this.btnAUTODecompress.TabIndex = 6;
			this.btnAUTODecompress.Text = "AUTO Decompress...";
			this.btnAUTODecompress.UseVisualStyleBackColor = true;
			this.btnAUTODecompress.Click += new System.EventHandler(this.btnAUTODecompress_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(577, 332);
			this.Controls.Add(this.btnAUTODecompress);
			this.Controls.Add(this.btnAUTOCompress);
			this.Controls.Add(this.btnRAWWDecompress);
			this.Controls.Add(this.btnRAWWCompress);
			this.Controls.Add(this.txtConsole);
			this.Controls.Add(this.btnJDLZDecompress);
			this.Controls.Add(this.btnJDLZCompress);
			this.Name = "frmMain";
			this.Text = "OpenNFSTools Testing";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnJDLZCompress;
		private System.Windows.Forms.Button btnJDLZDecompress;
		private System.Windows.Forms.TextBox txtConsole;
		private System.Windows.Forms.Button btnRAWWCompress;
		private System.Windows.Forms.Button btnRAWWDecompress;
		private System.Windows.Forms.Button btnAUTOCompress;
		private System.Windows.Forms.Button btnAUTODecompress;
	}
}

