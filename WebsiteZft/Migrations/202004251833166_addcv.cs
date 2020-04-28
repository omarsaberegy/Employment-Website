namespace WebsiteZft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyforJobs", "CV", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyforJobs", "CV");
        }
    }
}
