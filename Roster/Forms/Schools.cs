using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Roster.Forms
{
    public partial class Schools : DockContent
    {
        public Schools()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Schools_NewSearchTerm);
            RefreshSchools();
        }

        void Schools_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshSchools();
        }

        public void RefreshSchools()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT SchoolID, Name,
Address1, Address2, City, State, Zip, Phone
FROM Schools LEFT OUTER JOIN Contacts ON Schools.ContactID = Contacts.ContactID  WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new SchoolDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["SchoolID"]);
                string query = "SELECT ContactID FROM Schools WHERE SchoolID = @SchoolID;";
                SqlHelper.Parameters.Add("@SchoolID", studentID);
                object contactID = SqlHelper.GetScalar(query);
                if(!(contactID is DBNull) && Convert.ToInt64(contactID) > 0)
                {
                    SqlHelper.Parameters.Add("@ContactID", contactID);
                    SqlHelper.ExecteNonQuery("DELETE FROM Contacts WHERE ContactID = @ContactID;");
                }
                SqlHelper.Parameters.Add("@SchoolID", studentID);
                SqlHelper.ExecteNonQuery("DELETE FROM Schools WHERE SchoolID = @SchoolID;");
            }
            RefreshSchools();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["SchoolID"]);
                Central.MainForm.AddPanel(new SchoolDetails(studentID), DockState.DockBottom, true);
            }
        }

    }
}
