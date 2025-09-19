using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace remote_poc_webapi.Model
{
    public class DroneLocation
    {
        public int Id { get; set; }

        public int DroneId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? Created { get; set; }

        [NotMapped]
        public double TimeInMinutes { get; set; }

        [NotMapped]
        public double DistanceInKm { get; set; }

        [Required]
        public virtual DeployedDrones  Drone { get; set; }
    }
}
