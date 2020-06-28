using SourceCode.Web.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SourceCode.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }
            var clients = new Client[]
                    {
                        new Client
                        {
                        Name = "Justin Reeves",
                        WebSite = "http://www.google.com",
                        DirectorName = "Myself",
                        EmailAddress = "reeves999999@hotmail.com"
                        },
                        new Client
                        {
                            Name = "Fay Reeves",
                            WebSite = "http://www.apple.com",
                            DirectorName = "Justin",
                            EmailAddress = "fay999999@hotmail.com"
                        }
                    };

            
            foreach (Client e in clients)
            {
                context.Clients.Add(e);
            }
            context.SaveChanges();

            var firstClient = context.Clients.FirstOrDefault();
                
                firstClient.ClientProjects = new List<ClientProject> {
                new ClientProject
                {
                    ClientId = firstClient.Id,
                    Name = "Project 1"
                },
                new ClientProject
                {
                    ClientId = firstClient.Id,
                    Name = "Project 2"
                }
            };

            context.SaveChanges();

        }
    }
}
