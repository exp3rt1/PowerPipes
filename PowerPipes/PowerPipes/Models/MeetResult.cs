using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
	public class MeetResult
	{
		public int Id { get; set; }

		public int IdMeet { get; set; }

		public string Name { get; set; }

		[Required(ErrorMessage = "Un mouvement est requis")]
		[DisplayName("Mouvement")]
		public int MovementType { get; set; }

		[Required(ErrorMessage = "Un poids est requis")]
		[DisplayName("Poids")]
		public float Weight { get; set; }

		[Required]
		[DisplayName("Réussi")]
		public bool Success { get; set; }
	}
}