namespace Trabalho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facebookToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "facebook_token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "facebook_token");
        }
    }
}
