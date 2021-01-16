using FinalTest.Interfaces;
using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalTest.Controllers
{
    public class zaposleniController : ApiController
    {
        IEmployeeRepository _repository { get; set; }

        public zaposleniController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            Employee e = _repository.GetById(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        public IEnumerable<Employee> Get(int rodjenje)
        {
            return _repository.GetByBirthYear(rodjenje);
        }

        [Authorize]
        public IHttpActionResult Post(Employee e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(e);
            return CreatedAtRoute("DefaultApi", new { id = e.Id }, e);
        }

        public IHttpActionResult Put(Employee e, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != e.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(e);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(e);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            Employee e = _repository.GetById(id);
            if (e == null)
            {
                return NotFound();
            }
            _repository.Delete(e);
            return Ok();
        }

        [Authorize]
        [Route("api/pretraga")]
        public IEnumerable<Employee> PostPretraga(MinMax minMax)
        {
            return _repository.GetAllSallary(minMax);
        }
    }
}
