using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Repository;

namespace ProjectMVC.Controllers
{
    public class DepartmentController : Controller
    {
        // MVCContext context=new MVCContext();
        IDepartmentRepositroy DeptRepo;
        
       public DepartmentController(IDepartmentRepositroy deptRepo) 
        {
            DeptRepo = deptRepo; //new DepartmentRepository();
        }
        [Authorize]
        public IActionResult Index()
        {
            List<Department> departmentList = DeptRepo.GetAll();
            
            return View("Index",departmentList);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }
       

        [HttpPost]
        public IActionResult SaveAdd(Department newDeptFromRequest)
        {
            // if (newDeptFromRequest.Name != null)
            if (ModelState.IsValid)
            {
                //context.Department.Add(newDeptFromRequest);
                DeptRepo.Add(newDeptFromRequest);
                // context.SaveChanges();
                DeptRepo.Save();

                //Call Action from another Action
                RedirectToAction("Index");

            }
            return View("Add", newDeptFromRequest);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department department = DeptRepo.GetById(id);
            if (department == null)
            {
                return NotFound(); 
            }
            return View(department); // عرض صفحة التأكيد
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            Department department = DeptRepo.GetById(id);
            if (department == null)
            {
                return NotFound(); 
            }

            DeptRepo.Delete(id); 
            DeptRepo.Save(); 
            return RedirectToAction("Index");
        }
    }
}
