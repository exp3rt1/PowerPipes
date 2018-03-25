using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class Dashboard
    {
        public string Name { get; set; }

        [DisplayName("Max Training Squat")]
        [Range(0.0, Double.MaxValue)]
        public float MaxTrainingSquat { get; set; }

        public string MaxTrainingSquatUnit { get; set; }

        [DisplayName("Max Training Bench")]
        [Range(0.0, Double.MaxValue)]
        public float MaxTrainingBench { get; set; }

        public string MaxTrainingBenchUnit { get; set; }

        [DisplayName("Max Training Deadlift")]
        [Range(0.0, Double.MaxValue)]
        public float MaxTrainingDeadlift { get; set; }

        public string MaxTrainingDeadliftUnit { get; set; }

        [DisplayName("Max Competition Squat")]
        [Range(0.0, Double.MaxValue)]
        public float MaxCompetitionSquat { get; set; }

        public string MaxCompetitionSquatUnit { get; set; }

        [DisplayName("Max Competition Bench")]
        [Range(0.0, Double.MaxValue)]
        public float MaxCompetitionBench { get; set; }

        public string MaxCompetitionBenchUnit { get; set; }

        [DisplayName("Max Competition Deadlift")]
        [Range(0.0, Double.MaxValue)]
        public float MaxCompetitionDeadlift { get; set; }

        public string MaxCompetitionDeadliftUnit { get; set; }

        public List<MovementProgressElement> SquatProgression { get; set; }

        public List<MovementProgressElement> BenchProgression { get; set; }

        public List<MovementProgressElement> DeadliftProgression { get; set; }

        public List<GoalProgress> AccomplishedGoals { get; set; }

        public List<GoalProgress> PendingGoals { get; set; }
    }
}