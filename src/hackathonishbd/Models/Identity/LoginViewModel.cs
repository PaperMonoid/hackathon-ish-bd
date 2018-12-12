using System.ComponentModel.DataAnnotations;

namespace hackathonishbd.Models.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}