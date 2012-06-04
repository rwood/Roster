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
    
    public partial class StudentDetails : DockContent
    {
        Int64 _StudentID = -1;
        Int64 _ContactID = -1;
        public StudentDetails(Int64 StudentID)
        {
            InitializeComponent();
            UpdateSchoolList();

            UpdateStudentData(StudentID);
            txtLastName.TextChanged += new EventHandler(name_TextChanged);
            txtFirstName.TextChanged += new EventHandler(name_TextChanged);
            this.TabText = txtFirstName.Text + " " + txtLastName.Text + " Details";
        }

        private void UpdateStudentData(Int64 StudentID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("StudentID", StudentID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT StudentID, FirstName, LastName,
Address1, Address2, City, State, Zip, Phone, Mobile, Email, SchoolID, Students.ContactID
FROM Students LEFT OUTER JOIN Contacts ON Students.ContactID = Contacts.ContactID
WHERE StudentID = @StudentID", parameters);
            while (dr.Read())
            {
                txtAddress1.Text = dr["Address1"].ToString();
                txtAddress2.Text = dr["Address2"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                txtZip.Text = dr["Zip"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                txtMobile.Text = dr["Mobile"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtFirstName.Text = dr["FirstName"].ToString();
                txtLastName.Text = dr["LastName"].ToString();
                _StudentID = Convert.ToInt64(dr["StudentID"]);
                _ContactID = Convert.ToInt64(dr["ContactID"]);
                ddlSchool.SelectedValue = dr["SchoolID"];
            }
            dr.Close();
            
        }

        private void UpdateSchoolList()
        {
            DataSet ds = SqlHelper.GetDataSet(@"SELECT SchoolID, Name FROM Schools");
            ddlSchool.DisplayMember = "Name";
            ddlSchool.ValueMember = "SchoolID";
            ddlSchool.DataSource = ds.Tables[0];
        }

        public StudentDetails()
        {
            InitializeComponent();
            UpdateSchoolList();
            txtLastName.TextChanged += new EventHandler(name_TextChanged);
            txtFirstName.TextChanged += new EventHandler(name_TextChanged);
            this.TabText = "New Student";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ContactID > 0)
                {
                    SqlHelper.UpdateContact(_ContactID, txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, txtMobile.Text, txtEmail.Text);
                }
                else
                {
                    _ContactID = SqlHelper.CreateNewContact(txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, txtMobile.Text, txtEmail.Text);
                }
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@FirstName", txtFirstName.Text);
                parameters.Add("@LastName", txtLastName.Text);
                parameters.Add("@ContactID", _ContactID);
                parameters.Add("@SchoolID", ddlSchool.SelectedValue);
                parameters.Add("@StudentID", _StudentID);
                if (_StudentID < 0)
                    query = @"INSERT INTO Students (FirstName, LastName, ContactID, SchoolID) VALUES (@FirstName, @LastName, @ContactID, @SchoolID);";
                else
                    query = @"UPDATE Students SET FirstName = @FirstName, LastName = @LastName, SchoolID = @SchoolID WHERE StudentID = @StudentID";

                _StudentID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Students)
                    ((Students)item).RefreshStudents();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                _ContactID = SqlHelper.CreateNewContact(txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, txtMobile.Text, txtEmail.Text);
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@FirstName", txtFirstName.Text);
                parameters.Add("@LastName", txtLastName.Text);
                parameters.Add("@ContactID", _ContactID);
                parameters.Add("@SchoolID", ddlSchool.SelectedValue);
                query = @"INSERT INTO Students (FirstName, LastName, ContactID, SchoolID) VALUES (@FirstName, @LastName, @ContactID, @SchoolID);";

                _StudentID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Students)
                {
                    ((Students)item).RefreshStudents();
                    continue;
                }
            }

            this.TabText = txtFirstName.Text + " " + txtLastName.Text + " Details";
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT Count(*) FROM Students WHERE FirstName = @FirstName AND LastName = @LastName;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@FirstName", txtFirstName.Text);
            parameters.Add("@LastName", txtLastName.Text);
            int records = Convert.ToInt32(SqlHelper.GetScalar(query, parameters));
            if (records > 0)
            {
                MessageBox.Show("Note: A student already exists with that name.");
            } 
        }
    }
}
