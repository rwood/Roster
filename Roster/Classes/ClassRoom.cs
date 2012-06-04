using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace HeadCount
{
    public class ClassRoom : BaseEntity<ClassRoom>
    {
        public Int64 ClassRoomID { get; set; }
        public School School { get; set; }
        public Int64 RoomNumber { get; set; }
        public Int64 MaxStudents { get; set; }
        public string Notes { get; set; }

        public override void Load(Int64 _ID)
        {
            string q = "SELECT * FROM ClassRooms WHERE ClassRoomID = @ID";
            SqlHelper.Parameters.Add("@ID", _ID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.ClassRoomID = reader["ClassRoomID"];
                    this.ChangeDate = Convert.ToDateTime(reader["ChangeDate"]);
                    this.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    this.MaxStudents = Convert.ToInt64(reader["MaxStudents"]);
                    this.Notes = reader["Notes"].ToString();
                    this.RoomNumber = Convert.ToInt64(reader["RoomNumber"]);
                    this.School = new School(Convert.ToInt64(reader["SchoolID"]));
                }
            }

        }

        public void Delete()
        {
            string q = "DELETE FROM ClassRooms WHERE ClassRoomID = @ID";
            SqlHelper.Parameters.Add("@ID", this.ClassRoomID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            this.School.Save();
            string q = @"
UPDATE ClassRooms 
SET SchoolID = @SchoolID,
RoomNumber = @RoomNumber, 
MaxStudents = @MaxStudents, 
Notes = @Notes
WHERE ClassRoomID = @ID";
            SqlHelper.Parameters.Add("@CourseID", this.Course.CourseID);
            SqlHelper.Parameters.Add("@SchoolID", this.School.SchoolID);
            SqlHelper.Parameters.Add("@RoomNumber", this.RoomNumber);
            SqlHelper.Parameters.Add("@MaxStudents", this.MaxStudents);
            SqlHelper.Parameters.Add("@Notes", this.Notes);
            SqlHelper.Parameters.Add("@ID", this.ClassRoomID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void SaveAsNew()
        {
            this.School.Save();
            string q = @"
INSERT INTO ClassRooms 
(SchoolID, RoomNumber, MaxStudents, Notes) 
VALUES 
(@SchoolID, @RoomNumber, @MaxStudents, @Notes)";
            SqlHelper.Parameters.Add("@SchoolID", this.School.SchoolID);
            SqlHelper.Parameters.Add("@RoomNumber", this.RoomNumber);
            SqlHelper.Parameters.Add("@MaxStudents", this.MaxStudents);
            SqlHelper.Parameters.Add("@Notes", this.Notes);
            SqlHelper.Parameters.Add("@ID", this.ClassRoomID);
            SqlHelper.ExecteNonQuery(q);
            this.ClassRoomID = Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        }
    }


}
    

