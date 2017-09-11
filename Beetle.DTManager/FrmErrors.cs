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
	public partial class FrmErrors : MaterialForm
	{
		public FrmErrors()
		{
			InitializeComponent();
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}
		private readonly MaterialSkinManager materialSkinManager;


		public IList<Beetle.DTCore.Network.ErrorInfo> Errors { get; set; }

		private void FrmErrors_Load(object sender, EventArgs e)
		{
			lstErrors.DataSource = this.Errors;
		}

		private void lstErrors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstErrors.SelectedItem != null)
			{
				Beetle.DTCore.Network.ErrorInfo info = (Beetle.DTCore.Network.ErrorInfo)lstErrors.SelectedItem;
				richTextBox1.Text = info.StackTrace;

			}
		}
	}
}
