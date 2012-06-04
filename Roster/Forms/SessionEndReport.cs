using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SQLite;
using System.IO;

namespace Roster.Forms
{
    public partial class SessionEndReport : DockContent
    {
        public SessionEndReport()
        {
            InitializeComponent();
            UpdateSessions();
        }

        private void UpdateSessions()
        {
            string query = "SELECT SessionID, Name FROM Sessions;";
            DataSet ds = SqlHelper.GetDataSet(query);
            cmbSession.DataSource = ds.Tables[0];
            cmbSession.DisplayMember = "Name";
            cmbSession.ValueMember = "SessionID";
        }

        private void btnPrintRpt_Click(object sender, EventArgs e)
        {
            string query = "SELECT Tag, FeesPaid, Count(*) AS \"Count\" FROM Enrollments WHERE ClassID IN (SELECT ClassID FROM Classes WHERE SessionID = @SessionID) GROUP BY Tag, FeesPaid";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("@SessionID", cmbSession.SelectedValue);
            DataSet tagBreakDown = SqlHelper.GetDataSet(query, p);

            query = "SELECT SUM(FeesPaid) FROM Enrollments WHERE ClassID IN (SELECT ClassID FROM Classes WHERE SessionID = @SessionID)";
            double sessionTotal = Convert.ToDouble(SqlHelper.GetScalar(query, p));

            FileInfo template = new FileInfo("SessionEndReportTemplate.pyhtm");
            if (!template.Exists)
            {
                MessageBox.Show("Cannot find template file");
                return;
            }
            StreamReader sr = template.OpenText();
            string strTemplate = sr.ReadToEnd();
            sr.Close();
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<title>Session End Report</title>");
            sb.Append("</head>");
            sb.Append("<body>");
            PyTemplate py = new PyTemplate();
            py.AddGlobal("TagBreakDown", tagBreakDown.Tables[0]);
            py.AddGlobal("SessionTotal", sessionTotal);
            query = "SELECT Name FROM Sessions WHERE SessionID = @SessionID";
            string SessionName = SqlHelper.GetScalar(query, p).ToString();
            py.AddGlobal("Session", SessionName);
            py.Parse(strTemplate);
            sb.AppendLine(py.Render());
            sb.Append("</body>");
            sb.Append("</html>");
            webBrowser1.DocumentText = sb.ToString();
            do
            {
                Application.DoEvents();
            } while (webBrowser1.ReadyState != WebBrowserReadyState.Complete);
            
            webBrowser1.ShowPrintPreviewDialog();
        }
    }
}
