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
	public partial class FrmInput : MaterialForm
	{
		
		public FrmInput()
		{
			InitializeComponent();
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}
		private readonly MaterialSkinManager materialSkinManager;
		private void FrmInput_Load(object sender, EventArgs e)
		{

		}

		public string Value
		{
			get
			{
				return textBox1.Text;
			}
			set { textBox1.Text = value; }
		}

		private void cmdConfig_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBox1.Text))
			{
				MessageBox.Show(this, "请输入有效的值");
				return;
			}
			this.DialogResult = DialogResult.OK;
		}
	}
}
