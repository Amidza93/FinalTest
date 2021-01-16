using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        IEnumerable<Employee> GetByBirthYear(int birthYear);
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        void Delete(Employee employee);
        IEnumerable<Employee> GetAllSallary(MinMax minMax);
    }
}
