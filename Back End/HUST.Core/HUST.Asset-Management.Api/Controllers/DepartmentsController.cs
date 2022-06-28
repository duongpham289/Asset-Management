using HUST.Core.Interfaces.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUST.Asset_Management.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IConfiguration _configuration;
        IDepartmentRepository _departmentRepository;

        public DepartmentsController(IConfiguration configuration, IDepartmentRepository departmentRepository)
        {
            _configuration = configuration;
            _departmentRepository = departmentRepository;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentRepository.Get();
            return Ok(departments);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}



