using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "First Name is Required ")]
       
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email is Required ")]
        [EmailAddress(ErrorMessage = "Invalid Email")]

        public String Email { get; set; }
        [Required(ErrorMessage ="PassWord is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Does't Match ")]
        [Display(Name = "ConFirm Password")]
        [DataType(DataType.Password)]
        public string ConFirmPassword { get; set; }
       

        

    }
}
