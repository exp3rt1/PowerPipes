using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class MovementProgressElement
    {
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Poids")]
        public float Weight { get; set; }
    }
}