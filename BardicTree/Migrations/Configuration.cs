namespace BardicTree.Migrations
{
    using BardicTree.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BardicTree.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BardicTree.Models.ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(p => p.UserName,
                 new ApplicationUser { UserName = "SystemUser", Email = "system@localhost", DisplayName = "God", Joined = DateTime.Now });

            context.SaveChanges();

            context.Nodes.AddOrUpdate(p => p.NodeID
                , new Node { NodeID = 1, Title = "In the Beginning...", BodyText = "The formless void. Speak your word of creation!", Question = "To which story do you wish to travel?", CreationDate = DateTime.Now, CreatorUserID = context.Users.First(u => u.UserName == "SystemUser").Id
                });

            context.SaveChanges();
        }
    }
}
