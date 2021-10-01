namespace Ev_M_P01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static Ev_M_P01.Models.DbModel;

    internal sealed class Configuration : DbMigrationsConfiguration<Ev_M_P01.Models.DbModel.TraineeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ev_M_P01.Models.DbModel.TraineeDbContext context)
        {
            context.Trainees.Add(new Trainee { TraineeName = "Trainee01", Email = "abc@gmail.com", AdmitDate = DateTime.Now.Date, Picture = "no-pic.png" });
            context.SaveChanges();
        }
    }
}
