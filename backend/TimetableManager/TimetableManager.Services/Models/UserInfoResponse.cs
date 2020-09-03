using System.ComponentModel.DataAnnotations;

namespace TimetableManager.Helpers.Models
{
    public class UserInfoResponse
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
