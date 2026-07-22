using System.ComponentModel.DataAnnotations;

namespace Shared_DTOs_.IdentityDTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    
}
}
