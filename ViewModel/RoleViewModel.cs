using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class RoleViewModel
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
