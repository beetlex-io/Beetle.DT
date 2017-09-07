namespace Beetle.DTManager
{
	partial class FrmErrors
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
			this.lstErrors = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lstErrors
			// 
			this.lstErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstErrors.DisplayMember = "Message";
			this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstErrors.FormattingEnabled = true;
			this.lstErrors.ItemHeight = 12;
			this.lstErrors.Location = new System.Drawing.Point(5, 65);
			this.lstErrors.Name = "lstErrors";
			this.lstErrors.Size = new System.Drawing.Size(834, 422);
			this.lstErrors.TabIndex = 1;
			this.lstErrors.SelectedIndexChanged += new System.EventHandler(this.lstErrors_SelectedIndexChanged);
			// 
			// FrmErrors
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 492);
			this.Controls.Add(this.lstErrors);
			this.Name = "FrmErrors";
			this.Padding = new System.Windows.Forms.Padding(5, 65, 5, 5);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Errors";
			this.Load += new System.EventHandler(this.FrmErrors_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstErrors;
	}
}