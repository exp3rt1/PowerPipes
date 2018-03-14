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
	public static class GoalBL
	{
		public static List<SelectListItem> GetUnits()
		{
			var units = new List<SelectListItem>();
			units.Add(new SelectListItem
			{
				Value = "lbs",
				Text = "lbs"
			});
			units.Add(new SelectListItem
			{
				Value = "kg",
				Text = "kg"
			});

			return units;
		}

		public static List<SelectListItem> GetMovementTypes(DatabaseConnection db)
		{
			var movements = new List<SelectListItem>();

			var cmd = new SqlCommand("SELECT * FROM MovementType", db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					movements.Add(new SelectListItem
					{
						Value = reader["Id"].ToString(),
						Text = reader["Name"].ToString()
					});
				}

				reader.Dispose();
				cmd.Dispose();
			}

			return movements;
		}

		public static List<Goal> GetGoals(int idUser, DatabaseConnection db)
		{
			var goalList = new List<Goal>();

			var cmd = new SqlCommand("SELECT * FROM Goal WHERE IdUser =" + idUser + "ORDER BY Date", db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					goalList.Add(new Goal
					{
						Id = (int)reader["Id"],
						Name = reader["Name"].ToString(),
						Date = (DateTime)reader["Date"],
						Repetition = (int)reader["Repetition"],
						MovementType = (int)reader["MovementType"],
						Weight = (int)reader["Weight"],
						Unit = reader["Unit"].ToString(),
						IdUser = (int)reader["IdUser"]
				});
				}
			}

			reader.Dispose();
			cmd.Dispose();

			return goalList;
		}

		public static Goal GetGoal(int idGoal, DatabaseConnection db)
		{
			var goal = new Goal();

			var cmd = new SqlCommand("SELECT * FROM Goal WHERE Id =" + idGoal, db.connection);

			var reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				reader.Read();
				goal.Id = (int)reader["Id"];
				goal.Name = reader["Name"].ToString();
				goal.Date = (DateTime)reader["Date"];
				goal.Repetition = (int)reader["Repetition"];
				goal.MovementType = (int)reader["MovementType"];
				goal.Weight = (int)reader["Weight"];
				goal.Unit = reader["Unit"].ToString();
				goal.IdUser = (int)reader["IdUser"];
			}

			reader.Dispose();
			cmd.Dispose();

			return goal;
		}

		public static void DeleteGoal(int idGoal, DatabaseConnection db)
		{
			var cmd = new SqlCommand("DELETE FROM Goal WHERE Id = " + idGoal, db.connection);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
		}

		public static void UpdateGoal(Goal goal, DatabaseConnection db)
		{
			var cmd = new SqlCommand("UPDATE Goal SET Date = '" + goal.Date + 
				"', Name = '" + goal.Name +
				"', Repetition = '" + goal.Repetition +
				"', MovementType = '" + goal.MovementType +
				"', Weight = '" + goal.Weight +
				"', Unit = '" + goal.Unit +
				"' WHERE Id =" + goal.Id, db.connection);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
		}

		public static void CreateGoal(Goal goal, DatabaseConnection db)
		{
			var cmd = new SqlCommand("INSERT INTO Goal (Date, Name, Repetition, MovementType, Weight, Unit, IdUser) output INSERTED.ID VALUES('" + 
				goal.Date + "', '" + 
				goal.Name + "', '" + 
				goal.Repetition + "', '" + 
				goal.MovementType + "', '" + 
				goal.Weight + "', '" + 
				goal.Unit + "', '" + 
				goal.IdUser + "')", db.connection);
		
			goal.Id = (int)cmd.ExecuteScalar();

			cmd.Dispose();
		}
	}
}
