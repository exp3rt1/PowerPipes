using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class Training
    {
        public TrainingHeader Header { get; set; } = new TrainingHeader();

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}