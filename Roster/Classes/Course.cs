using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace HeadCount
{
    public class Course : BaseEntity<Course>
    {
        public Int64 CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override void Load(Int64 _ID)
        {
            string q = "SELECT * FROM Courses WHERE CourseID = @ID";
            SqlHelper.Parameters.Add("@ID", _ID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.CourseID = reader["CourseID"];
                    this.ChangeDate = Convert.ToDateTime(reader["ChangeDate"]);
                    this.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    this.Title = reader["Title"];
                    this.Description = reader["Description"];
                }
            }

        }

        public void Delete()
        {
            string q = "DELETE FROM Courses WHERE CourseID = @ID";
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            string q = @"
UPDATE Courses 
SET Title = @Title,
Description = @Description
WHERE ContactID = @ID";
            SqlHelper.Parameters.Add("@Title", this.Title);
            SqlHelper.Parameters.Add("@Description", this.Description);
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void SaveAsNew()
        {
            this.School.Save();
            string q = @"
INSERT INTO Contacts 
(Title, Description) 
VALUES 
(@Title, @Description)";
            SqlHelper.Parameters.Add("@Title", this.Title);
            SqlHelper.Parameters.Add("@Description", this.Description);
            SqlHelper.ExecteNonQuery(q);
            this.ContactID = Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        }
    }
}
