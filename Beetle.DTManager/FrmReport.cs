using Beetle.DTCore.Center;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Beetle.DTManager
{
	public partial class FrmReport : MaterialForm
	{
		public FrmReport()
		{
			InitializeComponent();
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
		}
		private readonly MaterialSkinManager materialSkinManager;

		public IList<NodeInfo> Nodes { get; set; }

		private void FrmReport_Load(object sender, EventArgs e)
		{
			ChangeChartStyle(chatTestSummary, chartNodeSuccessDistribution, chartResponseTime);
			BindTestSummary();
			BindResponseTime();
			BindNodeSuccessDistribution();
		}

		private void ChangeChartStyle(params Chart[] items)
		{
			foreach (Chart chart in items)
			{
				chart.ChartAreas["Area1"].Area3DStyle.Enable3D = true;

			}
		}

		private void BindTestSummary()
		{
			var serises = chatTestSummary.Series["Series1"];
			serises.Points.Clear();
			long success = 0, errors = 0;
			foreach (NodeInfo node in Nodes)
			{
				success += node.Report.GetSuccess();
				errors += node.Report.GetErrors();
			}
			var successpoint = new DataPoint();
			successpoint.LegendText = "Success(#VAL{N0})";
			successpoint.YValues = new double[] { success };

			serises.Points.Add(successpoint);
			var errorpoint = new DataPoint();
			errorpoint.LegendText = "Error(#VAL{N0})";
			errorpoint.YValues = new double[] { errors };

			serises.Points.Add(errorpoint);
		}


		private void BindNodeSuccessDistribution()
		{
			var serises = chartNodeSuccessDistribution.Series["Series1"];
			serises.Points.Clear();
			foreach (NodeInfo node in Nodes)
			{
				var point = new DataPoint();
				point.LegendText = node.EndPoint + "(#VAL{N0})  " + node.Name;
				point.YValues = new double[] { node.Report.GetSuccess() };

				serises.Points.Add(point);
			}
		}

		private void BindResponseTime()
		{
			var serises = chartResponseTime.Series["Series1"];
			serises.Points.Clear();
			ResponseTimeRegion[] regions = new ResponseTimeRegion[] {
				new ResponseTimeRegion(0,20),
				new ResponseTimeRegion(20,50),
				new ResponseTimeRegion(50,100),
				new ResponseTimeRegion(100,200),
				new ResponseTimeRegion(200,500),
				new ResponseTimeRegion(500,1000),
				new ResponseTimeRegion(1000,5000),
				new ResponseTimeRegion(5000,-1)

			};
			foreach (NodeInfo node in Nodes)
			{
				foreach (long item in node.Report.DelayTimes)
				{
					ResponseTimeRegion region = regions.FirstOrDefault(r => r.Equals(item));
					if (region != null)
						region.Count++;
				}
			}
			foreach (ResponseTimeRegion region in regions)
			{
				serises.Points.Add(region.CreatePoint());
			}
		}

		public class ResponseTimeRegion
		{

			public ResponseTimeRegion(long start, long end)
			{
				Start = start;
				End = end;
				if (end != -1)
				{
					Name = string.Format("{0}-{1}", start, end);
				}
				else
					Name = string.Format(">{0}", start);
			}

			public long Start { get; set; }

			public long End { get; set; }

			public string Name { get; set; }

			public int Count { get; set; }

			public DataPoint CreatePoint()
			{
				var result = new DataPoint();
				result.LegendText = Name + "(#VAL{N0})";
				result.YValues = new double[] { Count };

				return result;
			}

			public override bool Equals(object obj)
			{
				if (obj is long)
				{
					long value = (long)obj;
					if (End == -1)
					{
						return value >= Start;
					}
					else
					{
						return value >= Start && value < End;
					}
				}
				return base.Equals(obj);
			}
		}


	}
}
