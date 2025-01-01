using ProjectMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.ViewModel
{
    public class EmpWhthDeptListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        public int Salary { get; set; }
        public string JobTitle { get; set; }
        public string ImageURL { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public List<Department>DeptList { get; set; }

        // إضافة خصائص المشروع
        public string ProjectName { get; set; }  // اسم المشروع
        public DateTime? StartDate { get; set; }  // تاريخ البدء
        public DateTime? EndDate { get; set; }    // تاريخ الانتهاء
       /* public decimal? Budget { get; set; }  */    // الميزانية
    }
}
