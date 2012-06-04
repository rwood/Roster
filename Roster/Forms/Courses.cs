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
    public partial class Courses : DockContent
    {
        public Courses()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Courses_NewSearchTerm);
            RefreshCourses();
        }

        void Courses_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshCourses();
        }

        public void RefreshCourses()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT CourseID, Title, Description FROM Course WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new CourseDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["CourseID"]);
                SqlHelper.Parameters.Add("@CourseID", sessionID);
                SqlHelper.ExecteNonQuery("DELETE FROM Course WHERE CourseID = @CourseID;");
            }
            RefreshCourses();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["CourseID"]);
                Central.MainForm.AddPanel(new CourseDetails(sessionID), DockState.DockBottom, true);
            }
        }

    }
}
