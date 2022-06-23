namespace ArticleManagement.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ImageURL");
        }
    }
}
