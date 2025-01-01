using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Repository;
using ProjectMVC.ViewModel;

namespace ProjectMVC.Controllers
{
    public class DepartmentController : Controller
    {
       
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
        public IActionResult SaveAdd(AddDepartmentModel DeptFromRequest)
        {
           
                //save
                if (ModelState.IsValid)
                {
                    var department = new Department
                    {
                        Name = DeptFromRequest.Name,
                        ManagerName=DeptFromRequest.ManagerName,
                        Description= DeptFromRequest.Description,


                    };

                DeptRepo.Add(department);
                DeptRepo.Save();
                    return RedirectToAction("Index");
                }

            ViewData["DeptList"] = DeptRepo.GetAll();
            return View("Add", DeptFromRequest);
        }


        public IActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // جلب بيانات الموظف
            Department deptModel = DeptRepo.GetById(id);
            if (deptModel == null)
            {
                return NotFound();
            }

            // إنشاء ViewModel وتعبئته بالبيانات
            var deptViewModel = new EditDepartmentModel // Create a specific ViewModel for Department
            {
               Id=deptModel.Id,
               Name=deptModel.Name,
               Description=deptModel.Description,
               ManagerName=deptModel.ManagerName,
                
            };

            
            ViewBag.DeptList = DeptRepo.GetAll();

            return View("Edit", deptViewModel);
        }

        [HttpPost]
        public IActionResult SaveEdit(EditDepartmentModel deptFromRequest)
        {
            if (!ModelState.IsValid)
            {
                
                ViewBag.DeptList = DeptRepo.GetAll();
                return View("Edit", deptFromRequest);
            }

            // جلب بيانات الموظف من قاعدة البيانات
            Department deptFromDb = DeptRepo.GetById(deptFromRequest.Id);
            if (deptFromDb == null)
            {
                return NotFound();
            }


            deptFromDb.Name = deptFromRequest.Name;
            deptFromDb.ManagerName = deptFromRequest.ManagerName;
           deptFromDb.Description = deptFromRequest.Description;

         
            DeptRepo.Update(deptFromDb);
            DeptRepo.Save();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            //جلب البيانات القسم من قاعده البيانات
            Department deptModel = DeptRepo.GetById(id);
            if (deptModel == null)
            {
                return NotFound();
            }
            return View(deptModel);
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
