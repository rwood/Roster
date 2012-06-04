using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Reflection;
using System.Data.Common;

namespace Roster
{
    public static class SqlHelper
    {
        private static SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        public static Dictionary<string, object> Parameters = new Dictionary<string, object>();

        static SqlHelper()
        {
            builder.DataSource = Central.SaveDir.FullName + "\\Roster.sqlite";
            
            if (!File.Exists(builder.DataSource))
            {
                FileStream fs = File.Create(builder.DataSource);
                fs.Close();
            }
            FileInfo info = new FileInfo(builder.DataSource);
            if (info.Length < 1)
            {
                try
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    Stream sql = asm.GetManifestResourceStream("Roster.RosterDb.sql");
                    StreamReader sr = new StreamReader(sql);
                    string createScripts = sr.ReadToEnd();
                    ExecteNonQuery(createScripts);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

            }
            else
            {
                SortedDictionary<int, string> updates = new SortedDictionary<int, string>();
                try
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    Stream sql = asm.GetManifestResourceStream("Roster.DBUpdate.sql");
                    StreamReader sr = new StreamReader(sql);
                    string createScripts = sr.ReadToEnd();
                    string[] sections = createScripts.Split(new string[] { "{Version" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string section in sections)
                    {
                        int secIndex = section.IndexOf("}");
                        int verNum = Convert.ToInt32(section.Substring(0, secIndex));
                        string verSection = section.Substring(secIndex+1);
                        updates.Add(verNum, verSection);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                int version = 0;
                while (true)
                {
                    string query = "SELECT Version FROM Version";
                    try { version = Convert.ToInt32(GetScalar(query)); } catch{}
                    if (version >= updates.Keys.Max())
                        break;
                    version++;
                    try
                    {
                        ExecteNonQuery(updates[version]);
                    }
                    catch (Exception ex)
                    { System.Windows.Forms.MessageBox.Show(ex.Message); }
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("@Version", version);
                    ExecteNonQuery("UPDATE Version SET Version = @Version", p);
                }
            }
        }

        public static SQLiteConnection GetActiveConnection()
        {
            SQLiteConnection con = new SQLiteConnection(builder.ConnectionString);
            con.Open();
            return con;
        }


        public static SQLiteDataReader GetDataReader(string query)
        {
            SQLiteDataReader rdr = GetDataReader(query, Parameters);
            Parameters.Clear();
            return rdr;
        }

        public static SQLiteDataReader GetDataReader(string query, Dictionary<string, object> parameters)
        {
            SQLiteConnection con = GetActiveConnection();
            SQLiteCommand cmd = con.CreateCommand();
            foreach (string k in parameters.Keys)
            {
                cmd.Parameters.Add(new SQLiteParameter(k, parameters[k]));
            }
            cmd.CommandText = query;
            return cmd.ExecuteReader();
        }

        public static DataSet GetDataSet(string query)
        {
            DataSet ds = GetDataSet(query, Parameters);
            Parameters.Clear();
            return ds;
        }

        public static DataSet GetDataSet(string query, Dictionary<string, object> parameters)
        {
            SQLiteDataAdapter da = GetDataAdapter(query);
            foreach (string k in parameters.Keys)
            {
                da.SelectCommand.Parameters.Add(new SQLiteParameter(k, parameters[k]));
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public static SQLiteDataAdapter GetDataAdapter(string query)
        {
            SQLiteConnection con = GetActiveConnection();
            return new SQLiteDataAdapter(query, con);
        }

        public static object GetScalar(string query)
        {
            object result = GetScalar(query, Parameters);
            Parameters.Clear();
            return result;
        }

        public static object GetScalar(string query, Dictionary<string, object> parameters)
        {
            SQLiteConnection con = GetActiveConnection();
            SQLiteCommand cmd = con.CreateCommand();
            foreach (string k in parameters.Keys)
            {
                cmd.Parameters.Add(new SQLiteParameter(k, parameters[k]));
            }
            cmd.CommandText = query;
            object val = cmd.ExecuteScalar();
            con.Close();
            cmd = null;
            con = null;
            return val;
        }

        public static Int64 ExecteNonQuery(string statement)
        {
            Int64 ret = ExecteNonQuery(statement, Parameters);
            Parameters.Clear();
            return ret;
        }

        public static Int64 ExecteNonQuery(string statement, Dictionary<string, object> parameters)
        {
            SQLiteConnection con = GetActiveConnection();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = statement;
            foreach (string k in parameters.Keys)
            {
                cmd.Parameters.Add(new SQLiteParameter(k, parameters[k]));
            }
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT last_insert_rowid()";
            Int64 ret = Convert.ToInt64(cmd.ExecuteScalar());
            con.Close();
            cmd = null;
            con = null;
            return ret;
        }

        public static Int64 CreateNewContact(string Address1, string Address2, string City, string State, string Zip, string Phone, string Mobile, string Email)
        {
            string query = "INSERT INTO Contacts (Address1, Address2, City, State, Zip, Phone, Mobile, Email) VALUES (@Address1, @Address2, @City, @State, @Zip, @Phone, @Mobile, @Email);";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Address1", Address1);
            parameters.Add("@Address2", Address2);
            parameters.Add("@City", City);
            parameters.Add("@State", State);
            parameters.Add("@Zip", Zip);
            parameters.Add("@Phone", Phone);
            parameters.Add("@Mobile", Mobile);
            parameters.Add("@Email", Email);
            return SqlHelper.ExecteNonQuery(query, parameters);
        }

        public static void UpdateContact(Int64 ContactID, string Address1, string Address2, string City, string State, string Zip, string Phone, string Mobile, string Email)
        {
            string query = "UPDATE Contacts SET Address1 = @Address1, Address2 = @Address2, City = @City, State = @State, Zip = @Zip, Phone = @Phone, Mobile = @Mobile, Email = @Email WHERE ContactID = @ContactID;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ContactID", ContactID);
            parameters.Add("@Address1", Address1);
            parameters.Add("@Address2", Address2);
            parameters.Add("@City", City);
            parameters.Add("@State", State);
            parameters.Add("@Zip", Zip);
            parameters.Add("@Phone", Phone);
            parameters.Add("@Mobile", Mobile);
            parameters.Add("@Email", Email);
            SqlHelper.ExecteNonQuery(query, parameters);
        }

        public static void DeleteContact(Int64 ContactID)
        {
            string query = "DELETE FROM Contacts WHERE ContactID = @ContactID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ContactID", ContactID);
            SqlHelper.ExecteNonQuery(query, parameters);
        }


        //public static T LoadObj<T>(DbDataReader reader)
        //{
        //    T obj = (T)Activator.CreateInstance(typeof(T).Assembly.FullName, typeof(T).FullName).Unwrap();
        //    foreach (PropertyInfo info in obj.GetType().GetProperties())
        //    {
        //        try 
        //        {
        //            bool isDerived = false;
        //            Type baseType = info.PropertyType.BaseType;
        //            while (baseType != typeof(Object) && !isDerived)
        //            {
        //                isDerived = (baseType == typeof(BaseObject));
        //                baseType = baseType.BaseType;
        //            }
        //            if (isDerived)
        //            {
        //                Int64 ID = Convert.ToInt64(reader[info.PropertyType.Name.ToString() + "ID"]);
        //                BaseObject tmp = (BaseObject)Activator.CreateInstance(info.PropertyType);
        //                tmp.Load(ID);
        //                info.SetValue(obj, tmp, null);
        //            }
        //            else
        //                info.SetValue(obj, reader[info.Name], null); 
        //        }
        //        catch (Exception Exception){}
        //    }
        //    return obj;
        //}
    }
}
