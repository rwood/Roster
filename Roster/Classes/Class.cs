using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace HeadCount
{
    public class Class : BaseEntity<Class>
    {
        public Int64 ClassID { get; set; }
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public List<Enrollment> Roll { get; set; }
        public Session Session { get; set; }
        public Period Period { get; set; }

        public Class()
        {

        }

        public Class(Int64 _ID)
        {
            this.Load(_ID);
        }

        public override void Load(Int64 _ID)
        {
            string q = "SELECT * FROM Classes WHERE ClassID = @ID";
            SqlHelper.Parameters.Add("@ID", _ID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.ClassID = reader["ClassID"];
                    this.Course = new Course((Int64)reader["CourseID"]);
                    this.Instructor = new Instructor((Int64)reader["InstructorID"]);
                    this.ClassRoom = new ClassRoom((Int64)reader["ClassRoomID"]);
                    this.Session = new Session((Int64)reader["SessionID"]);
                    this.Period = new Period((Int64)reader["PeriodID"]);
                    this.ChangeDate = (DateTime)reader["ChangeDate"];
                    this.CreateDate = (DateTime)reader["CreateDate"];
                    this.Roll = new List<Enrollment>();
                    q = "SELECT * FROM Enrollments WHERE ClassID = @ID";
                    SqlHelper.Parameters.Add("@ID", this.ClassID);
                    using (SQLiteDataReader reader1 = SqlHelper.GetDataReader(q))
                    {
                        while (reader.Read())
                        {
                            Enrollment enrollment = new Enrollment();
                            enrollment.Class = this;
                            enrollment.ChangeDate = (DateTime)reader1["ChangeDate"];
                            enrollment.CreateDate = (DateTime)reader1["CreateDate"];
                            enrollment.FeesPaid = (float)reader["FeesPaid"];
                            enrollment.Grade = (float)reader["Grade"];
                            enrollment.Student = new Student((Int64)reader["StudentID"]);
                            this.Roll.Add(enrollment);
                        }
                    }

                }
            }
            
        }

        public void Delete()
        {
            string q = "DELETE FROM Classes WHERE ClassID = @ID";
            SqlHelper.Parameters.Add("@ID", this.ClassID);
            SqlHelper.ExecteNonQuery(q);
            foreach (Enrollment item in Roll)
            {
                item.Delete();
            }
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            this.Course.Save();
            this.Instructor.Save();
            this.ClassRoom.Save();
            this.Session.Save();
            this.Period.Save();

            string q = @"
UPDATE Classes 
SET CourseID = @CourseID, 
InstructorID = @InstructorID, 
ClassroomD = @ClasRoomID,
SessionID = @SessionID, 
PeriodID = @PeriodID
WHERE ClassID = @ID";
            SqlHelper.Parameters.Add("@CourseID", this.Course.CourseID);
            SqlHelper.Parameters.Add("@InstructorID", this.Instructor.InstructorID);
            SqlHelper.Parameters.Add("@ClassRoomID", this.ClassRoom.ClassRoomID);
            SqlHelper.Parameters.Add("@SessionID", this.Session.SessionID);
            SqlHelper.Parameters.Add("@PeriodID", this.Period.PeriodID);
            SqlHelper.Parameters.Add("@ID", this.ClassID);
            SqlHelper.ExecteNonQuery(q);
        }

        public Int64 SaveAsNew()
        {
            this.Course.Save();
            this.Instructor.Save();
            this.ClassRoom.Save();
            this.Session.Save();
            this.Period.Save();
            string q = @"
INSERT INTO Classes 
(CourseID. InstructorID, ClassRoomID, SessionID, PeriodID) 
VALUES 
(@CourseID. @InstructorID, @ClassRoomID, @SessionID, @PeriodID)";
            SqlHelper.Parameters.Add("@CourseID", this.Course.CourseID);
            SqlHelper.Parameters.Add("@InstructorID", this.Instructor.InstructorID);
            SqlHelper.Parameters.Add("@ClassRoomID", this.ClassRoom.ClassRoomID);
            SqlHelper.Parameters.Add("@SessionID", this.Session.SessionID);
            SqlHelper.Parameters.Add("@PeriodID", this.Period.PeriodID);
            SqlHelper.Parameters.Add("@ID", this.ClassID);
            SqlHelper.ExecteNonQuery(q);
            this.ClassID = Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        }
    }
}
