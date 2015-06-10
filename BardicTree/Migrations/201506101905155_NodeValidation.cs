namespace BardicTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeValidation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.NodeChoices", new[] { "NodeID" });
            AlterColumn("dbo.Nodes", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Nodes", "BodyText", c => c.String(nullable: false));
            AlterColumn("dbo.Nodes", "Question", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.NodeChoices", "text", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.NodeChoices", new[] { "text", "NodeID" }, unique: true, name: "UIX_NodeChoice_node_text");
        }
        
        public override void Down()
        {
            DropIndex("dbo.NodeChoices", "UIX_NodeChoice_node_text");
            AlterColumn("dbo.NodeChoices", "text", c => c.String());
            AlterColumn("dbo.Nodes", "Question", c => c.String());
            AlterColumn("dbo.Nodes", "BodyText", c => c.String());
            AlterColumn("dbo.Nodes", "Title", c => c.String());
            CreateIndex("dbo.NodeChoices", "NodeID");
        }
    }
}
