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
    
    public partial class CourseDetails : DockContent
    {
        Int64 _CourseID = -1;
        Int64 _ContactID = -1;
        public CourseDetails(Int64 CourseID)
        {
            InitializeComponent();

            UpdateCourseData(CourseID);
            this.TabText = txtTitle.Text + " Details";
        }

        private void UpdateCourseData(Int64 CourseID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("CourseID", CourseID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT CourseID, Title, Description FROM Course WHERE CourseID = @CourseID", parameters);
            while (dr.Read())
            {
                txtTitle.Text = dr["Title"].ToString();
                txtDescription.Text = dr["Description"].ToString();
                _CourseID = Convert.ToInt64(dr["CourseID"]);
            }
            dr.Close();
        }

        public CourseDetails()
        {
            InitializeComponent();
            this.TabText = "New Course";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Title", txtTitle.Text);
                parameters.Add("@Description", txtDescription.Text);
                parameters.Add("@CourseID", _CourseID);
                if (_CourseID < 0)
                    query = @"INSERT INTO Course (Title, Description) VALUES (@Title, @Description);";
                else
                    query = @"UPDATE Course SET Title = @Title, Description = @Description WHERE CourseID = @CourseID";

                _CourseID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Courses)
                    ((Courses)item).RefreshCourses();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Title", txtTitle.Text);
                parameters.Add("@Description", txtDescription.Text);
                parameters.Add("@CourseID", _CourseID);
                query = @"INSERT INTO Course (Title, Description) VALUES (@Title, @Description);";

                _CourseID = SqlHelper.ExecteNonQuery(query, parameters);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Courses)
                {
                    ((Courses)item).RefreshCourses();
                    continue;
                }
            }

            this.TabText = txtTitle.Text + " Details";
        }
    }
}
