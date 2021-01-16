using FinalTest.Interfaces;
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalTest.Repositories
{
    public class OrganizationalUnitRepo : IDisposable, IOrganizationalUnitRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<OrganizationalUnit> GetAll()
        {
            return db.OrganizationalUnits;
        }

        public OrganizationalUnit GetById(int id)
        {
            return db.OrganizationalUnits.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<OrganizationalUnit> GetOldestYoungest()
        {
            List<OrganizationalUnit> list = new List<OrganizationalUnit>();

            OrganizationalUnit oldest = db.OrganizationalUnits.OrderBy(x => x.EstablishmentYear).First();
            OrganizationalUnit youngest = db.OrganizationalUnits.OrderByDescending(x => x.EstablishmentYear).First();

            list.Add(oldest);
            list.Add(youngest);

            return list.OrderBy(x => x.EstablishmentYear);
        }

        public IEnumerable<Brojnost> GetBrojnost()
        {
            List<Brojnost> list = new List<Brojnost>();

            IEnumerable<OrganizationalUnit> orgUnits = db.OrganizationalUnits;
            IEnumerable<Employee> employees = db.Employees.Include(x => x.OrganizationalUnit);

            foreach (OrganizationalUnit unit in orgUnits)
            {
                Brojnost b = new Brojnost();
                b.OrganizationalUnit = unit;
                int counter = 0;
                foreach (Employee e in employees)
                {
                    if (e.OrganizationalUnitId == unit.Id)
                    {
                        counter++;
                    }
                    b.NumberOfEmployees = counter;
                }
                list.Add(b);
            }
            return list.OrderByDescending(x=>x.NumberOfEmployees);
        }

        public IEnumerable<Plate> PostPlate(Boundary boundary)
        {
            List<Plate> list = new List<Plate>();

            IEnumerable<OrganizationalUnit> orgUnits = db.OrganizationalUnits;
            IEnumerable<Employee> employees = db.Employees.Include(x => x.OrganizationalUnit);

            foreach (OrganizationalUnit unit in orgUnits)
            {
                Plate p = new Plate();
                p.OrganizationalUnit = unit;
                int counter = 0;
                decimal total = 0;

                foreach (Employee e in employees)
                {
                    if (e.OrganizationalUnitId == unit.Id)
                    {
                        counter++;
                        total += e.Sallary;
                    }

                    if (counter != 0)
                    {
                        p.AvgSallary = total / counter;
                    }
                    else
                    {
                        p.AvgSallary = total;
                    }

                }
                list.Add(p);
            }
            return list.Where(x => x.AvgSallary > boundary.granica).OrderBy(x => x.AvgSallary);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}