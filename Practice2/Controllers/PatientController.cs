using Microsoft.AspNetCore.Mvc;
using UPB.BusinessLogic.Managers;
using UPB.BusinessLogic.Models;

namespace UPB.Practice2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientManager _patientManager;

        public PatientController(PatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        // GET: api/<PatientController>
        [HttpGet]
        public List<Patient> Get()
        {
            return _patientManager.GetAll();
        }


        // GET api/<PatientController>/5
        [HttpGet]
        [Route("{id}")]
        public Patient Get(int ci)
        {
            throw new NotImplementedException();
        }


        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            _patientManager.CreatePatient(value);
        }


        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int ci, [FromBody] Patient value)
        {
            throw new NotImplementedException();
        }


        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int ci)
        {
            throw new NotImplementedException();
        }
    }
}
