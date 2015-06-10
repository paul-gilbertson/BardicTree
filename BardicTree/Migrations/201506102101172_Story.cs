namespace BardicTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Story : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        CreationDate = c.DateTime(nullable: false),
                        OwnerUserID = c.String(nullable: false, maxLength: 128),
                        RootNodeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerUserID, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.RootNodeID, cascadeDelete: true)
                .Index(t => t.Title, unique: true, name: "UIX_Story_title")
                .Index(t => t.OwnerUserID)
                .Index(t => t.RootNodeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories", "RootNodeID", "dbo.Nodes");
            DropForeignKey("dbo.Stories", "OwnerUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Stories", new[] { "RootNodeID" });
            DropIndex("dbo.Stories", new[] { "OwnerUserID" });
            DropIndex("dbo.Stories", "UIX_Story_title");
            DropTable("dbo.Stories");
        }
    }
}
