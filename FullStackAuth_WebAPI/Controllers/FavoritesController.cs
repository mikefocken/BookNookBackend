using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        // Get api/<FavoritesController>
        [HttpGet]
        public void Get([FromBody] string value)
        {
        }

        // POST api/<FavoritesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}
