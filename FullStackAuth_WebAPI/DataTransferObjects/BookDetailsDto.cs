using FullStackAuth_WebAPI.DataTransferObjects;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class BookDetailsDto
    {
        public List<ReviewWithUserDto> Reviews { get; set; }
    
        public double AverageRating { get; set; }

        public bool IsFavorited { get; set; }

    }
}


    

