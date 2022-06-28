using HUST.Core.Interfaces.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUST.Asset_Management.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoriesController : ControllerBase
    {
        IConfiguration _configuration;
        IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        public FixedAssetCategoriesController(IConfiguration configuration, IFixedAssetCategoryRepository fixedAssetCategoryRepository)
        {
            _configuration = configuration;
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
        }

        // GET: api/<FixedAssetCategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var fixedAssetCategorys = _fixedAssetCategoryRepository.Get();
            return Ok(fixedAssetCategorys);
        }

        // GET api/<FixedAssetCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FixedAssetCategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FixedAssetCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FixedAssetCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}



