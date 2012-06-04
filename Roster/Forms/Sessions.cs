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
    public partial class Sessions : DockContent
    {
        public Sessions()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Sessions_NewSearchTerm);
            RefreshSessions();
        }

        void Sessions_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshSessions();
        }

        public void RefreshSessions()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT SessionID, Name, StartDate, EndDate FROM Sessions WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new SessionDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["SessionID"]);
                SqlHelper.Parameters.Add("@SessionID", sessionID);
                SqlHelper.ExecteNonQuery("DELETE FROM Sessions WHERE SessionID = @SessionID;");
            }
            RefreshSessions();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["SessionID"]);
                Central.MainForm.AddPanel(new SessionDetails(sessionID), DockState.DockBottom, true);
            }
        }

    }
}
