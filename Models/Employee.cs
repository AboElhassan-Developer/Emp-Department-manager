using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Range(6000, int.MaxValue, ErrorMessage = "Salary must be at least 6000")]
        public int Salary{ get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }
        [Required]
        [RegularExpression(@"^.*\.(jpg|png)$", ErrorMessage = "Invalid image URL")]
       
        public string ImageURL { get; set; }
        [Required]
        [RegularExpression(@"(Alex|Cairo|Assuit)", ErrorMessage = "Invalid Address")]
        public string? Address { get; set; }
        [ForeignKey("Department")]
        [Display(Name="Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
