using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Department Name is required.")]
        //[StringLength(100, ErrorMessage = "Department Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        //[StringLength(100, ErrorMessage = "Manager Name cannot be longer than 100 characters.")]

        public string ManagerName { get; set; }
        //[StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }
        //public List<Employee>? Emps { get; set; }
        //public List<Employee> Employees { get; set; }
    }
}
