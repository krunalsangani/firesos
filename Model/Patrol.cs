using System.ComponentModel;

namespace remote_poc_webapi.Model
{
    public class Patrol
    {
        public int id { get; set; }
        public string? PatrolNumber { get; set; }
        public string? PatrolCallSign { get; set; }
        public int PatrolTypeId { get; set; }
        public string? MobileNumber { get; set; }
        public Boolean IsCurrentlyPatrolling { get; set; }

        public string? PatrollingNotes { get; set; }

        public virtual PatrolType PatrolType { get; set; }
        
    }

    public class PatrolType
    {
        public int id { get; set; }
        public string PatrolTypeName { get; set; }

        public Boolean IsActive { get; set; }
        
        public string? TravelMode { get; set; }
    }
}
    