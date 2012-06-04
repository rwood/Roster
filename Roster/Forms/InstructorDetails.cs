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
    
    public partial class InstructorDetails : DockContent
    {
        Int64 _InstructorID = -1;
        Int64 _ContactID = -1;
        public InstructorDetails(Int64 InstructorID)
        {
            InitializeComponent();

            UpdateInstructorData(InstructorID);
            this.TabText = txtFirstName.Text + " " + txtLastName.Text + " Details";
        }

        private void UpdateInstructorData(Int64 InstructorID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("InstructorID", InstructorID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT InstructorID, FirstName, LastName,
Address1, Address2, City, State, Zip, Phone, Mobile, Email, Instructors.ContactID
FROM Instructors LEFT OUTER JOIN Contacts ON Instructors.ContactID = Contacts.ContactID
WHERE InstructorID = @InstructorID", parameters);
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
                _InstructorID = Convert.ToInt64(dr["InstructorID"]);
                _ContactID = Convert.ToInt64(dr["ContactID"]);
            }
            dr.Close();
        }

        public InstructorDetails()
        {
            InitializeComponent();
            this.TabText = "New Instructor";
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
                parameters.Add("FirstName", txtFirstName.Text);
                parameters.Add("LastName", txtLastName.Text);
                parameters.Add("ContactID", _ContactID);
                parameters.Add("InstructorID", _InstructorID);
                if (_InstructorID < 0)
                    query = @"INSERT INTO Instructors (FirstName, LastName, ContactID) VALUES (@FirstName, @LastName, @ContactID);";
                else
                    query = @"UPDATE Instructors SET FirstName = @FirstName, LastName = @LastName WHERE InstructorID = @InstructorID";

                _InstructorID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Instructors)
                    ((Instructors)item).RefreshInstructors();
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
                parameters.Add("FirstName", txtFirstName.Text);
                parameters.Add("LastName", txtLastName.Text);
                parameters.Add("ContactID", _ContactID);
                query = @"INSERT INTO Instructors (FirstName, LastName, ContactID) VALUES (@FirstName, @LastName, @ContactID);";

                _InstructorID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Instructors)
                {
                    ((Instructors)item).RefreshInstructors();
                    continue;
                }
            }

            this.TabText = txtFirstName.Text + " " + txtLastName.Text + " Details";
        }
    }
}
