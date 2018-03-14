using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PowerPipes.Models
{
	public class Goal
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Une date est requise")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Un nom est requis")]
		[DisplayName("Nom")]
		public string Name { get; set; }

		[Required]
		public int MovementType { get; set; }

		[Range(0.0, Double.MaxValue)]
		public int Repetition { get; set; }

		[Range(0.0, Double.MaxValue)]
		public int Weight { get; set; }

		public string Unit { get; set; }

		public int IdUser { get; set; }
	}
}