using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using Martinez_CrystalReport.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Martinez_CrystalReport
{
	public partial class Form1 : Form
	{
		private readonly CompanyXDataContext _context;
		private readonly ReportDocument _report;
		private static string EXCESS_STRING = ConfigurationManager.AppSettings["Excess"];
		private static string REPORT_PATH = ConfigurationManager.AppSettings["Report"];

		public Form1(CompanyXDataContext context, ReportDocument report)
		{
			InitializeComponent();
			_context = context;
			_report = report;
		}

		private List<GetEmployeeDetailsResult> GetAllRecord() => _context.GetEmployeeDetails().ToList();

		private string GetReportPath() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(EXCESS_STRING, ""), REPORT_PATH);

		private CrystalReportViewer LoadReport(ReportDocument document)
		{
			crystalReportViewer1.ReportSource = document;
			crystalReportViewer1.Refresh();
			return crystalReportViewer1;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			_report.Load(GetReportPath());
			_report.SetDataSource(GetAllRecord());
			LoadReport(_report);
		}
	}
}
