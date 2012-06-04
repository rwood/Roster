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
    public partial class Periods : DockContent
    {
        public Periods()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Periods_NewSearchTerm);
            RefreshPeriods();
        }

        void Periods_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshPeriods();
        }

        public void RefreshPeriods()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT PeriodID, Name, StartTime, EndTime FROM Periods  WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                if (this.dataGridView1.Columns[i].ValueType == typeof(DateTime))
                {
                    DataGridViewCellStyle dgvcellStyle = new DataGridViewCellStyle();
                    dgvcellStyle.Format = "T";
                    this.dataGridView1.Columns[i].DefaultCellStyle = dgvcellStyle;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new PeriodDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["PeriodID"]);
                SqlHelper.Parameters.Add("@PeriodID", sessionID);
                SqlHelper.ExecteNonQuery("DELETE FROM Periods WHERE PeriodID = @PeriodID;");
            }
            RefreshPeriods();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 sessionID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["PeriodID"]);
                Central.MainForm.AddPanel(new PeriodDetails(sessionID), DockState.DockBottom, true);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    DateTime StartTime = (DateTime)row.Cells["StartTime"].Value;
            //    row.Cells["StartTime"].ValueType = typeof(string);
            //    row.Cells["StartTime"].Value = StartTime.ToShortTimeString();
            //    DateTime EndTime = (DateTime)row.Cells["EndTime"].Value;
            //    row.Cells["EndTime"].ValueType = typeof(string);
            //    row.Cells["EndTime"].Value = EndTime.ToShortTimeString();
            //}
            //dataGridView1.Columns["StartTime"].ValueType = typeof(string);
            //dataGridView1.Columns["EndTime"].ValueType = typeof(string);
        }

    }
}
