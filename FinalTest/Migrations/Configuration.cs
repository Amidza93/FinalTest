namespace FinalTest.Migrations
{
    using FinalTest.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalTest.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.OrganizationalUnits.AddOrUpdate(x => x.Id,
           new OrganizationalUnit() { Id = 1, Name = "Administracija", EstablishmentYear = 2010 },
           new OrganizationalUnit() { Id = 2, Name = "Racunovodstvo", EstablishmentYear = 2012 },
           new OrganizationalUnit() { Id = 3, Name = "Razvoj", EstablishmentYear = 2013 }
           );

            context.Employees.AddOrUpdate(x => x.Id,
          new Employee() { Id = 1, Name = "Pera Peric", Role = "Direktor", BirthYear = 1980, EmploymentYear = 2010, Sallary = 3000m, OrganizationalUnitId = 1 },
          new Employee() { Id = 2, Name = "Mika Mikic", Role = "Sekretar", BirthYear = 1985, EmploymentYear = 2011, Sallary = 1000m, OrganizationalUnitId = 1 },
          new Employee() { Id = 3, Name = "Iva Ivic", Role = "Racunovodja", BirthYear = 1981, EmploymentYear = 2012, Sallary = 2000m, OrganizationalUnitId = 2 },
          new Employee() { Id = 4, Name = "Zika Zikic", Role = "Inzenjer", BirthYear = 1982, EmploymentYear = 2013, Sallary = 2500m, OrganizationalUnitId = 3 },
          new Employee() { Id = 5, Name = "Ana Anic", Role = "Inzenjer", BirthYear = 1984, EmploymentYear = 2014, Sallary = 2500m, OrganizationalUnitId = 3 }
          );
        }
    }
}
