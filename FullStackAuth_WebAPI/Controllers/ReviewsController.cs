using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Post: Api/Reviews
        [HttpPost, Authorize]
        public IActionResult Post([FromBody]Review data)
        {
            try
            {     // Retrieve the authenticated user's ID from the JWT token  
                string userId = User.FindFirstValue("id");

                // If the user ID is null or empty, the user is not authenticated, so return a 401 unauthorized response
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();

                }

                // Set the car's owner ID  the authenticated user's ID we found earlier             
                data.UserId=userId;

                // Add the car to the database and save changes
                _context.Reviews.Add(data); ;

                if (!ModelState.IsValid)
                {   // If the car model state is invalid, return a 400 bad request response with the model state errors
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                // Return the newly created car as a 201 created response
                return StatusCode(201, data);


            }
            
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            

        }



    }

        
}

