using System.ComponentModel.DataAnnotations.Schema;

namespace remote_poc_webapi.Model
{
    // Entity class for EF Core
    public class DeployedDrones
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? StationAddress { get; set; }
        public string? DroneName { get; set; }
        public string? DroneNumber { get; set; }

        [NotMapped]
        public double DistanceInKms { get; set; }

        [NotMapped]
        public double TimeInMinutes { get; set; }
        public DateTime? Created { get; set; }
    }

    // DTO for input data
    public class ItemDrone
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? StationAddress { get; set; }
        public string? DroneName { get; set; }
        public string? DroneNumber { get; set; }

    }
}
