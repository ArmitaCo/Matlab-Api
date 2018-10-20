namespace Matlab.DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Matlab.DataModel.MatlabDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Matlab.DataModel.MatlabDb";
        }

        protected override void Seed(Matlab.DataModel.MatlabDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.AvatarImages.AddOrUpdate(x=>x.Name,new []
            {
                new AvatarImage(){Name = "adam.png"},
                new AvatarImage(){Name = "anjali.png"},
                new AvatarImage(){Name = "arjun.png"},
                new AvatarImage(){Name = "jorge.png"},
                new AvatarImage(){Name = "maya.png"},
                new AvatarImage(){Name = "rahul.png"},
                new AvatarImage(){Name = "sadona.png"},
                new AvatarImage(){Name = "sandy.png"},
                new AvatarImage(){Name = "sid.png"},
                new AvatarImage(){Name = "steve.png"},

            });

        }
    }
}
