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
    
    public partial class EnrollmentTags : DockContent
    {
        public EnrollmentTags()
        {
            InitializeComponent();
            LoadTags();
        }

        private void LoadTags()
        {
            listTags.DataSource = null;
            listTags.Items.Clear();
            string query = @"SELECT Tag FROM EnrollmentTags";
            listTags.DisplayMember = "Tag";
            listTags.ValueMember = "Tag";
            SQLiteDataReader reader = SqlHelper.GetDataReader(query);
            List<string> tags = new List<string>();
            while(reader.Read())
            {
                tags.Add(reader["Tag"].ToString());
            }
            listTags.DataSource = tags;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNew.Text.Length < 1)
                    return;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@Tag", txtNew.Text);
                string query = @"INSERT INTO EnrollmentTags (Tag) VALUES (@Tag);";

                SqlHelper.ExecteNonQuery(query, p);
                LoadTags();
                txtNew.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listTags_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (listTags.SelectedIndex < 0)
                        return;
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("@Tag", listTags.SelectedValue.ToString());
                    string query = @"DELETE FROM EnrollmentTags WHERE Tag = @Tag;";

                    SqlHelper.ExecteNonQuery(query, p);
                    LoadTags();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
