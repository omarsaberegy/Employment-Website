namespace WebsiteZft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserImage");
        }
    }
}