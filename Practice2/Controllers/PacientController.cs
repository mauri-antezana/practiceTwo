using Microsoft.AspNetCore.Mvc;
using UPB.BusinessLogic.Models;

namespace UPB.Practice2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacientController : ControllerBase
    {
        public PacientController()
        {
        }

        // GET: api/<PacientController>
        [HttpGet]
        public List<Pacient> Get()
        {
            throw new NotImplementedException();
        }


        // GET api/<PacientController>/5
        [HttpGet]
        [Route("{id}")]
        public Pacient Get(int ci)
        {
            throw new NotImplementedException();
        }


        // POST api/<PacientController>
        [HttpPost]
        public void Post([FromBody] Pacient value)
        {
            throw new NotImplementedException();
        }


        // PUT api/<PacientController>/5
        [HttpPut("{id}")]
        public void Put(int ci, [FromBody] Pacient value)
        {
            throw new NotImplementedException();
        }


        // DELETE api/<PacientController>/5
        [HttpDelete("{id}")]
        public void Delete(int ci)
        {
            throw new NotImplementedException();
        }
    }
}
