using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class GoalProgress
    {
        public Goal goal { get; set; }

        public Exercise PerformedExercise { get; set; }

        public String ExerciseName { get; set; }

        [DisplayName("Écart")]
        [Range(0.0, Double.MaxValue)]
        public float Difference { get; set; }

        public DateTime AccomplishedDate { get; set; }
    }
}