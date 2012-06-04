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
    public partial class Rolls : DockContent
    {
        public Rolls()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Rolls_NewSearchTerm);
            UpdateSessions();
        }

        void Rolls_NewSearchTerm(object sender, EventArgs e)
        {
            if(cmbSession.SelectedValue.ToString().Length > 0)
                UpdateClasses(cmbSession.SelectedValue.ToString());
        }

        private void UpdateSessions()
        {
            string query = "SELECT SessionID, Name FROM Sessions;";
            DataSet ds = SqlHelper.GetDataSet(query);
            cmbSession.DataSource = ds.Tables[0];
            cmbSession.DisplayMember = "Name";
            cmbSession.ValueMember = "SessionID";
        }

        private void cmbSession_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateClasses(cmbSession.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateClasses(string _Session)
        {
            string query = @"SELECT ClassID, Course.Title, Instructors.LastName AS Instructor, Periods.Name AS Period, Schools.Name As School, ClassRooms.RoomNumber AS RoomNumber FROM Classes 
LEFT OUTER JOIN Course ON Course.CourseID = Classes.CourseID
LEFT OUTER JOIN Instructors ON Classes.InstructorID = Instructors.InstructorID
LEFT OUTER JOIN Periods ON Classes.PeriodID = Periods.PeriodID
LEFT OUTER JOIN ClassRooms ON Classes.ClassRoomID = ClassRooms.ClassRoomID
LEFT OUTER JOIN Schools ON ClassRooms.SchoolID = Schools.SchoolID WHERE SessionID = @SessionID " + GetSearchTerms(dataGridView1);
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("SessionID", _Session);
            p.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(query, p);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string query = @"SELECT ClassID, 
Course.Title, Course.Description,
Instructors.LastName, Instructors.FirstName, 
Periods.Name AS Period, Periods.StartTime, Periods.EndTime,
ClassRooms.RoomNumber, ClassRooms.MaxStudents, ClassRooms.Notes,
Schools.Name As School,
Sessions.Name AS Session, Sessions.StartDate AS StartDate, Sessions.EndDate
FROM Classes 
LEFT OUTER JOIN Course ON Course.CourseID = Classes.CourseID
LEFT OUTER JOIN Instructors ON Classes.InstructorID = Instructors.InstructorID
LEFT OUTER JOIN Periods ON Classes.PeriodID = Periods.PeriodID
LEFT OUTER JOIN ClassRooms ON Classes.ClassRoomID = ClassRooms.ClassRoomID
LEFT OUTER JOIN Schools ON ClassRooms.SchoolID = Schools.SchoolID
LEFT OUTER JOIN Sessions ON Classes.SessionID = Sessions.SessionID
WHERE ClassID = @ClassID;";
            FileInfo template = new FileInfo("RollTemplate.pyhtm");
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
    		sb.Append("<title>Class Roll</title>");
        	sb.Append("</head>");
        	sb.Append("<body>");
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                PyTemplate py = new PyTemplate();
                SqlHelper.Parameters.Clear();
                Int64 ID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassID"]);
                SqlHelper.Parameters.Add("@ClassID", ID);
                DataSet dsClassInfo = SqlHelper.GetDataSet(query);
                py.AddGlobal("ClassInfo", dsClassInfo.Tables[0]);
                string enrollmentQuery = @"SELECT LastName, FirstName, Schools.Name AS School FROM Enrollments
JOIN Students ON Enrollments.StudentID = Students.StudentID
JOIN Schools ON Students.SchoolID = Schools.SchoolID 
WHERE ClassID = @ClassID 
ORDER BY LastName ASC";
                SqlHelper.Parameters.Clear();
                SqlHelper.Parameters.Add("@ClassID", ID);
                DataSet dsRoll = SqlHelper.GetDataSet(enrollmentQuery);
                py.AddGlobal("RollInfo", dsRoll.Tables[0]);
                py.Parse(strTemplate);
                sb.AppendLine(py.Render());
                count++;
                if(count < dataGridView1.SelectedRows.Count)
                    sb.AppendLine("<br style=\"page-break-after: always;\"/>");
            }
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
