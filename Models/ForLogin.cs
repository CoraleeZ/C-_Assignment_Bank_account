using System.ComponentModel.DataAnnotations;

namespace Bank_Accounts.Models
{
    public class ForL
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}

    }

}