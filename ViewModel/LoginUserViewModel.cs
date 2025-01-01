using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "UserName is Required ")]
      
        public string UserName { get; set; }
        [Required(ErrorMessage = " Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me!!")]
        public bool RememberMe {  get; set; }

    }
}
