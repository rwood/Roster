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
    
    public partial class PeriodDetails : DockContent
    {
        Int64 _PeriodID = -1;
        Int64 _ContactID = -1;
        public PeriodDetails(Int64 PeriodID)
        {
            InitializeComponent();

            UpdatePeriodData(PeriodID);
            this.TabText = txtName.Text + " Details";
        }

        private void UpdatePeriodData(Int64 PeriodID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("PeriodID", PeriodID);
            SQLiteDataReader dr = SqlHelper.GetDataReader(@"SELECT PeriodID, Name, StartTime, EndTime FROM Periods WHERE PeriodID = @PeriodID", parameters);
            while (dr.Read())
            {
                dtpEnd.Value = DateTime.Parse(dr["EndTime"].ToString());
                dtpStart.Value = DateTime.Parse(dr["StartTime"].ToString());
                txtName.Text = dr["Name"].ToString();
                _PeriodID = Convert.ToInt64(dr["PeriodID"]);
            }
            dr.Close();
        }

        public PeriodDetails()
        {
            InitializeComponent();
            this.TabText = "New Period";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@StartTime", dtpStart.Value.TimeOfDay);
                parameters.Add("@EndTime", dtpEnd.Value.TimeOfDay);
                parameters.Add("@PeriodID", _PeriodID);
                if (_PeriodID < 0)
                    query = @"INSERT INTO Periods (Name, StartTime, EndTime) VALUES (@Name, @StartTime, @EndTime);";
                else
                    query = @"UPDATE Periods SET Name = @Name, StartTime = @StartTime, EndTime = @EndTime WHERE PeriodID = @PeriodID";

                _PeriodID = SqlHelper.ExecteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Periods)
                    ((Periods)item).RefreshPeriods();
            }
        }

        private void btnSaveAsNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", txtName.Text);
                parameters.Add("@StartTime", dtpStart.Value.TimeOfDay);
                parameters.Add("@EndTime", dtpEnd.Value.TimeOfDay);
                query = @"INSERT INTO Periods (Name, StartTime, EndTime) VALUES (@Name, @StartTime, @EndTime);";

                _PeriodID = SqlHelper.ExecteNonQuery(query, parameters);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            Central.MainForm.Focus();
            foreach (DockContent item in Central.MainForm.Contents)
            {
                if (item is Periods)
                {
                    ((Periods)item).RefreshPeriods();
                    continue;
                }
            }

            this.TabText = txtName.Text + " Details";
        }
    }
}
