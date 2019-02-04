namespace NFSTools.VLTEditLegacy
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
			this.classGrid = new System.Windows.Forms.DataGridView();
			this.pnlData = new System.Windows.Forms.Panel();
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.pnlFieldInfo = new System.Windows.Forms.Panel();
			this.lblFieldOffset = new System.Windows.Forms.Label();
			this.lblFieldType = new System.Windows.Forms.Label();
			this.spltFieldData = new System.Windows.Forms.Splitter();
			this.tvFields = new System.Windows.Forms.TreeView();
			this.pnlBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.classGrid)).BeginInit();
			this.pnlData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.pnlFieldInfo.SuspendLayout();
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
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new System.Drawing.Size(200, 437);
			this.tv.TabIndex = 1;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// spltLeft
			// 
			this.spltLeft.Location = new System.Drawing.Point(200, 0);
			this.spltLeft.Name = "spltLeft";
			this.spltLeft.Size = new System.Drawing.Size(4, 437);
			this.spltLeft.TabIndex = 3;
			this.spltLeft.TabStop = false;
			// 
			// classGrid
			// 
			this.classGrid.AllowUserToAddRows = false;
			this.classGrid.AllowUserToDeleteRows = false;
			this.classGrid.AllowUserToResizeRows = false;
			this.classGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.classGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.classGrid.Location = new System.Drawing.Point(204, 0);
			this.classGrid.Name = "classGrid";
			this.classGrid.ReadOnly = true;
			this.classGrid.RowHeadersVisible = false;
			this.classGrid.RowTemplate.Height = 18;
			this.classGrid.RowTemplate.ReadOnly = true;
			this.classGrid.Size = new System.Drawing.Size(648, 437);
			this.classGrid.TabIndex = 2;
			this.classGrid.Visible = false;
			// 
			// pnlData
			// 
			this.pnlData.Controls.Add(this.dataGrid);
			this.pnlData.Controls.Add(this.pnlFieldInfo);
			this.pnlData.Controls.Add(this.spltFieldData);
			this.pnlData.Controls.Add(this.tvFields);
			this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlData.Location = new System.Drawing.Point(204, 0);
			this.pnlData.Name = "pnlData";
			this.pnlData.Size = new System.Drawing.Size(648, 437);
			this.pnlData.TabIndex = 3;
			this.pnlData.Visible = false;
			// 
			// dataGrid
			// 
			this.dataGrid.AllowUserToAddRows = false;
			this.dataGrid.AllowUserToDeleteRows = false;
			this.dataGrid.AllowUserToResizeRows = false;
			this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.Location = new System.Drawing.Point(180, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.RowHeadersVisible = false;
			this.dataGrid.RowTemplate.Height = 18;
			this.dataGrid.RowTemplate.ReadOnly = true;
			this.dataGrid.Size = new System.Drawing.Size(468, 389);
			this.dataGrid.TabIndex = 1;
			// 
			// pnlFieldInfo
			// 
			this.pnlFieldInfo.Controls.Add(this.lblFieldOffset);
			this.pnlFieldInfo.Controls.Add(this.lblFieldType);
			this.pnlFieldInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlFieldInfo.Location = new System.Drawing.Point(180, 389);
			this.pnlFieldInfo.Name = "pnlFieldInfo";
			this.pnlFieldInfo.Size = new System.Drawing.Size(468, 48);
			this.pnlFieldInfo.TabIndex = 5;
			// 
			// lblFieldOffset
			// 
			this.lblFieldOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFieldOffset.Location = new System.Drawing.Point(8, 28);
			this.lblFieldOffset.Name = "lblFieldOffset";
			this.lblFieldOffset.Size = new System.Drawing.Size(456, 16);
			this.lblFieldOffset.TabIndex = 6;
			this.lblFieldOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFieldType
			// 
			this.lblFieldType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFieldType.Location = new System.Drawing.Point(8, 8);
			this.lblFieldType.Name = "lblFieldType";
			this.lblFieldType.Size = new System.Drawing.Size(456, 16);
			this.lblFieldType.TabIndex = 5;
			this.lblFieldType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// spltFieldData
			// 
			this.spltFieldData.Location = new System.Drawing.Point(176, 0);
			this.spltFieldData.Name = "spltFieldData";
			this.spltFieldData.Size = new System.Drawing.Size(4, 437);
			this.spltFieldData.TabIndex = 4;
			this.spltFieldData.TabStop = false;
			// 
			// tvFields
			// 
			this.tvFields.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvFields.HideSelection = false;
			this.tvFields.Location = new System.Drawing.Point(0, 0);
			this.tvFields.Name = "tvFields";
			this.tvFields.Size = new System.Drawing.Size(176, 437);
			this.tvFields.TabIndex = 0;
			this.tvFields.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFields_AfterSelect);
			// 
			// frmDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(852, 593);
			this.Controls.Add(this.pnlData);
			this.Controls.Add(this.classGrid);
			this.Controls.Add(this.spltLeft);
			this.Controls.Add(this.tv);
			this.Controls.Add(this.spltBottom);
			this.Controls.Add(this.pnlBottom);
			this.Name = "frmDesigner";
			this.Text = "VLTEdit";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDesigner_FormClosed);
			this.Load += new System.EventHandler(this.onLoad);
			this.pnlBottom.ResumeLayout(false);
			this.pnlBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.classGrid)).EndInit();
			this.pnlData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.pnlFieldInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.TextBox txtConsole;
		private System.Windows.Forms.TextBox txtConsoleInput;
		private System.Windows.Forms.Splitter spltBottom;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Splitter spltLeft;
		private System.Windows.Forms.DataGridView classGrid;
		private System.Windows.Forms.Panel pnlData;
		private System.Windows.Forms.TreeView tvFields;
		private System.Windows.Forms.Splitter spltFieldData;
		private System.Windows.Forms.Panel pnlFieldInfo;
		private System.Windows.Forms.DataGridView dataGrid;
		private System.Windows.Forms.Label lblFieldType;
		private System.Windows.Forms.Label lblFieldOffset;
	}
}