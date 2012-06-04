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
    public partial class ClassRooms : DockContent
    {
        public ClassRooms()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(ClassRooms_NewSearchTerm);
            RefreshClassRooms();
        }

        void ClassRooms_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshClassRooms();
        }

        public void RefreshClassRooms()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT ClassRoomID, Schools.Name AS School, RoomNumber, MaxStudents, Notes 
FROM ClassRooms LEFT OUTER JOIN Schools ON ClassRooms.SchoolID = Schools.SchoolID WHERE 1 = 1 " + GetSearchTerms(dataGridView1));
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new ClassRoomDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 classRoomID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassRoomID"]);
                SqlHelper.Parameters.Add("@ClassRoomID", classRoomID);
                SqlHelper.ExecteNonQuery("DELETE FROM ClassRooms WHERE ClassRoomID = @ClassRoomID;");
            }
            RefreshClassRooms();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 classRoomID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassRoomID"]);
                Central.MainForm.AddPanel(new ClassRoomDetails(classRoomID), DockState.DockBottom, true);
            }
        }

    }
}
