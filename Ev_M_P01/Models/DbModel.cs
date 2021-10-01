using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ev_M_P01.Models
{
    public class DbModel
    {
        public class Trainee
        {
            public int TraineeId { get; set; }
            [Required, StringLength(40), Display(Name = "Trainee Name")]
            public string TraineeName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required, Display(Name = "Admit Date"), Column(TypeName = "date")]
            public DateTime AdmitDate { get; set; }
            [Required, StringLength(400)]
            public string Picture { get; set; }
        }
        public class TraineeDbContext : DbContext
        {
            public TraineeDbContext()
            {
                Database.SetInitializer(new DbInitializer());
            }
            public DbSet<Trainee> Trainees { get; set; }
        }
        public class DbInitializer : DropCreateDatabaseIfModelChanges<TraineeDbContext>
        {
            protected override void Seed(TraineeDbContext context)
            {
                context.Trainees.Add(new Trainee { TraineeName = "Trainee01", Email = "abc@gmail.com", AdmitDate = DateTime.Now.Date, Picture = "no-pic.png" });
                context.SaveChanges();
            }
        }
    }
}