using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PowerPipes.Models
{
    public class Profile
    {
        [Required(ErrorMessage = "Un nom est requis")]
        [DisplayName("Nom")]
        public string Name { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int Age { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxTrainingSquat { get; set; }

        public string MaxTrainingSquatUnit { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxTrainingBench { get; set; }

        public string MaxTrainingBenchUnit { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxTrainingDeadlift { get; set; }

        public string MaxTrainingDeadliftUnit { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxCompetitionSquat { get; set; }

        public string MaxCompetitionSquatUnit { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxCompetitionBench { get; set; }

        public string MaxCompetitionBenchUnit { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int MaxCompetitionDeadlift { get; set; }

        public string MaxCompetitionDeadliftUnit { get; set; }

        public int IdUser { get; set; }
    }
}