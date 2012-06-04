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

namespace Roster.Forms
{
    public partial class StudentEnrollments : DockContent
    {
        public StudentEnrollments()
        {
            InitializeComponent();
            txtFees.ReadOnly = true;
            txtGrade.ReadOnly = true;
            ddlTag.Enabled = false;
            UpdateSessions();
            UpdateStudents();
            UpdateTags();
        }

        private void UpdateSessions()
        {
            string query = "SELECT SessionID, Name FROM Sessions;";
            DataSet ds = SqlHelper.GetDataSet(query);
            cmbSession.DataSource = ds.Tables[0];
            cmbSession.DisplayMember = "Name";
            cmbSession.ValueMember = "SessionID";
        }

        private void UpdateTags()
        {
            string query = "SELECT Tag FROM EnrollmentTags;";
            DataSet ds = SqlHelper.GetDataSet(query);
            ddlTag.DataSource = ds.Tables[0];
            ddlTag.DisplayMember = "Tag";
            ddlTag.ValueMember = "Tag";
        }

        private void Enrollments_Enter(object sender, EventArgs e)
        {
            //UpdateDropDowns();
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
            string query = "SELECT ClassID, Course.Title FROM Classes LEFT OUTER JOIN Course ON Course.CourseID = Classes.CourseID WHERE SessionID = @SessionID;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("SessionID", _Session);
            DataSet ds = SqlHelper.GetDataSet(query, p);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbClass.DataSource = ds.Tables[0];
                cmbClass.DisplayMember = "Title";
                cmbClass.ValueMember = "ClassID";
            }
            else
            {
                cmbClass.DataSource = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
(SELECT cr.MaxStudents FROM Classes c JOIN ClassRooms cr ON c.ClassRoomID = cr.ClassRoomID WHERE c.ClassID = @ClassID) -
(SELECT COUNT(*) FROM Enrollments WHERE ClassID = @ClassID)";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("@ClassID", cmbClass.SelectedValue);
            int seatsLeft = Convert.ToInt32(SqlHelper.GetScalar(query, p));
            if (seatsLeft < 1)
            {
                MessageBox.Show("The ClassRoom is full for this class, you must either add more seats or change the room");
                return;
            }
            query = @"INSERT INTO Enrollments (StudentID, ClassID, Tag) VALUES (@StudentID, @ClassID, @Tag);";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@StudentID", cmbStudent.SelectedValue);
            parameters.Add("@ClassID", cmbClass.SelectedValue);
            parameters.Add("@Tag", ddlTag.SelectedValue);
            try
            {
                SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (SQLiteException ex) { MessageBox.Show(ex.Message); }
            UpdateStudentClasses();
        }

        private void UpdateStudents()
        {
            string query = "SELECT StudentID, LastName || ', ' || FirstName AS Name FROM Students;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = SqlHelper.GetDataSet(query, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbStudent.DataSource = ds.Tables[0];
                cmbStudent.DisplayMember = "Name";
                cmbStudent.ValueMember = "StudentID";
            }
            else
                cmbStudent.DataSource = null;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 ID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassID"]);
                SqlHelper.Parameters.Add("@StudentID", cmbStudent.SelectedValue);
                SqlHelper.Parameters.Add("@ClassID", ID);
                SqlHelper.ExecteNonQuery("DELETE FROM Enrollments WHERE ClassID = @ClassID AND StudentID = @StudentID;");
            }
            UpdateStudentClasses();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtFees.Text = txtGrade.Text = string.Empty;
                txtFees.ReadOnly = txtGrade.ReadOnly = dataGridView1.SelectedRows.Count != 1;
                ddlTag.Enabled = !txtFees.ReadOnly;
                if (!txtFees.ReadOnly)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    if (((DataRowView)row.DataBoundItem).Row["Fees"] != null)
                        txtFees.Text = ((DataRowView)row.DataBoundItem).Row["Fees"].ToString();
                    if (((DataRowView)row.DataBoundItem).Row["Grade"] != null)
                        txtGrade.Text = ((DataRowView)row.DataBoundItem).Row["Grade"].ToString();
                    if (((DataRowView)row.DataBoundItem).Row["Tag"] != null)
                        ddlTag.SelectedValue = ((DataRowView)row.DataBoundItem).Row["Tag"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                return;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            Int64 ID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassID"]);
            SqlHelper.Parameters.Add("@StudentID", cmbStudent.SelectedValue);
            SqlHelper.Parameters.Add("@ClassID", ID);
            SqlHelper.Parameters.Add("@Fees", txtFees.Text);
            SqlHelper.Parameters.Add("@Grade", txtGrade.Text);
            SqlHelper.Parameters.Add("@Tag", ddlTag.SelectedValue);
            SqlHelper.ExecteNonQuery("UPDATE Enrollments SET FeesPaid = @Fees, Tag = @Tag, Grade = @Grade WHERE ClassID = @ClassID AND StudentID = @StudentID;");
            UpdateStudentClasses();
        }

        private void cmbStudent_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateStudentClasses();
        }

        private void UpdateStudentClasses()
        {
            string query = @"SELECT e.ClassID, cs.Title, i.LastName || ',' || i.FirstName as Instructor, s.Name AS Session, p.Name AS Period, e.FeesPaid AS Fees, e.Tag, e.Grade
FROM Enrollments e
LEFT OUTER JOIN Classes c ON e.ClassID = c.ClassID
LEFT OUTER JOIN Sessions s ON c.SessionID = s.SessionID
LEFT OUTER JOIN Course cs ON c.CourseID = cs.CourseID
LEFT OUTER JOIN Periods p ON c.PeriodID = p.PeriodID
LEFT OUTER JOIN Instructors i on c.InstructorID = i.InstructorID
WHERE StudentID = @StudentID 
ORDER BY Session DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@StudentID", cmbStudent.SelectedValue);
            DataSet ds = SqlHelper.GetDataSet(query, parameters);
            if (ds.Tables[0].Rows.Count > 0)
                dataGridView1.DataSource = ds.Tables[0];
            else
                dataGridView1.DataSource = null;
        }
    }
}
