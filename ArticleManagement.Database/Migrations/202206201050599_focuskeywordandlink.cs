namespace ArticleManagement.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class focuskeywordandlink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "FocusKeyWord", c => c.String());
            AddColumn("dbo.Articles", "KeywordLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "KeywordLink");
            DropColumn("dbo.Articles", "FocusKeyWord");
        }
    }
}
