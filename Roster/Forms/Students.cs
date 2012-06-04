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
    public partial class Students : DockContent
    {
        public Students()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Students_NewSearchTerm);
            RefreshStudents();
        }

        void Students_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshStudents();
        }
        
        public void RefreshStudents()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT StudentID, FirstName, LastName,
Address1, Address2, City, State, Zip, Phone, Mobile, Email, Name AS School
FROM Students LEFT OUTER JOIN Contacts ON Students.ContactID = Contacts.ContactID
LEFT OUTER JOIN Schools ON Students.SchoolID = Schools.SchoolID WHERE 1 = 1 " + GetSearchTerms(dataGridView1) );
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new StudentDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["StudentID"]);
                string query = "SELECT ContactID FROM Students WHERE StudentID = @StudentID;";
                SqlHelper.Parameters.Add("@StudentID", studentID);
                object contactID = SqlHelper.GetScalar(query);
                if(!(contactID is DBNull) && Convert.ToInt64(contactID) > 0)
                {
                    SqlHelper.Parameters.Add("@ContactID", contactID);
                    SqlHelper.ExecteNonQuery("DELETE FROM Contacts WHERE ContactID = @ContactID;");
                }
                SqlHelper.Parameters.Add("@StudentID", studentID);
                SqlHelper.ExecteNonQuery("DELETE FROM Students WHERE StudentID = @StudentID;");
            }
            RefreshStudents();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 studentID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["StudentID"]);
                Central.MainForm.AddPanel(new StudentDetails(studentID), DockState.DockBottom, true);
            }
        }

    }
}
