using PowerPipes.DataAccess;
using PowerPipes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerPipes.BL
{
    public static class UserBL
    {
        public static User GetUser(int idUser, DatabaseConnection db)
        {
            var user = new User();

            var cmd = new SqlCommand("SELECT * FROM Users WHERE Id =" + idUser, db.connection);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                user.UserName = (String)reader["UserName"];
                user.Name = (String)reader["Name"];
                user.Age = (int)reader["Age"];
            }

            reader.Dispose();
            cmd.Dispose();

            return user;
        }

        public static void UpdateUser(Profile profile, DatabaseConnection db)
        {
            var cmd = new SqlCommand("UPDATE Users SET UserName= '" + profile.UserName +
                "', Name = '" + profile.Name +
                "', Age = '" + profile.Age+
                "' WHERE Id =" + profile.IdUser, db.connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}
