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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// lstErrors
			// 
			this.lstErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstErrors.DisplayMember = "Message";
			this.lstErrors.Dock = System.Windows.Forms.DockStyle.Top;
			this.lstErrors.FormattingEnabled = true;
			this.lstErrors.ItemHeight = 12;
			this.lstErrors.Location = new System.Drawing.Point(5, 65);
			this.lstErrors.Name = "lstErrors";
			this.lstErrors.Size = new System.Drawing.Size(834, 288);
			this.lstErrors.TabIndex = 1;
			this.lstErrors.SelectedIndexChanged += new System.EventHandler(this.lstErrors_SelectedIndexChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(5, 353);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(834, 134);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// FrmErrors
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 492);
			this.Controls.Add(this.richTextBox1);
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
		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}