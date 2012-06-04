using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeadCount
{
    public class Enrollment : BaseEntity<Enrollment>
    {
        public Student Student { get; set; }
        public Class Class { get; set; }
        public float FeesPaid { get; set; }
        public float Grade { get; set; }

        public override void Load(Int64 _StudentID, Int64 _ClassID)
        {
            string q = "SELECT * FROM Enrollment WHERE StudentID = @StudentID AND ClassID = @ClassID";
            SqlHelper.Parameters.Add("@StudentID", _StudentID);
            SqlHelper.Parameters.Add("@ClassID", _ClassID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.EnrollmentID = reader["EnrollmentID"];
                    this.ChangeDate = Convert.ToDateTime(reader["ChangeDate"]);
                    this.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    this.Name = reader["Name"];
                    this.ContactInfo = new Contact(Convert.ToInt64(reader["ContactID"]));
                }
            }

        }

        public void Delete()
        {
            string q = "DELETE FROM Enrollments WHERE EnrollmentID = @ID";
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            this.ContactInfo.Save();
            string q = @"
UPDATE Enrollments
SET Name = @Name,
ContactID = @ContactID
WHERE EnrollmentID = @ID";
            SqlHelper.Parameters.Add("@Name", this.Name);
            SqlHelper.Parameters.Add("@ContactID", this.ContactInfo.ContactID);
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void SaveAsNew()
        {
            this.Enrollment.Save();
            string q = @"
INSERT INTO Enrollments
(Name, ContactID) 
VALUES 
(@Name, @ContactID)";
            SqlHelper.Parameters.Add("@Name", this.Name);
            SqlHelper.Parameters.Add("@ContactID", this.ContactInfo.ContactID);
            SqlHelper.ExecteNonQuery(q);
            this.ContactID = Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        }
    }
}
