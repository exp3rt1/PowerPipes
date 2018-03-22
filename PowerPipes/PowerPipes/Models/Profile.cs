﻿using System;
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
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Un alias est requis")]
        [DisplayName("UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Un age est requis")]
        [DisplayName("Age")]
        [Range(0.0, Double.MaxValue)]
        public int Age { get; set; }

        public int IdUser { get; set; }
    }
}