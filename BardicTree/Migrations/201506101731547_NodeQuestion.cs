namespace BardicTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nodes", "Question", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nodes", "Question");
        }
    }
}
