using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMVC.Models;
namespace ProjectMVC.Models
{
    public class Project
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Project Name is required.")]
        //[StringLength(200, ErrorMessage = "Project Name cannot be longer than 200 characters.")]
        public string Name { get; set; }
        //[StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //[Required(ErrorMessage = "Budget is required.")]
        //[Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value.")]
        public decimal Budget { get; set; }
    }
}
