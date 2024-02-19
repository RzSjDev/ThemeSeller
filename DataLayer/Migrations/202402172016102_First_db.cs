namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageComments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PageID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 200),
                        WebSite = c.String(maxLength: 200),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        Page_ThemeID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Themes", t => t.Page_ThemeID)
                .Index(t => t.Page_ThemeID);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        ThemeID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        ThemeTitle = c.String(nullable: false, maxLength: 250),
                        ShortDescription = c.String(nullable: false, maxLength: 350),
                        Text = c.String(nullable: false),
                        Visit = c.Int(nullable: false),
                        ImageName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Tags = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThemeID)
                .ForeignKey("dbo.ThemeGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.ThemeGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false, maxLength: 150),
                        ImageTitleGroup = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Themes", "GroupID", "dbo.ThemeGroups");
            DropForeignKey("dbo.PageComments", "Page_ThemeID", "dbo.Themes");
            DropIndex("dbo.Themes", new[] { "GroupID" });
            DropIndex("dbo.PageComments", new[] { "Page_ThemeID" });
            DropTable("dbo.ThemeGroups");
            DropTable("dbo.Themes");
            DropTable("dbo.PageComments");
        }
    }
}
