using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Member
{
    public class DataAccess
    {
        private static string dbPath = @"Data Source = C:\Users\csf\Desktop\Member\Member\Member\bin\Debug\Member.db";

        public static DataTable ExecuteQuery(string query) 
        {
            SQLiteConnection con = new SQLiteConnection(dbPath);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

    }
}
