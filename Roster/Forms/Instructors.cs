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
    public partial class Instructors : DockContent
    {
        public Instructors()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Instructors_NewSearchTerm);
            RefreshInstructors();
        }

        void Instructors_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshInstructors();
        }

        public void RefreshInstructors()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT InstructorID, FirstName, LastName,
Address1, Address2, City, State, Zip, Phone, Mobile, Email
FROM Instructors LEFT OUTER JOIN Contacts ON Instructors.ContactID = Contacts.ContactID WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new InstructorDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["InstructorID"]);
                string query = "SELECT ContactID FROM Instructors WHERE InstructorID = @InstructorID;";
                SqlHelper.Parameters.Add("@InstructorID", studentID);
                object contactID = SqlHelper.GetScalar(query);
                if(!(contactID is DBNull) && Convert.ToInt64(contactID) > 0)
                {
                    SqlHelper.Parameters.Add("@ContactID", contactID);
                    SqlHelper.ExecteNonQuery("DELETE FROM Contacts WHERE ContactID = @ContactID;");
                }
                SqlHelper.Parameters.Add("@InstructorID", studentID);
                SqlHelper.ExecteNonQuery("DELETE FROM Instructors WHERE InstructorID = @InstructorID;");
            }
            RefreshInstructors();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["InstructorID"]);
                Central.MainForm.AddPanel(new InstructorDetails(studentID), DockState.DockBottom, true);
            }
        }

    }
}
