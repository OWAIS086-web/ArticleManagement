namespace ArticleManagement.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Status");
        }
    }
}
