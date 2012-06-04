using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeadCount
{
    public class School : BaseEntity<School>
    {
        public Int64 SchoolID { get; set; }
        public string Name { get; set; }
        public Contact ContactInfo { get; set; }

        public override void Load(Int64 _ID)
        {
            string q = "SELECT * FROM School WHERE SchoolID = @ID";
            SqlHelper.Parameters.Add("@ID", _ID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.SchoolID = reader["SchoolID"];
                    this.ChangeDate = Convert.ToDateTime(reader["ChangeDate"]);
                    this.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    this.Name = reader["Name"];
                    this.ContactInfo = new Contact(Convert.ToInt64(reader["ContactID"]));
                }
            }

        }

        public void Delete()
        {
            string q = "DELETE FROM Schools WHERE SchoolID = @ID";
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            this.ContactInfo.Save();
            string q = @"
UPDATE Schools
SET Name = @Name,
ContactID = @ContactID
WHERE SchoolID = @ID";
            SqlHelper.Parameters.Add("@Name", this.Name);
            SqlHelper.Parameters.Add("@ContactID", this.ContactInfo.ContactID);
            SqlHelper.Parameters.Add("@ID", this.CourseID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void SaveAsNew()
        {
            this.ContactInfo.Save();
            string q = @"
INSERT INTO Schools
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
