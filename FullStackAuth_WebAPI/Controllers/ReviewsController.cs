using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        // Get api/<ReviewsController>
        [HttpGet]
        public IActionResult GetReviews()
        {
            try
            {
                var cars = _context.Cars.Select(c => new CarWithUserDto
                { 
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    Owner = new UserForDisplayDto
                    {
                        Id = c.Owner.Id,
                        FirstName = c.Owner.FirstName,
                        LastName = c.Owner.LastName,
                        UserName = c.Owner.UserName,
                    }
                }).ToList();

            }



            }









        }

        // POST api/<ReviewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}

