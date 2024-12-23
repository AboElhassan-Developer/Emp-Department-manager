using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "ConFirm Password")]
        [DataType(DataType.Password)]
        public string ConFirmPassword { get; set; }

        public string Address { get; set; }

    }
}
