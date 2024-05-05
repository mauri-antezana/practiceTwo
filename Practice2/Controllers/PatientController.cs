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
        [Route("{ci}")]
        public Patient Get(int ci)
        {
            Task<Patient> patientFoundTask = _patientManager.GetPatientByCi(ci);
            return patientFoundTask.Result;
        }


        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {
            _patientManager.CreatePatient(value);
        }


        // PUT api/<PatientController>/5
        [HttpPut("{ci}")]
        public void Put(int ci, [FromBody] Patient value)
        {
            _patientManager.UpdatePatient(ci, value);
        }


        // DELETE api/<PatientController>/5
        [HttpDelete("{ci}")]
        public void Delete(int ci)
        {
            _patientManager.DeletePatient(ci);
        }
    }
}
