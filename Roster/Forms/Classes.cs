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
    public partial class Classes : DockContent
    {
        public Classes()
        {
            InitializeComponent();
            this.NewSearchTerm += new EventHandler(Classes_NewSearchTerm);
            RefreshClasses();
        }

        void Classes_NewSearchTerm(object sender, EventArgs e)
        {
            RefreshClasses();
        }

        public void RefreshClasses()
        {
            BindingSource bs = new BindingSource();
            SqlHelper.Parameters.Clear();
            SqlHelper.Parameters.Add("@SearchTerm", "%" + this.SearchFilterTerm + "%");
            DataSet ds = SqlHelper.GetDataSet(@"SELECT ClassID, Course.Title AS Course, Instructors.LastName AS Instructor,
Schools.Name AS School, ClassRooms.RoomNumber AS ClassRoom, Sessions.Name AS Session, Periods.Name AS Period
FROM Classes
LEFT OUTER JOIN Course ON Classes.CourseID = Course.CourseID
LEFT OUTER JOIN Instructors ON Classes.InstructorID = Instructors.InstructorID
LEFT OUTER JOIN ClassRooms ON Classes.ClassRoomID = ClassRooms.ClassRoomID
LEFT OUTER JOIN Sessions ON Classes.SessionID = Sessions.SessionID
LEFT OUTER JOIN Periods ON Classes.PeriodID = Periods.PeriodID
LEFT OUTER JOIN Schools ON ClassRooms.SchoolID = Schools.SchoolID WHERE 1 = 1 " + GetSearchTerms(dataGridView1) );
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Central.MainForm.AddPanel(new ClassDetails(), DockState.DockBottom, true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 classID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassID"]);
                SqlHelper.Parameters.Add("@ClassID", classID);
                SqlHelper.ExecteNonQuery("DELETE FROM Classes WHERE ClassID = @ClassID;");
            }
            RefreshClasses();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Int64 classID = Convert.ToInt64(((DataRowView)row.DataBoundItem).Row["ClassID"]);
                Central.MainForm.AddPanel(new ClassDetails(classID), DockState.DockBottom, true);
            }
        }

    }
}
