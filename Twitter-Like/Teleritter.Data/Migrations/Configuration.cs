namespace Teleritter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Teleritter.Models;

    public class Configuration : DbMigrationsConfiguration<Teleritter.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Teleritter.Data.ApplicationDbContext context)
        {
            if (context.Users.Count() > 0)
            {
                return;
            }

            var userRole = SeedRoles(context);

            var rand = new Random();

            var tags = GetTags(rand);

            SeedUsers(rand, tags, userRole, context);

            context.SaveChanges();
        }
  
        private void SeedUsers(Random rand, List<Tag> tags, Role userRole, ApplicationDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var user = new ApplicationUser { UserName = "User" + i };

                var tweetsCount = rand.Next(15);
                for (int k = 0; k < tweetsCount; k++)
                {
                    var bytes = new byte[rand.Next(10, 100)];
                    rand.NextBytes(bytes);
                    var tweet = new Telereet 
                    { 
                        Text = "Tweet" + k + Convert.ToBase64String(bytes).ToLower(),
                        PostedOn = DateTime.Now.AddDays(-rand.Next(20))
                    };

                    var tagsLen = rand.Next(10);
                    for (int j = 0; j < tagsLen; j++)
                    {
                        tweet.Tags.Add(tags[rand.Next(10)]);
                    }
                    user.Tweets.Add(tweet);
                }

                context.UserRoles.Add(new Microsoft.AspNet.Identity.EntityFramework.UserRole 
                { 
                    User = user,
                    Role = userRole 
                });

                context.Users.Add(user);
            }
        }
  
        private Role SeedRoles(ApplicationDbContext context)
        {
            var userRole = new Microsoft.AspNet.Identity.EntityFramework.Role { Name = "User" };

            context.Roles.Add(userRole);
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.Role { Name = "Administrator" });
            return userRole;
        }
  
        private List<Tag> GetTags(Random rand)
        {
            var tags = new List<Tag>();

            for (int i = 0; i < 10; i++)
            {
                var length = rand.Next(3);

                var bytes = new byte[length];
                rand.NextBytes(bytes); 

                tags.Add(new Tag { Name = "tag " + Convert.ToBase64String(bytes).ToLower() });
            }
            return tags;
        }
    }
}
