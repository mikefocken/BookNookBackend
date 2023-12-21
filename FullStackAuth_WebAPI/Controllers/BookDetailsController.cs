using Azure.Identity;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailsControllers : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailsControllers(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public IActionResult GetBookDetails(string bookId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

             
                var reviews = _context.Reviews
                    .Where(r => r.BookId == bookId)
                    .Select(r => new ReviewWithUserDto
                    {
                        Id = r.Id,
                        BookId = r.BookId,
                        Text = r.Text,
                        Rating = r.Rating,
                        Owner = new UserForDisplayDto
                        {
                            Id = r.User.Id,
                            FirstName = r.User.FirstName,
                            LastName = r.User.LastName,
                            UserName = r.User.UserName,
                        }
                    })
                    .ToList();

                
                if (reviews.Count == 0)
                {
                    return NotFound($"No reviews found for book with ID {bookId}.");
                }

               
                double averageRating = reviews.Average(r => r.Rating);

                bool isFavoritedByCurrentUser = _context.Favorites
                    .Any(f => f.UserId == userId && f.BookId == bookId);

                var bookDetails = new BookDetailsDto
                {
                    Reviews = reviews,
                    AverageRating = averageRating,
                    IsFavorited = isFavoritedByCurrentUser,
                };

                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }




}
