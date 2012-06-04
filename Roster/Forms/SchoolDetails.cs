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
    
    public partial class SchoolDetails : DockContent
    {
        Int64 _SchoolID = -1;
        Int64 _ContactID = -1;
        public SchoolDetails(Int64 SchoolID)
        {
            InitializeComponent();

            UpdateSchoolData(SchoolID);
            this.TabText = txtName.Text + " Details";
        }

        private void UpdateSchoolData(Int64 SchoolID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("SchoolID", SchoolID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT SchoolID, Name,
Address1, Address2, City, State, Zip, Phone, Mobile, Email, Schools.ContactID
FROM Schools LEFT OUTER JOIN Contacts ON Schools.ContactID = Contacts.ContactID
WHERE SchoolID = @SchoolID", parameters);
            while (dr.Read())
            {
                txtAddress1.Text = dr["Address1"].ToString();
                txtAddress2.Text = dr["Address2"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                txtZip.Text = dr["Zip"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                txtName.Text = dr["Name"].ToString();
                _SchoolID = Convert.ToInt64(dr["SchoolID"]);
                _ContactID = Convert.ToInt64(dr["ContactID"]);
            }
            dr.Close();
        }

        public SchoolDetails()
        {
            InitializeComponent();
            this.TabText = "New School";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ContactID > 0)
                {
                    SqlHelper.UpdateContact(_ContactID, txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, string.Empty, string.Empty);
                }
                else
                {
                    _ContactID = SqlHelper.CreateNewContact(txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, string.Empty, string.Empty);
                }
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@ContactID", _ContactID);
                parameters.Add("@SchoolID", _SchoolID);
                if (_SchoolID < 0)
                    query = @"INSERT INTO Schools (Name, ContactID) VALUES (@Name, @ContactID);";
                else
                    query = @"UPDATE Schools SET Name = @Name, ContactID = @ContactID WHERE SchoolID = @SchoolID";

                _SchoolID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Schools)
                    ((Schools)item).RefreshSchools();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                _ContactID = SqlHelper.CreateNewContact(txtAddress1.Text, txtAddress2.Text, txtCity.Text,
                        txtState.Text, txtZip.Text, txtPhone.Text, string.Empty, string.Empty);
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@ContactID", _ContactID);
                parameters.Add("@SchoolID", _SchoolID);
                query = @"INSERT INTO Schools (Name, ContactID) VALUES (@Name, @ContactID);";

                _SchoolID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Schools)
                {
                    ((Schools)item).RefreshSchools();
                    continue;
                }
            }

            this.TabText = txtName.Text + " Details";
        }
    }
}
