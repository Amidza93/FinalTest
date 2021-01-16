using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalTest.Interfaces;
using FinalTest.Models;

namespace FinalTest.Controllers
{
    public class jediniceController : ApiController
    {
        IOrganizationalUnitRepository _repository { get; set; }

        public jediniceController(IOrganizationalUnitRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrganizationalUnit> GetOrganizationalUnits()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult GetById (int id)
        {
            OrganizationalUnit unit = _repository.GetById(id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        [Route("api/tradicija")]
        public IEnumerable<OrganizationalUnit> GetTradicija()
        {
            return _repository.GetOldestYoungest();
        }

        [Route("api/brojnost")]
        public IEnumerable<Brojnost> GetBrojnost()
        {
            return _repository.GetBrojnost();
        }

        [Route("api/plate")]
        public IEnumerable<Plate> Post(Boundary boundary)
        {
            return _repository.PostPlate(boundary);
        }




    }
}
