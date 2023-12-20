using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get api/favoritesUsers Favorites
        [HttpGet, Authorize]
        public IActionResult GetUsersFavorites()
        {
            try
            {
                string userId = User.FindFirstValue("id");

                var favorites = _context.Favorites.Where(f => f.UserId.Equals(userId));

                return StatusCode(200, favorites);
              
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        // POST api/<FavoritesController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Favorite data)
        {
            try
            {
                string userId = User.FindFirstValue("id");
               

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                data.UserId =userId;

                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                _context.Favorites.Add(data);

                _context.SaveChanges();

                return Ok(data);

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }

        }


    }
   
}
