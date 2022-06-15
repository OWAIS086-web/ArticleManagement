namespace ArticleManagement.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articleandblogsitetitle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocURL = c.String(),
                        ArticleName = c.String(),
                        Note = c.String(),
                        PostingDate = c.DateTime(nullable: false),
                        BlogSiteTitle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BlogTitles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlogTitles");
            DropTable("dbo.Articles");
        }
    }
}
