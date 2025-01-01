using ProjectMVC.Models;

namespace ProjectMVC.ViewModel
{
    public class EditDepartmentModel
    {
        public int Id { get; set; }          // ID of the department
        public string Name { get; set; }      // Name of the department
        public string ManagerName { get; set; } // Name of the manager
        public string Description { get; set; }  // Image URL for the department
       /* public List<Department> DeptList { get; set; }*/ // List of departments for dropdowns 

    }
}
