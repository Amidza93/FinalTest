using FinalTest.Interfaces;
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FinalTest.Repositories
{
    public class EmployeeRepo : IDisposable, IEmployeeRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Employee Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return db.Employees.Include(x => x.OrganizationalUnit).FirstOrDefault(x => x.Id == employee.Id);

        }

        public void Delete(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees.Include(x => x.OrganizationalUnit).OrderBy(x => x.EmploymentYear);
        }

        public IEnumerable<Employee> GetAllSallary(MinMax minMax)
        {
            if (minMax.Najmanje == null && minMax.Najvise !=null)
            {
                return db.Employees.Include(x => x.OrganizationalUnit).Where(x => x.Sallary <= minMax.Najvise).OrderByDescending(x => x.Sallary);
            }

            if (minMax.Najmanje != null && minMax.Najvise == null)
            {
                return db.Employees.Include(x => x.OrganizationalUnit).Where(x => x.Sallary >= minMax.Najmanje).OrderByDescending(x => x.Sallary);
            }
            return db.Employees.Include(x => x.OrganizationalUnit).Where(x => x.Sallary >= minMax.Najmanje && x.Sallary <= minMax.Najvise).OrderByDescending(x => x.Sallary);
        }

        public IEnumerable<Employee> GetByBirthYear(int birthYear)
        {
            return db.Employees.Include(x => x.OrganizationalUnit).Where(x => x.BirthYear > birthYear).OrderBy(x => x.BirthYear);
        }

        public Employee GetById(int id)
        {
            return db.Employees.Include(x => x.OrganizationalUnit).FirstOrDefault(x => x.Id == id);
        }

        public Employee Update(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return db.Employees.Include(x => x.OrganizationalUnit).FirstOrDefault(x => x.Id == employee.Id);
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