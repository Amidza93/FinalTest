using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest.Interfaces
{
    public interface IOrganizationalUnitRepository
    {
        IEnumerable<OrganizationalUnit> GetAll();
        OrganizationalUnit GetById(int id);
        IEnumerable<OrganizationalUnit> GetOldestYoungest();
        IEnumerable<Brojnost> GetBrojnost();
        IEnumerable<Plate> PostPlate(Boundary boundary);
    }
}
