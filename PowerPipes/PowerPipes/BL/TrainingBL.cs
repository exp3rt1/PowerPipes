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

        public static String GetMovementName(int MovementType, DatabaseConnection db)
        {
            var movementName = "";

            var cmd = new SqlCommand("SELECT * FROM MovementType WHERE Id = " + MovementType, db.connection);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                movementName = reader["Name"].ToString();

                reader.Dispose();
                cmd.Dispose();
            }

            return movementName;
        }

        public static Exercise GetMaxForReps(int MovementType, int reps, DatabaseConnection db)
        {
            var cmd = new SqlCommand("SELECT MAX(Weight) AS MaxWeight FROM Exercise WHERE MovementType =" + MovementType +" AND Repetition ="+reps, db.connection);
            var maxWeight = 0.0f;

            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    maxWeight = (float)reader["MaxWeight"];
                }
            }
            reader.Dispose();
            cmd.Dispose();

            cmd = new SqlCommand("SELECT MAX(Repetition) AS MaxRep FROM Exercise WHERE MovementType =" + MovementType + " AND Weight=" + maxWeight, db.connection);
            reader = cmd.ExecuteReader();
            int maxRep = 0;
            if(reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    maxRep = (int)reader["MaxRep"];
                }
            }
            reader.Dispose();
            cmd.Dispose();

            cmd = new SqlCommand("SELECT * FROM Exercise WHERE MovementType =" + MovementType + " AND Repetition =" + maxRep + " AND Weight ="+maxWeight, db.connection);
            Exercise max = new Exercise();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                max.Id = (int)reader["Id"];
                max.IdTraining = (int)reader["IdTraining"];
                max.Repetition = (int)reader["Repetition"];
                max.FailedRepetition = (int)reader["FailedRepetition"];
                max.MovementType = (int)reader["MovementType"];
                max.Weight = (float)reader["Weight"];
                max.Unit = reader["Unit"].ToString();
            }

            else
            {
                max.Id = 0;
                max.IdTraining = 0;
                max.Repetition = 0;
                max.FailedRepetition = 0;
                max.MovementType = MovementType;
                max.Weight = 0.0f;
                max.Unit = "kg";
            }

            reader.Dispose();
            cmd.Dispose();

            return max;
        }

        public static List<TrainingHeader> GetTrainings(int idUser, DatabaseConnection db)
        {
            return GetTrainingsWithOrder(idUser, false, db);
        }

        public static List<TrainingHeader> GetTrainingsWithOrder(int idUser, bool ascending, DatabaseConnection db)
        {
            var trainingList = new List<TrainingHeader>();

            SqlCommand cmd;
            if(ascending)
            {
                cmd = new SqlCommand("SELECT * FROM Training WHERE IdUser =" + idUser + "ORDER BY Date ASC", db.connection);
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM Training WHERE IdUser =" + idUser + "ORDER BY Date DESC", db.connection);
            }

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

        public static List<MovementProgressElement> GetSquatProgression(int idUser, DatabaseConnection db)
        {
            var exerciseList = new List<MovementProgressElement>();

            var bestSquat = 0.0f;

            foreach(var training in GetTrainingsWithOrder(idUser, true, db))
            {
                var cmd = new SqlCommand("SELECT * FROM Exercise WHERE IdTraining =" + training.Id, db.connection);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var exercise = new Exercise
                        {
                            Id = (int)reader["Id"],
                            IdTraining = (int)reader["IdTraining"],
                            MovementType = (int)reader["MovementType"],
                            Repetition = (int)reader["Repetition"],
                            FailedRepetition = (int)reader["FailedRepetition"],
                            Weight = (float)reader["Weight"],
                            Unit = reader["Unit"].ToString()
                        };

                        if(exercise.MovementType == 1 && exercise.MovementType > 0 && exercise.Weight > bestSquat)
                        {
                            exerciseList.Add(new MovementProgressElement
                            {
                                Date = training.Date,
                                Weight = exercise.Weight
                            });

                            bestSquat = exercise.Weight;
                        }
                    }
                }

                reader.Dispose();
                cmd.Dispose();
            }
            return exerciseList;
        }

        public static List<MovementProgressElement> GetBenchProgression(int idUser, DatabaseConnection db)
        {
            var exerciseList = new List<MovementProgressElement>();

            var bestBench = 0.0f;

            foreach (var training in GetTrainingsWithOrder(idUser, true, db))
            {
                var cmd = new SqlCommand("SELECT * FROM Exercise WHERE IdTraining =" + training.Id, db.connection);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var exercise = new Exercise
                        {
                            Id = (int)reader["Id"],
                            IdTraining = (int)reader["IdTraining"],
                            MovementType = (int)reader["MovementType"],
                            Repetition = (int)reader["Repetition"],
                            FailedRepetition = (int)reader["FailedRepetition"],
                            Weight = (float)reader["Weight"],
                            Unit = reader["Unit"].ToString()
                        };

                        if (exercise.MovementType == 2 && exercise.MovementType > 0 && exercise.Weight > bestBench)
                        {
                            exerciseList.Add(new MovementProgressElement {
                                Date = training.Date,
                                Weight = exercise.Weight
                            });
                            bestBench = exercise.Weight;
                        }
                    }
                }

                reader.Dispose();
                cmd.Dispose();
            }
            return exerciseList;
        }

        public static List<MovementProgressElement> GetDeadliftProgression(int idUser, DatabaseConnection db)
        {
            var exerciseList = new List<MovementProgressElement>();

            var bestDeadlift = 0.0f;

            foreach (var training in GetTrainingsWithOrder(idUser, true, db))
            {
                var cmd = new SqlCommand("SELECT * FROM Exercise WHERE IdTraining =" + training.Id, db.connection);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var exercise = new Exercise
                        {
                            Id = (int)reader["Id"],
                            IdTraining = (int)reader["IdTraining"],
                            MovementType = (int)reader["MovementType"],
                            Repetition = (int)reader["Repetition"],
                            FailedRepetition = (int)reader["FailedRepetition"],
                            Weight = (float)reader["Weight"],
                            Unit = reader["Unit"].ToString()
                        };

                        if (exercise.MovementType == 3 && exercise.MovementType > 0 && exercise.Weight > bestDeadlift)
                        {
                            exerciseList.Add(new MovementProgressElement {
                                Date = training.Date,
                                Weight = exercise.Weight
                            });
                            bestDeadlift = exercise.Weight;
                        }
                    }
                }

                reader.Dispose();
                cmd.Dispose();
            }
            return exerciseList;
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
                        Weight = (float)reader["Weight"],
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