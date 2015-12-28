namespace VLTEdit
{
	partial class frmDesigner
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
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.txtConsole = new System.Windows.Forms.TextBox();
			this.txtConsoleInput = new System.Windows.Forms.TextBox();
			this.spltBottom = new System.Windows.Forms.Splitter();
			this.tv = new System.Windows.Forms.TreeView();
			this.spltLeft = new System.Windows.Forms.Splitter();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.txtConsole);
			this.pnlBottom.Controls.Add(this.txtConsoleInput);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 441);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(852, 152);
			this.pnlBottom.TabIndex = 0;
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtConsole.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtConsole.Location = new System.Drawing.Point(0, 0);
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size(852, 132);
			this.txtConsole.TabIndex = 1;
			// 
			// txtConsoleInput
			// 
			this.txtConsoleInput.AcceptsReturn = true;
			this.txtConsoleInput.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtConsoleInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtConsoleInput.Location = new System.Drawing.Point(0, 132);
			this.txtConsoleInput.Name = "txtConsoleInput";
			this.txtConsoleInput.Size = new System.Drawing.Size(852, 20);
			this.txtConsoleInput.TabIndex = 0;
			this.txtConsoleInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsoleInput_KeyPress);
			// 
			// spltBottom
			// 
			this.spltBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.spltBottom.Location = new System.Drawing.Point(0, 437);
			this.spltBottom.Name = "spltBottom";
			this.spltBottom.Size = new System.Drawing.Size(852, 4);
			this.spltBottom.TabIndex = 5;
			this.spltBottom.TabStop = false;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Left;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(200, 437);
			this.tv.TabIndex = 1;
			// 
			// spltLeft
			// 
			this.spltLeft.Location = new System.Drawing.Point(200, 0);
			this.spltLeft.Name = "spltLeft";
			this.spltLeft.Size = new System.Drawing.Size(4, 437);
			this.spltLeft.TabIndex = 3;
			this.spltLeft.TabStop = false;
			// 
			// frmDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(852, 593);
			this.Controls.Add(this.spltLeft);
			this.Controls.Add(this.tv);
			this.Controls.Add(this.spltBottom);
			this.Controls.Add(this.pnlBottom);
			this.Name = "frmDesigner";
			this.Text = "VLTEdit";
			this.Load += new System.EventHandler(this.onLoad);
			this.pnlBottom.ResumeLayout(false);
			this.pnlBottom.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.TextBox txtConsole;
		private System.Windows.Forms.TextBox txtConsoleInput;
		private System.Windows.Forms.Splitter spltBottom;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Splitter spltLeft;
	}
}