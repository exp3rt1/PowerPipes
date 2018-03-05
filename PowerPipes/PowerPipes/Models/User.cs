using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace PowerPipes.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Un nom d'utilisateur est requis")]
        [Display(Name = "Utilisateur")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Un mot de passe est requis")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe")]
        public string ConfirmPassword { get; set; }
    }
}