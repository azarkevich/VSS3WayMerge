namespace Vss3WayMerge
{
	partial class ScanRules
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxRules = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxRules
			// 
			this.textBoxRules.AcceptsReturn = true;
			this.textBoxRules.AcceptsTab = true;
			this.textBoxRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRules.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "ScanRules", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxRules.Location = new System.Drawing.Point(12, 12);
			this.textBoxRules.Multiline = true;
			this.textBoxRules.Name = "textBoxRules";
			this.textBoxRules.Size = new System.Drawing.Size(565, 172);
			this.textBoxRules.TabIndex = 0;
			this.textBoxRules.Text = global::Vss3WayMerge.Properties.Settings.Default.ScanRules;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(502, 190);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// ScanRules
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(589, 225);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxRules);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ScanRules";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Scan Rules";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxRules;
		private System.Windows.Forms.Button buttonOK;
	}
}