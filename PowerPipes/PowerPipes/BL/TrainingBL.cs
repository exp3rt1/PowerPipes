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
    public static class TrainingBL
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

        public static List<TrainingHeader> GetTrainings(int idUser, DatabaseConnection db)
        {
            var trainingList = new List<TrainingHeader>();

            var cmd = new SqlCommand("SELECT * FROM Training WHERE IdUser =" + idUser + "ORDER BY Date", db.connection);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    trainingList.Add(new TrainingHeader
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Date = (DateTime)reader["Date"]
                    });
                }
            }

            reader.Dispose();
            cmd.Dispose();

            return trainingList;
        }

        public static TrainingHeader GetHeader(int idTraining, DatabaseConnection db)
        {
            var header = new TrainingHeader();

            var cmd = new SqlCommand("SELECT * FROM Training WHERE Id =" + idTraining, db.connection);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    header.Id = (int)reader["Id"];
                    header.Name = reader["Name"].ToString();
                    header.Date = (DateTime)reader["Date"];
                    header.IdUser = (int)reader["IdUser"];
                }
            }

            reader.Dispose();
            cmd.Dispose();

            return header;
        }

        public static List<Exercise> GetExercices(int idTraining, DatabaseConnection db)
        {
            var exercices = new List<Exercise>();

            var cmd = new SqlCommand("SELECT * FROM Exercise WHERE IdTraining =" + idTraining, db.connection);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    exercices.Add(new Exercise
                    {
                        Id = (int)reader["Id"],
                        IdTraining = idTraining,
                        Repetition = (int)reader["Repetition"],
                        FailedRepetition = (int)reader["FailedRepetition"],
                        MovementType = (int)reader["MovementType"],
                        Weight = (int)reader["Weight"],
                        Unit = reader["Unit"].ToString()
                    });
                }
            }

            reader.Dispose();
            cmd.Dispose();

            return exercices;
        }

        public static void DeleteTraining(int idTraining, DatabaseConnection db)
        {
            var cmd = new SqlCommand("DELETE FROM Exercise WHERE IdTraining = " + idTraining, db.connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand("DELETE FROM Training WHERE Id = " + idTraining, db.connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void UpdateTraining(Training training, DatabaseConnection db)
        {
            var cmd = new SqlCommand("UPDATE Training SET Date = '" + training.Header.Date + "', Name = '" + training.Header.Name + "' WHERE Id =" + training.Header.Id, db.connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand("DELETE FROM Exercise WHERE IdTraining = " + training.Header.Id, db.connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            foreach (var exercise in training.Exercises)
            {
                cmd = new SqlCommand("INSERT INTO Exercise (IdTraining, MovementType, Repetition, FailedRepetition, Weight, Unit) " +
                    "VALUES("+ training.Header.Id +", " + exercise.MovementType + ", " + exercise.Repetition + ", " + exercise.FailedRepetition + ", " + exercise.Weight+ ", '" + exercise.Unit+ "')", db.connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public static void CreateTraining(Training training, DatabaseConnection db)
        {
            var cmd = new SqlCommand("INSERT INTO Training (Date, Name, IdUser) output INSERTED.ID VALUES('" + training.Header.Date+ "', '" + training.Header.Name + "', '" + training.Header.IdUser + "')", db.connection);
            training.Header.Id = (int)cmd.ExecuteScalar();
            cmd.Dispose();

            foreach (var exercise in training.Exercises)
            {
                cmd = new SqlCommand("INSERT INTO Exercise (IdTraining, MovementType, Repetition, FailedRepetition, Weight, Unit) " +
                    "VALUES(" + training.Header.Id + ", " + exercise.MovementType + ", " + exercise.Repetition + ", " + exercise.FailedRepetition + ", " + exercise.Weight + ", '" + exercise.Unit + "')", db.connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
    }
}