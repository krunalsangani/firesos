namespace remote_poc_webapi.Model
{
    public class LoginModel
    {
        public string PhoneNumber { get; set; }
        public string Last4Aadhar { get; set; }

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Venue { get; set; }   
    }
}
