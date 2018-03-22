using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public int IdTraining { get; set; }

        [Required]
        public int MovementType { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int Repetition { get; set; }

        [Range(0.0, Double.MaxValue)]
        public int FailedRepetition { get; set; }

        [Range(0.0, Double.MaxValue)]
        public float Weight { get; set; }

        public string Unit { get; set; }
    }
}