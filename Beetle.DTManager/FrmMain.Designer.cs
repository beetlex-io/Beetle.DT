namespace Beetle.DTManager
{
	partial class FrmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.txtAddress = new System.Windows.Forms.ToolStripTextBox();
			this.txtPort = new System.Windows.Forms.ToolStripTextBox();
			this.cmdConnect = new System.Windows.Forms.ToolStripButton();
			this.cmdDisconnect = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdNew = new System.Windows.Forms.ToolStripButton();
			this.cmdDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdUpload = new System.Windows.Forms.ToolStripButton();
			this.cmdDeleteFiles = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdRun = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lstNodes = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lstFiles = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lstFolders = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdAbout = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAddress,
            this.txtPort,
            this.cmdConnect,
            this.cmdDisconnect,
            this.toolStripSeparator3,
            this.cmdNew,
            this.cmdDelete,
            this.toolStripSeparator1,
            this.cmdUpload,
            this.cmdDeleteFiles,
            this.toolStripSeparator2,
            this.cmdRun,
            this.toolStripSeparator4,
            this.cmdAbout});
			this.toolStrip1.Location = new System.Drawing.Point(6, 70);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1095, 39);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// txtAddress
			// 
			this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(100, 39);
			this.txtAddress.Text = "127.0.0.1";
			// 
			// txtPort
			// 
			this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(60, 39);
			this.txtPort.Text = "9092";
			// 
			// cmdConnect
			// 
			this.cmdConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdConnect.Image = ((System.Drawing.Image)(resources.GetObject("cmdConnect.Image")));
			this.cmdConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdConnect.Name = "cmdConnect";
			this.cmdConnect.Size = new System.Drawing.Size(36, 36);
			this.cmdConnect.Text = "Connect to center";
			this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
			// 
			// cmdDisconnect
			// 
			this.cmdDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdDisconnect.Enabled = false;
			this.cmdDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("cmdDisconnect.Image")));
			this.cmdDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdDisconnect.Name = "cmdDisconnect";
			this.cmdDisconnect.Size = new System.Drawing.Size(36, 36);
			this.cmdDisconnect.Text = "Disconnect";
			this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
			// 
			// cmdNew
			// 
			this.cmdNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdNew.Enabled = false;
			this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
			this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(36, 36);
			this.cmdNew.Text = "Create folder";
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdDelete.Enabled = false;
			this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
			this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(36, 36);
			this.cmdDelete.Text = "delete folder";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// cmdUpload
			// 
			this.cmdUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdUpload.Enabled = false;
			this.cmdUpload.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpload.Image")));
			this.cmdUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdUpload.Name = "cmdUpload";
			this.cmdUpload.Size = new System.Drawing.Size(36, 36);
			this.cmdUpload.Text = "Update load files";
			this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
			// 
			// cmdDeleteFiles
			// 
			this.cmdDeleteFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdDeleteFiles.Enabled = false;
			this.cmdDeleteFiles.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteFiles.Image")));
			this.cmdDeleteFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdDeleteFiles.Name = "cmdDeleteFiles";
			this.cmdDeleteFiles.Size = new System.Drawing.Size(36, 36);
			this.cmdDeleteFiles.Text = "Delete files";
			this.cmdDeleteFiles.Click += new System.EventHandler(this.cmdDeleteFiles_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			// 
			// cmdRun
			// 
			this.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdRun.Enabled = false;
			this.cmdRun.Image = ((System.Drawing.Image)(resources.GetObject("cmdRun.Image")));
			this.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(36, 36);
			this.cmdRun.Text = "Runing unit test";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus});
			this.statusStrip1.Location = new System.Drawing.Point(6, 670);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1095, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// txtStatus
			// 
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.lstNodes);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(6, 419);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1095, 251);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// lstNodes
			// 
			this.lstNodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstNodes.CheckBoxes = true;
			this.lstNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.lstNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstNodes.FullRowSelect = true;
			this.lstNodes.LargeImageList = this.imageList2;
			this.lstNodes.Location = new System.Drawing.Point(3, 17);
			this.lstNodes.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
			this.lstNodes.Name = "lstNodes";
			this.lstNodes.Size = new System.Drawing.Size(1089, 231);
			this.lstNodes.SmallImageList = this.imageList2;
			this.lstNodes.TabIndex = 2;
			this.lstNodes.UseCompatibleStateImageBehavior = false;
			this.lstNodes.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Host";
			this.columnHeader5.Width = 473;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "CPU";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader6.Width = 132;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Available MBytes";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader7.Width = 134;
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "服务器.png");
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.White;
			this.groupBox2.Controls.Add(this.lstFiles);
			this.groupBox2.Controls.Add(this.lstFolders);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox2.Location = new System.Drawing.Point(6, 109);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.groupBox2.Size = new System.Drawing.Size(1095, 310);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			// 
			// lstFiles
			// 
			this.lstFiles.AllowDrop = true;
			this.lstFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFiles.FullRowSelect = true;
			this.lstFiles.HideSelection = false;
			this.lstFiles.Location = new System.Drawing.Point(208, 16);
			this.lstFiles.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
			this.lstFiles.Name = "lstFiles";
			this.lstFiles.Size = new System.Drawing.Size(883, 292);
			this.lstFiles.SmallImageList = this.imageList1;
			this.lstFiles.TabIndex = 3;
			this.lstFiles.UseCompatibleStateImageBehavior = false;
			this.lstFiles.View = System.Windows.Forms.View.Details;
			this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
			this.lstFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
			this.lstFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "FileName";
			this.columnHeader1.Width = 324;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "UpdateDate";
			this.columnHeader2.Width = 202;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Version";
			this.columnHeader3.Width = 125;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "文件夹.png");
			this.imageList1.Images.SetKeyName(1, "if_document_2318462.png");
			this.imageList1.Images.SetKeyName(2, "文件夹 (1).png");
			// 
			// lstFolders
			// 
			this.lstFolders.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstFolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
			this.lstFolders.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstFolders.FullRowSelect = true;
			this.lstFolders.HideSelection = false;
			this.lstFolders.Location = new System.Drawing.Point(4, 16);
			this.lstFolders.MultiSelect = false;
			this.lstFolders.Name = "lstFolders";
			this.lstFolders.Size = new System.Drawing.Size(204, 292);
			this.lstFolders.SmallImageList = this.imageList1;
			this.lstFolders.TabIndex = 2;
			this.lstFolders.UseCompatibleStateImageBehavior = false;
			this.lstFolders.View = System.Windows.Forms.View.Details;
			this.lstFolders.SelectedIndexChanged += new System.EventHandler(this.lstFolders_SelectedIndexChanged);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Folders";
			this.columnHeader4.Width = 198;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Multiselect = true;
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
			// 
			// cmdAbout
			// 
			this.cmdAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdAbout.Image = ((System.Drawing.Image)(resources.GetObject("cmdAbout.Image")));
			this.cmdAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdAbout.Name = "cmdAbout";
			this.cmdAbout.Size = new System.Drawing.Size(36, 36);
			this.cmdAbout.Text = "About me";
			this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
			// 
			// FrmMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1107, 698);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmMain";
			this.Padding = new System.Windows.Forms.Padding(6, 70, 6, 6);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Performance Test  Manager";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripTextBox txtAddress;
		private System.Windows.Forms.ToolStripTextBox txtPort;
		private System.Windows.Forms.ToolStripButton cmdConnect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel txtStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lstNodes;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ToolStripButton cmdDisconnect;
		private System.Windows.Forms.ToolStripButton cmdNew;
		private System.Windows.Forms.ToolStripButton cmdDelete;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton cmdRun;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton cmdUpload;
		private System.Windows.Forms.ToolStripButton cmdDeleteFiles;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ListView lstFiles;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView lstFolders;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton cmdAbout;
	}
}

