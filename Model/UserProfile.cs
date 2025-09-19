using System.ComponentModel.DataAnnotations;
using System.Net;

namespace remote_poc_webapi.Model
{
    public class UserProfile
    {

        public UserProfile(string Name,String phoneNumber,string address, string emergencyName,string emergencyPhone,string emergencyRelationship)
        {
            this.Name = Name;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.EmergencyContact = emergencyName;
            this.EmergencyContactPhoneNumber = emergencyPhone;
            this.EmergencyContactRelationship = emergencyRelationship;
        }

        public UserProfile()
        {
            this.Name = string.Empty;
            this.PhoneNumber = string.Empty;
            this.Address = string.Empty;
            this.EmergencyContact = string.Empty;
            this.EmergencyContactPhoneNumber = string.Empty;
            this.EmergencyContactRelationship = string.Empty;
        }


        [Key]
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public string Last4Aadhar { get; set; }

        [Required]
        public string EmergencyContact { get; set; }

        [Required]
        public string EmergencyContactPhoneNumber { get; set; }
        
        [Required]
        public string EmergencyContactRelationship { get; set; }
    }
}
