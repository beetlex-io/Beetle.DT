namespace Beetle.DTManager
{
	partial class FrmStep1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStep1));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.cmdSync = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.txtUsers = new System.Windows.Forms.ToolStripTextBox();
			this.cmdTest = new System.Windows.Forms.ToolStripButton();
			this.cmdStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdReport = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lstNodes = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstFolders = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "服务器 (1).png");
			this.imageList2.Images.SetKeyName(1, "服务器 (2).png");
			this.imageList2.Images.SetKeyName(2, "测试用例子集.png");
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSync,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtUsers,
            this.cmdTest,
            this.cmdStop,
            this.toolStripSeparator2,
            this.cmdReport});
			this.toolStrip1.Location = new System.Drawing.Point(4, 70);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1132, 39);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// cmdSync
			// 
			this.cmdSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdSync.Image = ((System.Drawing.Image)(resources.GetObject("cmdSync.Image")));
			this.cmdSync.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdSync.Name = "cmdSync";
			this.cmdSync.Size = new System.Drawing.Size(36, 36);
			this.cmdSync.Text = "Synchronize files";
			this.cmdSync.Click += new System.EventHandler(this.cmdSync_Click_1);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(44, 36);
			this.toolStripLabel1.Text = "Users:";
			// 
			// txtUsers
			// 
			this.txtUsers.Name = "txtUsers";
			this.txtUsers.Size = new System.Drawing.Size(60, 39);
			this.txtUsers.Text = "10";
			this.txtUsers.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cmdTest
			// 
			this.cmdTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdTest.Enabled = false;
			this.cmdTest.Image = ((System.Drawing.Image)(resources.GetObject("cmdTest.Image")));
			this.cmdTest.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Size = new System.Drawing.Size(36, 36);
			this.cmdTest.Text = "Runing";
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			// 
			// cmdStop
			// 
			this.cmdStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdStop.Enabled = false;
			this.cmdStop.Image = ((System.Drawing.Image)(resources.GetObject("cmdStop.Image")));
			this.cmdStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdStop.Name = "cmdStop";
			this.cmdStop.Size = new System.Drawing.Size(36, 36);
			this.cmdStop.Text = "Stop";
			this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			// 
			// cmdReport
			// 
			this.cmdReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdReport.Enabled = false;
			this.cmdReport.Image = ((System.Drawing.Image)(resources.GetObject("cmdReport.Image")));
			this.cmdReport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdReport.Name = "cmdReport";
			this.cmdReport.Size = new System.Drawing.Size(36, 36);
			this.cmdReport.Text = "Test report";
			this.cmdReport.Click += new System.EventHandler(this.cmdReport_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.White;
			this.groupBox2.Controls.Add(this.lstNodes);
			this.groupBox2.Controls.Add(this.lstFolders);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(4, 109);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.groupBox2.Size = new System.Drawing.Size(1132, 273);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			// 
			// lstNodes
			// 
			this.lstNodes.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lstNodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.lstNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstNodes.FullRowSelect = true;
			this.lstNodes.LargeImageList = this.imageList2;
			this.lstNodes.Location = new System.Drawing.Point(400, 16);
			this.lstNodes.Margin = new System.Windows.Forms.Padding(3, 16, 10, 3);
			this.lstNodes.Name = "lstNodes";
			this.lstNodes.Size = new System.Drawing.Size(728, 255);
			this.lstNodes.SmallImageList = this.imageList2;
			this.lstNodes.TabIndex = 4;
			this.lstNodes.UseCompatibleStateImageBehavior = false;
			this.lstNodes.View = System.Windows.Forms.View.Details;
			this.lstNodes.DoubleClick += new System.EventHandler(this.lstNodes_DoubleClick);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Host";
			this.columnHeader5.Width = 428;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "CPU";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader6.Width = 62;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Available MBytes";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader7.Width = 116;
			// 
			// lstFolders
			// 
			this.lstFolders.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstFolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
			this.lstFolders.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstFolders.FullRowSelect = true;
			this.lstFolders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstFolders.HideSelection = false;
			this.lstFolders.Location = new System.Drawing.Point(4, 16);
			this.lstFolders.MultiSelect = false;
			this.lstFolders.Name = "lstFolders";
			this.lstFolders.ShowGroups = false;
			this.lstFolders.Size = new System.Drawing.Size(396, 255);
			this.lstFolders.SmallImageList = this.imageList2;
			this.lstFolders.TabIndex = 2;
			this.lstFolders.UseCompatibleStateImageBehavior = false;
			this.lstFolders.View = System.Windows.Forms.View.Details;
			this.lstFolders.SelectedIndexChanged += new System.EventHandler(this.lstFolders_SelectedIndexChanged);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "TestCase";
			this.columnHeader4.Width = 198;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.chart1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(4, 382);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1132, 326);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Report";
			// 
			// chart1
			// 
			this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
			this.chart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
			this.chart1.BorderlineColor = System.Drawing.Color.RoyalBlue;
			chartArea3.Area3DStyle.Inclination = 40;
			chartArea3.Area3DStyle.IsClustered = true;
			chartArea3.Area3DStyle.IsRightAngleAxes = false;
			chartArea3.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
			chartArea3.Area3DStyle.Perspective = 9;
			chartArea3.Area3DStyle.Rotation = 25;
			chartArea3.Area3DStyle.WallWidth = 3;
			chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea3.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea3.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea3.BackColor = System.Drawing.Color.White;
			chartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
			chartArea3.BackSecondaryColor = System.Drawing.Color.White;
			chartArea3.BorderColor = System.Drawing.Color.White;
			chartArea3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea3.CursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
			chartArea3.CursorX.SelectionColor = System.Drawing.Color.White;
			chartArea3.Name = "Default";
			chartArea3.ShadowColor = System.Drawing.Color.Transparent;
			this.chart1.ChartAreas.Add(chartArea3);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
			legend3.BackColor = System.Drawing.Color.Transparent;
			legend3.Enabled = false;
			legend3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			legend3.IsTextAutoFit = false;
			legend3.Name = "Default";
			this.chart1.Legends.Add(legend3);
			this.chart1.Location = new System.Drawing.Point(3, 27);
			this.chart1.Name = "chart1";
			series5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
			series5.BorderWidth = 2;
			series5.ChartArea = "Default";
			series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			series5.Legend = "Default";
			series5.MarkerSize = 8;
			series5.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
			series5.Name = "Success";
			series5.ShadowColor = System.Drawing.Color.Black;
			series5.ShadowOffset = 2;
			series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			series6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
			series6.BorderWidth = 2;
			series6.ChartArea = "Default";
			series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
			series6.Color = System.Drawing.Color.Red;
			series6.Legend = "Default";
			series6.MarkerSize = 9;
			series6.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
			series6.Name = "Error";
			series6.ShadowColor = System.Drawing.Color.Black;
			series6.ShadowOffset = 2;
			series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			series6.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			this.chart1.Series.Add(series5);
			this.chart1.Series.Add(series6);
			this.chart1.Size = new System.Drawing.Size(1126, 296);
			this.chart1.TabIndex = 2;
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FrmStep1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1140, 712);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.toolStrip1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmStep1";
			this.Padding = new System.Windows.Forms.Padding(4, 70, 4, 4);
			this.Sizable = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Testing";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStep1_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStep1_FormClosed);
			this.Load += new System.EventHandler(this.FrmStep1_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton cmdSync;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox txtUsers;
		private System.Windows.Forms.ToolStripButton cmdTest;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView lstNodes;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ListView lstFolders;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolStripButton cmdStop;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton cmdReport;
	}
}