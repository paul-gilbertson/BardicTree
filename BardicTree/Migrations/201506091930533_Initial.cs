namespace BardicTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        NodeID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        BodyText = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NodeID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorUserID)
                .Index(t => t.CreatorUserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 50),
                        Joined = c.DateTime(nullable: false),
                        Description = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DisplayName, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NodeVisits",
                c => new
                    {
                        NodeVisitID = c.Int(nullable: false, identity: true),
                        NodeID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Visit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NodeVisitID)
                .ForeignKey("dbo.Nodes", t => t.NodeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.NodeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NodeChoices",
                c => new
                    {
                        NodeChoiceID = c.Int(nullable: false, identity: true),
                        childNodeID = c.Int(nullable: false),
                        text = c.String(),
                        NodeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NodeChoiceID)
                .ForeignKey("dbo.Nodes", t => t.NodeID, cascadeDelete: true)
                .Index(t => t.NodeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NodeChoices", "NodeID", "dbo.Nodes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeVisits", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.NodeVisits", "NodeID", "dbo.Nodes");
            DropForeignKey("dbo.Nodes", "CreatorUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NodeChoices", new[] { "NodeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.NodeVisits", new[] { "UserID" });
            DropIndex("dbo.NodeVisits", new[] { "NodeID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "DisplayName" });
            DropIndex("dbo.Nodes", new[] { "CreatorUserID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NodeChoices");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.NodeVisits");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Nodes");
        }
    }
}
