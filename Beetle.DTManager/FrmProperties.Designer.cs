namespace Beetle.DTManager
{
	partial class FrmProperties
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
			this.panelControls = new System.Windows.Forms.Panel();
			this.cdmComfirn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panelControls
			// 
			this.panelControls.AutoScroll = true;
			this.panelControls.BackColor = System.Drawing.Color.White;
			this.panelControls.Location = new System.Drawing.Point(12, 80);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(609, 374);
			this.panelControls.TabIndex = 0;
			// 
			// cdmComfirn
			// 
			this.cdmComfirn.Location = new System.Drawing.Point(277, 472);
			this.cdmComfirn.Name = "cdmComfirn";
			this.cdmComfirn.Size = new System.Drawing.Size(75, 23);
			this.cdmComfirn.TabIndex = 1;
			this.cdmComfirn.Text = "Confirm";
			this.cdmComfirn.UseVisualStyleBackColor = true;
			// 
			// FrmProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 514);
			this.Controls.Add(this.cdmComfirn);
			this.Controls.Add(this.panelControls);
			this.Name = "FrmProperties";
			this.Text = "Case config";
			this.Load += new System.EventHandler(this.FrmProperties_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelControls;
		private System.Windows.Forms.Button cdmComfirn;
	}
}