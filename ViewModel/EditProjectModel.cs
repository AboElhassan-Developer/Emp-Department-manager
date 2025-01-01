
using ProjectMVC.Models;
namespace ProjectMVC.ViewModel
{
    public class EditProjectModel
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmployeeId { get; set; }
        public decimal Budget { get; set; }
    }
}
