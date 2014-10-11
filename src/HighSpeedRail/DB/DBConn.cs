using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace HighSpeedRail.DB
{
    public static class DBConn
    {
        public static SQLiteConnection getConnection()
        {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings["AccountDB"].ConnectionString);
        }
    }
}