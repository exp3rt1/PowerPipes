using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public int IdTraining { get; set; }

        public int MovementType { get; set; }

        public int Repetition { get; set; }

        public int FailedRepetition { get; set; }

        public int Weight { get; set; }

        public string Unit { get; set; }
    }
}