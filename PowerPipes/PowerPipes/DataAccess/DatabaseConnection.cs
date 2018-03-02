using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PowerPipes.DataAccess
{
    public class DatabaseConnection
    {
        public SqlConnection connection;

        public DatabaseConnection(string path)
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"App_Data\Database.mdf;Integrated Security=True");
        }
    }
}