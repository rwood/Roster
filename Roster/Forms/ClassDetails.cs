using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SQLite;

namespace Roster.Forms
{
    
    public partial class ClassDetails : DockContent
    {
        Int64 _ClassID = -1;

        public ClassDetails(Int64 ClassID)
        {
            InitializeComponent();
            UpdateDropDowns();

            UpdateClassData(ClassID);
            this.TabText = "Class Details";
        }

        private void UpdateClassData(Int64 ClassID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ClassID", ClassID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT ClassID, CourseID, InstructorID, ClassRoomID, SessionID, PeriodID 
FROM Classes WHERE ClassID = @ClassID", parameters);
            while (dr.Read())
            {
                _ClassID = Convert.ToInt64(dr["ClassID"]);
                ddlClassRoom.SelectedValue = dr["ClassRoomID"];
                ddlCourse.SelectedValue = dr["CourseID"];
                ddlInstructor.SelectedValue = dr["InstructorID"];
                ddlSession.SelectedValue = dr["SessionID"];
                ddlPeriod.SelectedValue = dr["PeriodID"];
            }
            dr.Close();
        }

        private void UpdateDropDowns()
        {
            DataSet ClassRoom = SqlHelper.GetDataSet("SELECT ClassRoomID, Schools.Name || ' rm ' || RoomNumber AS Name FROM ClassRooms LEFT OUTER JOIN Schools ON ClassRooms.SchoolID = Schools.SchoolID");
            ddlClassRoom.DisplayMember = "Name";
            ddlClassRoom.ValueMember = "ClassRoomID";
            ddlClassRoom.DataSource = ClassRoom.Tables[0];
            DataSet Course = SqlHelper.GetDataSet("SELECT CourseID, Title FROM Course");
            ddlCourse.DisplayMember = "Title";
            ddlCourse.ValueMember = "CourseID";
            ddlCourse.DataSource = Course.Tables[0];
            DataSet Instructor = SqlHelper.GetDataSet("SELECT InstructorID, LastName FROM Instructors");
            ddlInstructor.DisplayMember = "LastName";
            ddlInstructor.ValueMember = "InstructorID";
            ddlInstructor.DataSource = Instructor.Tables[0];
            DataSet Session = SqlHelper.GetDataSet("SELECT SessionID, Name FROM Sessions");
            ddlSession.DisplayMember = "Name";
            ddlSession.ValueMember = "SessionID";
            ddlSession.DataSource = Session.Tables[0];
            DataSet Period = SqlHelper.GetDataSet("SELECT PeriodID, Name FROM Periods");
            ddlPeriod.DisplayMember = "Name";
            ddlPeriod.ValueMember = "PeriodID";
            ddlPeriod.DataSource = Period.Tables[0];
        }

        public ClassDetails()
        {
            InitializeComponent();
            UpdateDropDowns();
            this.TabText = "New Class";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@CourseID", ddlCourse.SelectedValue);
                parameters.Add("@InstructorID", ddlInstructor.SelectedValue);
                parameters.Add("@ClassRoomID", ddlClassRoom.SelectedValue);
                parameters.Add("@SessionID", ddlSession.SelectedValue);
                parameters.Add("@PeriodID", ddlPeriod.SelectedValue);
                parameters.Add("@ClassID", _ClassID);
                if (_ClassID < 0)
                    query = @"INSERT INTO Classes (CourseID, InstructorID, ClassRoomID, SessionID, PeriodID) VALUES (@CourseID, @InstructorID, @ClassRoomID, @SessionID, @PeriodID);";
                else
                    query = @"UPDATE Classes SET CourseID = @CourseID, InstructorID = @InstructorID, ClassRoomID = @ClassRoomID, SessionID = @SessionID, PeriodID = @PeriodID WHERE ClassID = @ClassID";

                _ClassID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Classes)
                    ((Classes)item).RefreshClasses();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@CourseID", ddlCourse.SelectedValue);
                parameters.Add("@InstructorID", ddlInstructor.SelectedValue);
                parameters.Add("@ClassRoomID", ddlClassRoom.SelectedValue);
                parameters.Add("@SessionID", ddlSession.SelectedValue);
                parameters.Add("@PeriodID", ddlPeriod.SelectedValue);
                parameters.Add("@ClassID", _ClassID);
                query = @"INSERT INTO Classes (CourseID, InstructorID, ClassRoomID, SessionID, PeriodID) VALUES (@CourseID, @InstructorID, @ClassRoomID, @SessionID, @PeriodID);";
                _ClassID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Classes)
                {
                    ((Classes)item).RefreshClasses();
                    continue;
                }
            }

            this.TabText = "Class Details";
        }
    }
}
