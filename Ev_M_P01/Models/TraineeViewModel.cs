using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev_M_P01.Models
{
    public class TraineeViewModel
    {
        public int TraineeId { get; set; }
        [Required, StringLength(40), Display(Name = "Trainee Name")]
        public string TraineeName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, Display(Name = "Admit Date")]
        public DateTime AdmitDate { get; set; }

        public string Picture { get; set; }
        [StringLength(15)]
        public string ImageType { get; set; }
    }
}