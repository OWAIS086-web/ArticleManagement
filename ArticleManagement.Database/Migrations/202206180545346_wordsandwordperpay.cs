namespace ArticleManagement.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wordsandwordperpay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Words", c => c.Single(nullable: false));
            AddColumn("dbo.Articles", "PayPerWord", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "PayPerWord");
            DropColumn("dbo.Articles", "Words");
        }
    }
}
