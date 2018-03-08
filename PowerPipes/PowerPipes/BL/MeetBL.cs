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
	public static class MeetBL
	{
		public static List<MeetHeader> GetTrainings(int idUser, DatabaseConnection db)
		{
			var meetList = new List<MeetHeader>();

			var cmd = new SqlCommand("SELECT * FROM Meet WHERE IdUser =" + idUser + "ORDER BY Date", db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					meetList.Add(new MeetHeader
					{
						Id = (int)reader["Id"],
						Name = reader["Name"].ToString(),
						Date = (DateTime)reader["Date"]
					});
				}
			}

			reader.Dispose();
			cmd.Dispose();

			return meetList;
		}

		public static List<SelectListItem> GetSuccessChoices()
		{
			var choices = new List<SelectListItem>();
			choices.Add(new SelectListItem
			{
				Value = true.ToString(),
				Text = "Oui"
			});
			choices.Add(new SelectListItem
			{
				Value = false.ToString(),
				Text = "Non"
			});

			return choices;			
		}

		public static List<MeetResult> GetResults(int idMeet, DatabaseConnection db)
		{
			var results = new List<MeetResult>();

			var cmd = new SqlCommand("SELECT * FROM MeetResult WHERE IdMeet =" + idMeet, db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					results.Add(new MeetResult
					{
						Id = (int)reader["Id"],
						IdMeet = idMeet,
						MovementType = (int)reader["MovementType"],
						Name = reader["Name"].ToString(),
						Weight = (int)reader["Weight"],
						Success = (bool)reader["Success"]
					});
				}
			}

			reader.Dispose();
			cmd.Dispose();

			return results;
		}

		public static MeetHeader GetHeader(int idMeet, DatabaseConnection db)
		{
			var header = new MeetHeader();

			var cmd = new SqlCommand("SELECT * FROM Meet WHERE Id =" + idMeet, db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					header.Id = (int)reader["Id"];
					header.Name = reader["Name"].ToString();
					header.Date = (DateTime)reader["Date"];
					header.IdUser = (int)reader["IdUser"];
					header.PersonalWeight = (int)reader["PersonalWeight"];
				}
			}

			reader.Dispose();
			cmd.Dispose();

			return header;
		}

		public static void CreateMeet(Meet meet, DatabaseConnection db)
		{
			var cmd = new SqlCommand("INSERT INTO Meet (Name, Date, PersonalWeight, IdUser) output INSERTED.ID VALUES('" + meet.Header.Name + "', '" + meet.Header.Date + "', '" + meet.Header.PersonalWeight + "', '" + meet.Header.IdUser + "')", db.connection);
			meet.Header.Id = (int)cmd.ExecuteScalar();
			cmd.Dispose();

			foreach (var result in meet.Results)
			{
				cmd = new SqlCommand("INSERT INTO MeetResult (IdMeet, Name, MovementType, Weight, Success) " +
					"VALUES(" + meet.Header.Id + ", '" + result.Name + "', " + result.MovementType + ", " + result.Weight + ", " + Convert.ToInt32(result.Success) + ")", db.connection);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
			}
		}
	}
}