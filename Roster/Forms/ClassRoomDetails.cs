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
    
    public partial class ClassRoomDetails : DockContent
    {
        Int64 _ClassRoomID = -1;
        public ClassRoomDetails(Int64 ClassRoomID)
        {
            InitializeComponent();
            UpdateSchoolList();

            UpdateClassRoomData(ClassRoomID);
            this.TabText = ddlSchool.SelectedText + txtRoomNumber.Text + " Details";
        }

        private void UpdateSchoolList()
        {
            DataSet ds = SqlHelper.GetDataSet(@"SELECT SchoolID, Name FROM Schools");
            ddlSchool.DisplayMember = "Name";
            ddlSchool.ValueMember = "SchoolID";
            ddlSchool.DataSource = ds.Tables[0];
        }

        private void UpdateClassRoomData(Int64 ClassRoomID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ClassRoomID", ClassRoomID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT ClassRoomID, SchoolID, RoomNumber, MaxStudents, Notes
FROM ClassRooms WHERE ClassRoomID = @ClassRoomID", parameters);
            while (dr.Read())
            {
                ddlSchool.SelectedValue = dr["SchoolID"];
                txtRoomNumber.Text = dr["RoomNumber"].ToString();
                txtMaxStudents.Text = dr["MaxStudents"].ToString();
                txtNotes.Text = dr["Notes"].ToString();
                _ClassRoomID = Convert.ToInt64(dr["ClassRoomID"]);
            }
            dr.Close();
        }

        public ClassRoomDetails()
        {
            InitializeComponent();
            UpdateSchoolList();
            this.TabText = "New Class Room";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@SchoolID", ddlSchool.SelectedValue);
                parameters.Add("@RoomNumber", txtRoomNumber.Text);
                parameters.Add("@MaxStudents", txtMaxStudents.Text);
                parameters.Add("@Notes", txtNotes.Text);
                parameters.Add("@ClassRoomID", _ClassRoomID);
                if (_ClassRoomID < 0)
                    query = @"INSERT INTO ClassRooms (SchoolID, RoomNumber, MaxStudents, Notes) VALUES (@SchoolID, @RoomNumber, @MaxStudents, @Notes);";
                else
                    query = @"UPDATE ClassRooms SET SchoolID = @SchoolID, RoomNumber = @RoomNumber, MaxStudents = @MaxStudents, Notes = @Notes WHERE ClassRoomID = @ClassRoomID";

                _ClassRoomID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is ClassRooms)
                    ((ClassRooms)item).RefreshClassRooms();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@SchoolID", ddlSchool.SelectedValue);
                parameters.Add("@RoomNumber", txtRoomNumber.Text);
                parameters.Add("@MaxStudents", txtMaxStudents.Text);
                parameters.Add("@Notes", txtNotes.Text);
                parameters.Add("@ClassRoomID", _ClassRoomID);
                query = @"INSERT INTO ClassRooms (SchoolID, RoomNumber, MaxStudents, Notes) VALUES (@SchoolID, @RoomNumber, @MaxStudents, @Notes);";

                _ClassRoomID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is ClassRooms)
                {
                    ((ClassRooms)item).RefreshClassRooms();
                    continue;
                }
            }

            this.TabText = ddlSchool.SelectedText + " " + txtRoomNumber.Text + " Details";
        }
    }
}
