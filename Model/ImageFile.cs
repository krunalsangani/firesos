namespace remote_poc_webapi.Model
{
    public class VIP_Request
    {
        public int Id { get; set; }
        public string FileLocation { get; set; }
        public string NumberPlate { get; set; }
        public Boolean IsProcessed { get; set; }
        public DateTime Created { get; set; }
    }
}
