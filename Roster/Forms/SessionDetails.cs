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
    
    public partial class SessionDetails : DockContent
    {
        Int64 _SessionID = -1;
        Int64 _ContactID = -1;
        public SessionDetails(Int64 SessionID)
        {
            InitializeComponent();

            UpdateSessionData(SessionID);
            this.TabText = txtName.Text + " Details";
        }

        private void UpdateSessionData(Int64 SessionID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("SessionID", SessionID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT SessionID, Name, StartDate, EndDate FROM Sessions WHERE SessionID = @SessionID", parameters);
            while (dr.Read())
            {
                dtpEnd.Value = DateTime.Parse(dr["EndDate"].ToString());
                dtpStart.Value = DateTime.Parse(dr["StartDate"].ToString());
                txtName.Text = dr["Name"].ToString();
                _SessionID = Convert.ToInt64(dr["SessionID"]);
            }
            dr.Close();
        }

        public SessionDetails()
        {
            InitializeComponent();
            this.TabText = "New Session";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@StartDate", dtpStart.Value.Date);
                parameters.Add("@EndDate", dtpEnd.Value.Date);
                parameters.Add("@SessionID", _SessionID);
                if (_SessionID < 0)
                    query = @"INSERT INTO Sessions (Name, StartDate, EndDate) VALUES (@Name, @StartDate, @EndDate);";
                else
                    query = @"UPDATE Sessions SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate WHERE SessionID = @SessionID";

                _SessionID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Sessions)
                    ((Sessions)item).RefreshSessions();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@StartDate", dtpStart.Value.Date);
                parameters.Add("@EndDate", dtpEnd.Value.Date);
                query = @"INSERT INTO Sessions (Name, StartDate, EndDate) VALUES (@Name, @StartDate, @EndDate);";

                _SessionID = SqlHelper.ExecteNonQuery(query, parameters);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Sessions)
                {
                    ((Sessions)item).RefreshSessions();
                    continue;
                }
            }

            this.TabText = txtName.Text + " Details";
        }
    }
}
