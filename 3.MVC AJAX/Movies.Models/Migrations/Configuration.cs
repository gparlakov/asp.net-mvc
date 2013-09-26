namespace Movies.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Movies.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            this.SeedMyDatabase(context);
        }

        private void SeedMyDatabase(ApplicationDbContext context)
        {
            var male = new Sex { Name = "Male" };
            var female =  new Sex { Name = "Female" };
            
            context.Sexes.AddOrUpdate(
                s => s.Id,
                male,
                female
            );

            context.Actors.AddOrUpdate(
                a => a.Id,
                new Actor {Name = "Brad Pitt", Sex = male},
                new Actor {Name = "Angelina Jolie", Sex = female},
                new Actor {Name = "Aston Kutcher", Sex = male},
                new Actor {Name = "Robert de Niro", Sex = male},
                new Actor {Name = "Jennifer Anniston", Sex = female},
                new Actor {Name = "Joddie Foster", Sex = female},
                new Actor {Name = "Julia Roberts", Sex = female},
                new Actor {Name = "Sophia Loren", Sex = female},
                new Actor {Name = "Grace Kelly", Sex = female},
                new Actor { Name = "Nataly Portman", Sex = female},
                new Actor { Name = "Marilyn Monroe", Sex = female},
                new Actor { Name = "Al Pacino", Sex = male}
            );

            context.Movies.AddOrUpdate(
                m => m.Id,
                new Movie
                {
                    Title = "Shawshank Redemption",
                    Year = "1994",
                    LeadingActor = new Actor { Name = "Tim Robbins", Sex = male },
                    Director = new Director { Name = "Frank Darabont", Sex = male }
                },
                new Movie
                {
                    Title = "The Godfather",
                    Year = "1972",
                    LeadingActor = new Actor { Name = "Marlon Brando", Sex = male },
                    Director = new Director { Name = "Francis Ford Coppola", Sex = male }
                },
                new Movie { Title = "The Godfather: Part II", Year = "1974" },
                new Movie
                {
                    Title = "Pulp Fiction",
                    Year = "1994",
                    LeadingActress = new Actor { Name = "Uma Turman", Sex = female },
                    LeadingActor = new Actor { Name = "John Travolta", Sex = male },
                    Director = new Director { Name = "Quentin Tarantino", Sex = male }
                },
                new Movie { Title = "Il buono, il brutto, il cattivo", Year = "1966" },
                new Movie
                {
                    Title = "The Dark Knight",
                    Year = "2008",
                    LeadingActor = new Actor { Name = "Christian Bale", Sex = male },
                    Director = new Director { Name = "Christopher Nolan", Sex = male}

                },
                new Movie { Title = "12 Angry Men", Year = "1957" },
                new Movie { Title = "Schindler's List", Year = "1993" },
                new Movie { Title = "The Lord of the Rings: The Return of the King", Year = "2003" },
                new Movie { Title = "Fight Club", Year = "1999" },
                new Movie { Title = "Star Wars: Episode V - The Empire Strikes Back", Year = "1980" },
                new Movie { Title = "The Lord of the Rings: The Fellowship of the Ring", Year = "2001" },
                new Movie { Title = "One Flew Over the Cuckoo's Nest", Year = "1975" },
                new Movie { Title = "Inception", Year = "2010" },
                new Movie { Title = "Goodfellas", Year = "1990" },
                new Movie { Title = "Star Wars", Year = "1977" },
                new Movie { Title = "Shichinin no samurai", Year = "1954" },
                new Movie { Title = "Forrest Gump", Year = "1994" },
                new Movie { Title = "The Matrix", Year = "1999" },
                new Movie { Title = "The Lord of the Rings: The Two Towers", Year = "2002" }
            );
        }
    }
}
