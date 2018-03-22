using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
	public class MeetHeader
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Une date est requise")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Un nom est requis")]
		[DisplayName("Compétition")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Un poids est requis")]
		[DisplayName("Poids (kg)")]
		public float PersonalWeight { get; set; }

		public int IdUser { get; set; }
	}
}