using System.ComponentModel.DataAnnotations;

namespace remote_poc_webapi.Model
{
    public class RequestLog
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? HelpText { get; set; }
        public DateTime? Created { get; set; }

        public UserProfile UserProfile { get; set; }    

    }

    // DTO for input data
    public class ItemDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? HelpText { get; set; }

        public int UserId { get; set; }
    }

    // Entity class for EF Core
}
