namespace Ev_M_P01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeId = c.Int(nullable: false, identity: true),
                        TraineeName = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false),
                        AdmitDate = c.DateTime(nullable: false, storeType: "date"),
                        Picture = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.TraineeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainees");
        }
    }
}
