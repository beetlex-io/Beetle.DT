using Beetle.DTCore;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beetle.DTManager
{
	public partial class FrmProperties : MaterialForm
	{
		public FrmProperties()
		{
			InitializeComponent();
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}

		private readonly MaterialSkinManager materialSkinManager;

		private IList<IBinder> mBinders;

		private void FrmProperties_Load(object sender, EventArgs e)
		{
			try
			{
				mBinders = CreateBinders(this.SelectObject);
				int top = 10;
				foreach (IBinder item in mBinders)
				{
					item.Init();
					item.AddTo(panelControls, top, toolTip1);
					top += item.Height;

				}
				if (top > 400)
				{
					this.panelControls.Height = 400;
					this.cmdCancel.Top = this.cdmComfirn.Top = 500;
					this.Height = 540;
				}
				else
				{
					this.panelControls.Height = top + 40;
					this.cmdCancel.Top = this.cdmComfirn.Top = this.panelControls.Height + 50;
					this.Height = cdmComfirn.Top + 40;
				}
			}
			catch (Exception e_)
			{
				MessageBox.Show(this, e_.Message);
			}
		}

		public object SelectObject { get; set; }

		public interface IBinder
		{
			int Height { get; set; }
			void Binding();
			void AddTo(Control control, int top, ToolTip tip = null);

			void Init();

		}


		private IList<IBinder> CreateBinders(object data)
		{
			List<IBinder> result = new List<DTManager.FrmProperties.IBinder>();
			if (data != null)
			{
				foreach (PropertyInfo p in data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					IBinder binder = null;

					if (p.PropertyType == typeof(string))
					{
						Beetle.DTCore.PropertyAttribute pa = p.GetCustomAttribute<Beetle.DTCore.PropertyAttribute>();
						if (pa != null && pa.Type == DTCore.PropertyType.Remark)
						{
							binder = new TestBuilder(data, p, true);
						}
						else
						{
							binder = new TestBuilder(data, p, false);
						}
					}
					else if (p.PropertyType.IsEnum)
					{
						binder = new EnumBinder(data, p);

					}
					else if (p.PropertyType == typeof(DateTime))
					{
						binder = new DateTimeBinder(data, p);
					}

					else if (p.PropertyType.IsValueType)
					{
						binder = new NumberBinder(data, p);
					}
					if (binder != null)
						result.Add(binder);
				}
			}
			return result;
		}


		public abstract class DataBinderBase<CONTROL> : IBinder where CONTROL : Control, new()
		{
			public DataBinderBase(object target, System.Reflection.PropertyInfo property)
			{
				Target = target;
				Property = property;
				Label = new Label();
				Label.AutoSize = false;
				Label.TextAlign = ContentAlignment.MiddleRight;
				Label.Width = 80;
				Label.Text = property.Name + ":";
				Control = new CONTROL();
				Control.Left = 90;
				mLabelAttribute = property.GetCustomAttribute<PropertyLabelAttribute>();
				Control.Width = 500;
				if (mLabelAttribute != null)
				{
					Label.Text = mLabelAttribute.Name + ":";

				}

				Height = 30;
			}


			private PropertyLabelAttribute mLabelAttribute;

			public int Height { get; set; }

			public virtual void Init()
			{

				SetControlData(this.Property.GetValue(this.Target));
			}

			public Label Label { get; set; }

			public CONTROL Control { get; set; }

			public PropertyInfo Property
			{ get; set; }

			public object Target { get; set; }

			protected abstract void SetControlData(object value);

			protected abstract object GetControlValue();

			public void Binding()
			{
				object value = Convert.ChangeType(GetControlValue(), Property.PropertyType);
				Property.SetValue(this.Target, value);
			}

			public void AddTo(Control control, int top, ToolTip tip = null)
			{
				Label.Top = top;
				this.Control.Top = top;
				control.Controls.Add(this.Label);
				control.Controls.Add(this.Control);
				if (tip != null && mLabelAttribute != null)
				{
					tip.SetToolTip(this.Label, mLabelAttribute.Remark);
					tip.SetToolTip(this.Control, mLabelAttribute.Remark);
				}
			}
		}

		public class EnumBinder : DataBinderBase<ComboBox>
		{
			public EnumBinder(object target, PropertyInfo property) : base(target, property) { }

			public override void Init()
			{
				this.Control.DropDownStyle = ComboBoxStyle.DropDownList;
				Array names = Enum.GetNames(Property.PropertyType);
				foreach (var item in names)
				{
					this.Control.Items.Add(item.ToString());
				}
				base.Init();

			}
			protected override void SetControlData(object value)
			{
				string name = Enum.GetName(Property.PropertyType, value);
				for (int i = 0; i < this.Control.Items.Count; i++)
				{
					if (name == this.Control.Items[i].ToString())
					{
						this.Control.SelectedIndex = i;
						return;
					}
				}
				this.Control.SelectedIndex = 0;
			}
			protected override object GetControlValue()
			{
				string name = this.Control.SelectedItem.ToString();
				return Enum.Parse(this.Property.PropertyType, name);
			}
		}

		public class TestBuilder : DataBinderBase<TextBox>
		{
			public TestBuilder(object target, PropertyInfo property, bool remark) : base(target, property)
			{
				IsRemark = remark;
			}

			public override void Init()
			{
				base.Init();
				if (IsRemark)
				{
					this.Control.Multiline = true;
					this.Control.Height = 120;
					this.Control.ScrollBars = ScrollBars.Vertical;
					this.Height = 130;
				}
			}

			protected override object GetControlValue()
			{
				return this.Control.Text;
			}

			protected override void SetControlData(object value)
			{
				this.Control.Text = (string)value;
			}
			public bool IsRemark { get; set; }
		}

		public class NumberBinder : DataBinderBase<NumericUpDown>
		{

			public NumberBinder(object target, PropertyInfo property) : base(target, property) { }


			public override void Init()
			{
				this.Control.Minimum = int.MinValue;
				this.Control.Maximum = int.MaxValue;
				this.Control.Width = 120;
				base.Init();

			}

			protected override object GetControlValue()
			{
				return (long)this.Control.Value;
			}
			protected override void SetControlData(object value)
			{
				this.Control.Value = (long)Convert.ChangeType(value, typeof(long));
			}
		}

		public class DateTimeBinder : DataBinderBase<DateTimePicker>
		{
			public DateTimeBinder(object target, System.Reflection.PropertyInfo property) : base(target, property) { }

			public override void Init()
			{
				base.Init();
				this.Control.Width = 120;
			}

			protected override object GetControlValue()
			{
				return this.Control.Value;
			}

			protected override void SetControlData(object value)
			{
				if ((DateTime)value == default(DateTime))
				{
					this.Control.Value = DateTime.Now;
				}
				else
				{
					this.Control.Value = (DateTime)value;
				}
			}
		}

		private void cdmComfirn_Click(object sender, EventArgs e)
		{
			if (mBinders != null)
				foreach (IBinder item in mBinders)
				{
					item.Binding();
				}
			DialogResult = DialogResult.Yes;
			Close();
		}
	}
}
