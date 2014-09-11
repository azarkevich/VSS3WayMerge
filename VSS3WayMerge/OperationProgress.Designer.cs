namespace Vss3WayMerge
{
	partial class OperationProgress
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
			this.components = new System.ComponentModel.Container();
			this.labelCurrentItem = new System.Windows.Forms.Label();
			this.timerUpdateCurrentItem = new System.Windows.Forms.Timer(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// labelCurrentItem
			// 
			this.labelCurrentItem.AutoSize = true;
			this.labelCurrentItem.Location = new System.Drawing.Point(55, 9);
			this.labelCurrentItem.Name = "labelCurrentItem";
			this.labelCurrentItem.Size = new System.Drawing.Size(35, 13);
			this.labelCurrentItem.TabIndex = 0;
			this.labelCurrentItem.Text = "label1";
			// 
			// timerUpdateCurrentItem
			// 
			this.timerUpdateCurrentItem.Enabled = true;
			this.timerUpdateCurrentItem.Interval = 500;
			this.timerUpdateCurrentItem.Tick += new System.EventHandler(this.timerUpdateCurrentItem_Tick);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Vss3WayMerge.Properties.Resources.spin;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(46, 34);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// OperationProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 40);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelCurrentItem);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OperationProgress";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Please wait...";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelCurrentItem;
		private System.Windows.Forms.Timer timerUpdateCurrentItem;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}