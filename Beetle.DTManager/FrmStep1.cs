using Beetle.DTCore.Center;
using Beetle.DTCore.Manager;
using Beetle.DTCore.Network;
using Beetle.MR;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beetle.DTManager
{
	public partial class FrmStep1 : MaterialForm
	{
		public FrmStep1()
		{

			InitializeComponent();
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}
		private readonly MaterialSkinManager materialSkinManager;

		private long mSuccess;

		private long mErrors;

		private string mLastTest = "";

		private void FrmStep1_Load(object sender, EventArgs e)
		{
			Client.ListTestCase(UnitTest);
			FrmMain.BindNodeListView(Nodes, lstNodes);
		}


		public ManagerClient Client { get; set; }

		public string UnitTest { get; set; }

		public IList<NodeInfo> Nodes { get; set; }

		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void ListTestCase(ListTestCaseResponse e)
		{
			foreach (string item in e.Cases)
			{
				lstFolders.Items.Add(item);
			}
		}

		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void GetUnitTestReportReponse(GetUnitTestReportReponse e)
		{
			long success = 0, errors = 0;

			foreach (StatisticalReport item in e.Items)
			{
				NodeInfo node = this.Nodes.FirstOrDefault(n => n.Name == item.Node);
				if (node != null)
					node.Report.Statistical.Add(item);
				success += item.Info.Success.Value;
				errors += item.Info.Error.Value;
			}
			chart1.Series["Success"].Points.AddY(success - mSuccess);
			mSuccess = success;
			chart1.Series["Error"].Points.AddY(errors - mErrors);
			mErrors = errors;

		}
		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void OnReportDelayTime(ReportDelayTimes e)
		{
			NodeInfo node = this.Nodes.FirstOrDefault(n => n.Name == e.Node);
			if (node != null)
				node.Report.DelayTimes.AddRange(e.Times);
		}

		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void OnReportError(ReportErrors e)
		{
			NodeInfo node = this.Nodes.FirstOrDefault(n => n.Name == e.Node);
			if (node != null)
				node.Report.Errors.AddRange(e.Errors);

			foreach (FrmMain.NodeListViewItem item in this.lstNodes.Items)
			{
				if (item.Info.Report.Errors.Count > 0)
					item.ImageIndex = 1;
				else
					item.ImageIndex = 0;
			}
		}


		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void OnRunTest(RunTestcaseResponse e)
		{
			cmdReport.Enabled = lstFolders.Enabled = cmdSync.Enabled = cmdTest.Enabled = !(timer1.Enabled = cmdStop.Enabled = true);

		}

		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void OnStopTest(StopTestCaseResponse e)
		{
			cmdReport.Enabled = lstFolders.Enabled = cmdSync.Enabled = cmdTest.Enabled = !(timer1.Enabled = cmdStop.Enabled = false);
		}


		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void GetTestConfigCode(GetTestConfigCodeResponse e)
		{
			if (!string.IsNullOrEmpty(e.Code))
			{
				DTCore.Domains.FileCompiler Compiler = new DTCore.Domains.FileCompiler();
				System.Reflection.Assembly assembly = Compiler.CreateAsssemblyFromCodes(true, e.Code);
				Type type = assembly.GetType(e.ClassName);

				ListViewItem item = GetSelectItem();
				Codes.CaseSetting setting = Codes.ManagerSetting.GetCaseSetting(this.UnitTest, item.Text);
				txtUsers.Text = setting.Users.ToString();
				if (!string.IsNullOrEmpty(setting.Config))
				{
					propertyGrid1.SelectedObject = Newtonsoft.Json.JsonConvert.DeserializeObject(setting.Config, type);
				}
				else
				{
					propertyGrid1.SelectedObject = Activator.CreateInstance(type);
				}
			}
		}


		[Handler]
		[FrmMain.FrmInvokeFilter]
		public void OnSyncUnitResponse(SyncUnitTestResponse e)
		{
			foreach (VerifyFilesResponse item in e.NodeVerifyFiles)
			{
				if (!item.Status)
				{
					MessageBox.Show(string.Format("{0} node synchronization failed!", item.Node));

					return;
				}

			}
			cmdTest.Enabled = true;
		}

		public void UpdateNodes(IList<NodeInfo> nodes)
		{
			foreach (NodeInfo node in nodes)
			{
				NodeInfo match = this.Nodes.FirstOrDefault(d => d.Name == node.Name);
				if (match != null)
					match.PerformanceInfo = node.PerformanceInfo;
			}
			FrmMain.BindNodeListView(this.Nodes, lstNodes);

		}

		private void lstFolders_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListViewItem item = GetSelectItem();
			if (item != null)
			{
				Client.GetTestConfigCode(UnitTest, item.Text);
			}
		}
		public ListViewItem GetSelectItem()
		{
			if (lstFolders.SelectedItems.Count > 0)
				return lstFolders.SelectedItems[0];
			return null;
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}

		private void cmdRun_Click(object sender, EventArgs e)
		{

		}

		private void cmdSync_Click_1(object sender, EventArgs e)
		{
			Client.SyncUnitTest(this.UnitTest,
				Nodes.Select(d => d.Name).ToArray());
			cmdTest.Enabled = false;
		}

		private void cmdTest_Click(object sender, EventArgs e)
		{
			ListViewItem item = GetSelectItem();
			if (item != null)
			{
				mLastTest = item.Text;
				mSuccess = 0;
				mErrors = 0;
				chart1.Series["Success"].Points.Clear();

				chart1.Series["Error"].Points.Clear();

				foreach (NodeInfo node in Nodes)
				{
					node.Report.Reset();
				}

				Codes.CaseSetting setting = new Codes.CaseSetting();
				setting.Users = int.Parse(txtUsers.Text);
				if (propertyGrid1.SelectedObject != null)
				{
					setting.Config = Newtonsoft.Json.JsonConvert.SerializeObject(propertyGrid1.SelectedObject);
				}
				Codes.ManagerSetting.SaveCaseSetting(this.UnitTest, item.Text, setting);
				Client.RunTest(this.UnitTest, item.Text, int.Parse(txtUsers.Text), propertyGrid1.SelectedObject,
					Nodes.Select(d => d.Name).ToArray());
			}
			else
			{
				MessageBox.Show(this, "Please select a test case!");
			}
		}

		private void cmdStop_Click(object sender, EventArgs e)
		{
			Client.StopTest(this.UnitTest, Nodes.Select(d => d.Name).ToArray());
		}

		private void FrmStep1_FormClosed(object sender, FormClosedEventArgs e)
		{

		}

		private void FrmStep1_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (NodeInfo node in Nodes)
			{
				node.Report.Reset();
			}
			ListViewItem item = GetSelectItem();
			if (item != null)
				Client.StopTest(this.UnitTest, Nodes.Select(d => d.Name).ToArray());
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Client.UnitTestReport(this.UnitTest, Nodes.Select(d => d.Name).ToArray());
		}

		private void cmdReport_Click(object sender, EventArgs e)
		{
			FrmReport report = new DTManager.FrmReport();
			report.Nodes = this.Nodes;
			report.Text = mLastTest + " report";
			report.ShowDialog(this);
		}

		private void lstNodes_DoubleClick(object sender, EventArgs e)
		{
			if (lstNodes.SelectedItems != null && lstNodes.SelectedItems.Count > 0)
			{
				FrmMain.NodeListViewItem item = (FrmMain.NodeListViewItem)lstNodes.SelectedItems[0];
				FrmErrors frmError = new DTManager.FrmErrors();
				frmError.Errors = item.Info.Report.Errors;
				frmError.Show(this);
			}
		}
	}
}
