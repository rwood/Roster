using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;

namespace HeadCount
{
    public abstract class BaseEntity<T> : BaseObject
    {
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
        protected bool Loaded = false;

        #region Neat Ideas that I just ran out of time for.
        //protected override void Load(Int64 _ID) 
        //{
        //    string tablename = typeof(T).Name;
        //    if (tablename.EndsWith("s"))
        //        tablename += "es";
        //    else
        //        tablename += "s";
        //    string query = "SELECT * FROM " + tablename + " WHERE " + typeof(T).Name + "ID = @ID";
        //    SqlHelper.Parameters.Add("@ID", _ID);
        //    DbDataReader reader = SqlHelper.GetDataReader(query);
        //    reader.Read();
        //    T obj = SqlHelper.LoadObj<T>(reader);
        //    foreach (PropertyInfo info in this.GetType().GetProperties())
        //    {
        //        info.SetValue(this, obj.GetType().GetProperty(info.Name).GetValue(obj, null), null);
        //    }
        //    this.Loaded = true;
        //    reader.Close();
        //}

        //protected bool Delete(Int64 _ID)
        //{
        //    string tablename = typeof(T).Name;
        //    if (tablename.EndsWith("s"))
        //        tablename += "es";
        //    else
        //        tablename += "s";
        //    string query = "DELETE FROM " + tablename + " WHERE " + typeof(T).Name + "ID = @ID";
        //    SqlHelper.Parameters.Add("@ID", _ID);
        //    SqlHelper.ExecteNonQuery(query);
        //    return true;
        //}

        //protected Int64 Save()
        //{
        //    if (!this.Loaded)
        //        return -1;
        //    return -1;
        //}

        //protected Int64 SaveAsNew()
        //{
        //    string tablename = typeof(T).Name;
        //    if (tablename.EndsWith("s"))
        //        tablename += "es";
        //    else
        //        tablename += "s";
        //    string query = "INSERT INTO " + tablename + " (";
        //    string query2 = "";
        //    foreach (PropertyInfo info in typeof(T).GetProperties())
        //    {
        //        if (info.Name.ToUpper().EndsWith("ID"))
        //            continue;
        //        if (info.Name.ToUpper() == "CREATEDATE" || info.Name.ToUpper() == "CHANGEDATE")
        //            continue;
        //        query += info.Name + ", ";
        //        query2 += "@" + info.Name + ", ";
        //        SqlHelper.Parameters.Add("@" + info.Name, info.GetValue(this, null));
        //    }
        //    query = query.Substring(0, query.Length - 2);
        //    query2 = query2.Substring(0, query2.Length - 2);
        //    query += ") VALUES (" + query2 + ");";
        //    SqlHelper.ExecteNonQuery(query);
        //    return Convert.ToInt64(SqlHelper.GetScalar("SELECT last_insert_rowid()"));
        //} 
        #endregion
    }
}
