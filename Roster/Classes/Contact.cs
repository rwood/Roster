using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace HeadCount
{
    public class Contact : BaseEntity<Contact>
    {
        public Int64 ContactID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public override void Load(Int64 _ID)
        {
            string q = "SELECT * FROM Contacts WHERE ContactID = @ID";
            SqlHelper.Parameters.Add("@ID", _ID);
            using (SQLiteDataReader reader = SqlHelper.GetDataReader(q))
            {
                while (reader.Read())
                {
                    this.Loaded = true;
                    this.ContactID = reader["ContactID"];
                    this.ChangeDate = Convert.ToDateTime(reader["ChangeDate"]);
                    this.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    this.Address1 = reader["Address1"].ToString();
                    this.Address2 = reader["Address2"].ToString();
                    this.City = reader["City"].ToString();
                    this.State = reader["State"].ToString();
                    this.Zip = reader["Zip"].ToString();
                    this.Phone = reader["Phone"].ToString();
                    this.Mobile = reader["Mobile"].ToString();
                    this.Email = reader["Email"].ToString();
                }
            }

        }

        public void Delete()
        {
            string q = "DELETE FROM Contacts WHERE ContactID = @ID";
            SqlHelper.Parameters.Add("@ID", this.ContactID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void Save()
        {
            if (!this.Loaded)
                return SaveAsNew();
            this.School.Save();
            string q = @"
UPDATE Contacts
SET Address1 = @Address1,
Address2 = @Address2,
City = @City, 
State = @State,
Zip = @Zip,
Phone = @Phone, 
Mobile = @Mobile,
Email = @Email
WHERE ContactID = @ID";
            SqlHelper.Parameters.Add("@Address1", this.Address1);
            SqlHelper.Parameters.Add("@Address2", this.Address2);
            SqlHelper.Parameters.Add("@City", this.City);
            SqlHelper.Parameters.Add("@State", this.State);
            SqlHelper.Parameters.Add("@Zip", this.Zip);
            SqlHelper.Parameters.Add("@Phone", this.Phone);
            SqlHelper.Parameters.Add("@Mobile", this.Mobile);
            SqlHelper.Parameters.Add("@Email", this.Email);
            SqlHelper.Parameters.Add("@ID", this.ContactID);
            SqlHelper.ExecteNonQuery(q);
        }

        public void SaveAsNew()
        {
            this.School.Save();
            string q = @"
INSERT INTO Contacts 
(Address1, Address2, City, State, Zip, Phone, Mobile, Email) 
VALUES 
(@Address1, @Address2, @City, @State, @Zip, @Phone, @Mobile, @Email)";
            SqlHelper.Parameters.Add("@Address1", this.Address1);
            SqlHelper.Parameters.Add("@Address2", this.Address2); 
            SqlHelper.Parameters.Add("@City", this.City);
            SqlHelper.Parameters.Add("@State", this.State);
            SqlHelper.Parameters.Add("@Zip", this.Zip);
            SqlHelper.Parameters.Add("@Phone", this.Phone);
            SqlHelper.Parameters.Add("@Mobile", this.Mobile);
            SqlHelper.Parameters.Add("@Email", this.Email);
            SqlHelper.ExecteNonQuery(q);
            this.ContactID = Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        }
    }
}
