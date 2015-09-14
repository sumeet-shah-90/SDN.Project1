namespace Project1.API.Migrations
{
    using Project1.Data.Enumerations;
    using Project1.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project1.API.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Project1.API.DAL.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            ApplicationUser user = new ApplicationUser()
            {
                Name = "Sumeet Shah",
                Password = "test123456",
                PhoneNumber = "+919630789790",
                SecurityHash = string.Empty,
                JoinTS = DateTime.Now.AddYears(-3),
                IsPhoneConfirmed = true,
                Active = true
            };

            context.ApplicationUsers.Add(user);

            context.SaveChanges();

            context.Documents.AddRange(new List<Document>{
                new Document
                {
                    UserID = user.UserID,
                    FileName = user.UserID + "\\Sample.txt",
                    FileType = FileType.TXT,
                    UploadedTS = DateTime.Now,
                    LastUpdatedTS = DateTime.Now,
                    IsActive = true
                },
                new Document
                {
                    UserID = user.UserID,
                    FileName = user.UserID + "\\AnotherSample.txt",
                    FileType = FileType.TXT,
                    UploadedTS = DateTime.Now,
                    LastUpdatedTS = DateTime.Now,
                    IsActive = true
                }
            });

            context.SaveChanges();
        }
    }
}
