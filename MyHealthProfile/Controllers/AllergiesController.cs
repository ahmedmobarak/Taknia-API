using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MyHealthProfile.Models;
using MyHealthProfile.Repositories.Allergies;

namespace MyHealthProfile.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        public IAllergyService _allergyService;
        public AllergiesController(IAllergyService allergyService) 
        {
            _allergyService = allergyService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            const string header = "Accept-Language";
            Request.Headers.TryGetValue(header, out StringValues headerValue);
           var list = await _allergyService.AllergiesList(headerValue!);

            return Ok(list);
        }
    }
}
