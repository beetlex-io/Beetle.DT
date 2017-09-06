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
	public partial class FrmMain : MaterialForm
	{

		private readonly MaterialSkinManager materialSkinManager;
		public FrmMain()
		{
			InitializeComponent();
			// Initialize MaterialSkinManager
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}


		private ManagerClient mClient = new ManagerClient();

		private Dictionary<string, Action<NetWorkResult>> mResultHandler = new Dictionary<string, Action<NetWorkResult>>();

		private FrmStep1 mFrmStep = null;

		private void Form1_Load(object sender, EventArgs e)
		{
			Codes.CenterSetting setting = Codes.ManagerSetting.GetCenterSetting();
			txtAddress.Text = setting.Host;
			txtPort.Text = setting.Port.ToString();

			mClient.MessageRoute.Register(this);
		}


		[Handler]
		[FrmInvokeFilter]
		public void OnList(ListFolderResponse e)
		{
			lstFolders.Items.Clear();
			foreach (string item in e.Items)
			{

				lstFolders.Items.Add(item, 0);
			}
		}
		[Handler]
		[FrmInvokeFilter]
		public void OnListNode(ListNodeResponse e)
		{
			if (e.Nodes != null)
			{

				BindNodeListView(e.Nodes, lstNodes);
				if (mFrmStep != null)
					mFrmStep.UpdateNodes(e.Nodes);
			}
			else
			{
				lstNodes.Items.Clear();
			}
		}

		public static void BindNodeListView(IList<NodeInfo> nodes, ListView view)
		{
			foreach (NodeInfo info in nodes)
			{
				NodeListViewItem item = view.Items.Cast<NodeListViewItem>().FirstOrDefault(o => o.Info.Name == info.Name);
				if (item != null)
				{
					item.SetInfo(info);
				}
				else
				{
					item = new NodeListViewItem(info);
					view.Items.Add(item);
				}
			}
			var removeitems = view.Items.Cast<NodeListViewItem>().Where(d => !nodes.Contains(d.Info)).Select(d => d);
			foreach (NodeListViewItem item in removeitems)
			{
				view.Items.Remove(item);
			}
		}

		public class NodeListViewItem : ListViewItem
		{
			public NodeListViewItem(NodeInfo info) : base(new string[] { string.Format("{0}({1})", info.Name, info.EndPoint), info.PerformanceInfo.CpuUsage, info.PerformanceInfo.AvailableMemory })
			{
				ImageIndex = 0;
				Info = info;
			}



			public NodeInfo Info
			{
				get;
				private set;
			}
			public void SetInfo(NodeInfo info)
			{
				Info = info;
				SubItems[1].Text = info.PerformanceInfo.CpuUsage;
				SubItems[2].Text = info.PerformanceInfo.AvailableMemory;
			}
			public override bool Equals(object obj)
			{
				if (obj is string)
				{
					return Info.Name == obj.ToString();
				}
				else if (obj is NodeInfo)
				{
					return ((NodeInfo)obj).Name == Info.Name;
				}
				return base.Equals(obj);
			}
		}


		[Handler]
		[FrmInvokeFilter]
		public void OnListFiles(ListFilesResponse e)
		{
			lstFiles.Items.Clear();
			cmdDeleteFiles.Enabled = false;
			if (e.Files != null)
			{
				foreach (FileInfo file in e.Files)
				{
					lstFiles.Items.Add(new ListViewItem(new string[] { file.Name, file.UpdateTime, file.Version }, 1));
				}
			}
		}

		[Handler]
		[FrmInvokeFilter]
		public void OnResult(NetWorkResult result)
		{
			Action<NetWorkResult> action = null;
			if (mResultHandler.TryGetValue(result.ID, out action))
			{
				mResultHandler.Remove(result.ID);
				action(result);
			}
			else
			{
				if (!result.Success)
				{
					txtStatus.Text = result.Message;
				}
			}
		}
		public class FrmInvokeFilter : FilterAttribute
		{
			public override void Execute(MethodContext context)
			{
				Form frm = (Form)context.Controller;
				frm.Invoke(new Action(() =>
				{
					try
					{

						base.Execute(context);
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message);
					}
				}));
			}
		}

		private void cmdConnect_Click(object sender, EventArgs e)
		{
			Codes.CenterSetting setting = new Codes.CenterSetting();
			setting.Host = txtAddress.Text;
			setting.Port = int.Parse(txtPort.Text);
			Codes.ManagerSetting.SaveCenterSetting(setting);
			mClient.Connect(txtAddress.Text, int.Parse(txtPort.Text));
			cmdNew.Enabled = cmdDisconnect.Enabled = !(cmdConnect.Enabled = !mClient.NetClient.Connected);
			if (!mClient.NetClient.Connected)
			{
				txtStatus.Text = mClient.NetClient.LastError.Message;
			}
			else
			{
				mClient.List();
			}

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (mClient.NetClient != null)
			{
				//mClient.Ping();
				cmdNew.Enabled = cmdDisconnect.Enabled = !(cmdConnect.Enabled = !mClient.NetClient.Connected);
				if (mClient.NetClient.Connected)
				{

					mClient.ListNode();
				}
			}
		}

		private void cmdNew_Click(object sender, EventArgs e)
		{
			FrmInput input = new DTManager.FrmInput();

			if (input.ShowDialog(this) == DialogResult.OK)
			{
				CreateTest ct = mClient.Create(input.Value);
				mResultHandler[ct.ID] = (d) => { txtStatus.Text = "create folder success!"; mClient.List(); };
			}
		}

		private void cmdDisconnect_Click(object sender, EventArgs e)
		{
			mClient.Close();
		}

		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
			if (GetSelectItem() != null)
			{
				System.Array items = ((System.Array)e.Data.GetData(DataFormats.FileDrop));
				foreach (string item in items)
				{
					var result = mClient.UpdateFile(GetSelectItem().Text, item);
					mResultHandler[result.ID] = (r) =>
					{
						mClient.ListFiles(GetSelectItem().Text);
					};
				}

			}
		}

		private void listView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void lstFolders_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Whether you want to delete the directory?", "warn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				mClient.Delete(GetSelectItem().Text);

				mClient.List();
			}
		}

		private void cmdUpload_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				foreach (string item in openFileDialog1.FileNames)
				{
					var result = mClient.UpdateFile(GetSelectItem().Text, item);
					mResultHandler[result.ID] = (r) =>
					{
						mClient.ListFiles(GetSelectItem().Text);
					};
				}
			}
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{


		}


		public ListViewItem GetSelectItem()
		{
			if (lstFolders.SelectedItems.Count > 0)
				return lstFolders.SelectedItems[0];
			return null;
		}

		private void lstFolders_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (GetSelectItem() != null)
			{
				mClient.ListFiles(GetSelectItem().Text);
				lstFiles.Items.Clear();
			}
			cmdRun.Enabled = cmdUpload.Enabled = cmdDelete.Enabled = GetSelectItem() != null;
		}


		private List<NodeInfo> GetNodes()
		{
			List<NodeInfo> result = new List<NodeInfo>();
			foreach (NodeListViewItem item in lstNodes.Items)
			{
				if (item.Checked)
				{
					result.Add(item.Info);
					if (item.Info.Report == null)
						item.Info.Report = new NodeInfo.NodeReport();
				}
			}
			return result;
		}


		public ListViewItem GetSelectFileItem()
		{
			if (lstFiles.SelectedItems.Count > 0)
				return lstFiles.SelectedItems[0];
			return null;
		}
		private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdDeleteFiles.Enabled = GetSelectFileItem() != null;
		}

		private void cmdDeleteFiles_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Whether you want to delete the selection file?", "warn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				List<string> files = new List<string>();
				foreach (ListViewItem item in lstFiles.SelectedItems)
				{
					files.Add(item.Text);
				}
				DeleteFiles del = mClient.DeleteFiles(GetSelectItem().Text, files);
				mResultHandler[del.ID] = (r) =>
				{
					mClient.ListFiles(GetSelectItem().Text);
				};
			}
		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			List<NodeInfo> nodes = GetNodes();
			if (nodes.Count == 0)
			{
				MessageBox.Show(this, "Please select the service node for the stress test!");
				return;
			}
			mFrmStep = new FrmStep1();
			mClient.MessageRoute.Register(mFrmStep);
			mFrmStep.UnitTest = GetSelectItem().Text;
			mFrmStep.Text = mFrmStep.UnitTest;
			mFrmStep.Client = mClient;
			mFrmStep.Nodes = nodes;
			mFrmStep.ShowDialog(this);

		}

		private void cmdAbout_Click(object sender, EventArgs e)
		{
			FrmAbout about = new DTManager.FrmAbout();
			about.ShowDialog(this);
		}
	}
}

