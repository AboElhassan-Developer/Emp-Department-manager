using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Repository;
using ProjectMVC.ViewModel;

namespace ProjectMVC.Controllers
{
    [Authorize] // التأكد من أن المستخدم مسجل دخوله

    public class EmployeeController : Controller
    {
        //MVCContext context=new MVCContext();
        IDepartmentRepositroy DepartmentRepository;
        IEmployeeRepository EmployeeRepository;

        public EmployeeController(IDepartmentRepositroy deptRepo, IEmployeeRepository empRepo)
        {
            DepartmentRepository = deptRepo;//new DepartmentRepository();
            EmployeeRepository = empRepo;//new EmployeeRepository();
        }

        public IActionResult EmpCardPartial(int id)
        {
            return View("_EmpCard", EmployeeRepository.GetById(id));
        }


        [HttpGet]
        public IActionResult New()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            ViewData["DeptList"] = DepartmentRepository.GetAll();//1



            return View("New");
        }
        [HttpPost]
        public IActionResult SaveNew(AddEmployeeModel EmpFromRequest)
        {
            if (EmpFromRequest.Name != null && EmpFromRequest.Salary >= 6000)
            {
                //save
                if (ModelState.IsValid)
                {
                    var employee = new Employee
                    {
                        Address = EmpFromRequest.Address,
                        DepartmentID = EmpFromRequest.DepartmentID,
                        ImageURL = EmpFromRequest.ImageURL,
                        JobTitle = EmpFromRequest.JobTitle,
                        Name = EmpFromRequest.Name,
                        Salary = EmpFromRequest.Salary
                    };

                    EmployeeRepository.Add(employee);
                    EmployeeRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            ViewData["DeptList"] = DepartmentRepository.GetAll();
            return View("New", EmpFromRequest);
        }
        public IActionResult Index()
        {
            return View("Index", EmployeeRepository.GetAll());
        }
        


        public IActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // جلب بيانات الموظف
            Employee EmpModel = EmployeeRepository.GetById(id);
            if (EmpModel == null)
            {
                return NotFound();
            }

            // إنشاء ViewModel وتعبئته بالبيانات
            AddEmployeeModel EmpViewModel = new AddEmployeeModel
            {
                Id = EmpModel.Id,
                Name = EmpModel.Name,
                Address = EmpModel.Address,
                ImageURL = EmpModel.ImageURL,
                JobTitle = EmpModel.JobTitle,
                Salary = EmpModel.Salary,
                DepartmentID = EmpModel.DepartmentID
            };

            // إضافة قائمة الأقسام إلى ViewBag
            ViewBag.DeptList = DepartmentRepository.GetAll();

            return View("Edit", EmpViewModel);
        }



        [HttpPost]
        public IActionResult SaveEdit(AddEmployeeModel EmpFromRequest)
        {
            if (!ModelState.IsValid)
            {
                // إعادة تعبئة قائمة الأقسام في حالة وجود أخطاء
                ViewBag.DeptList = DepartmentRepository.GetAll();
                return View("Edit", EmpFromRequest);
            }

            // جلب بيانات الموظف من قاعدة البيانات
            Employee EmpFromDb = EmployeeRepository.GetById(EmpFromRequest.Id);
            if (EmpFromDb == null)
            {
                return NotFound();
            }

           
            EmpFromDb.Name = EmpFromRequest.Name;
            EmpFromDb.Address = EmpFromRequest.Address;
            EmpFromDb.Salary = EmpFromRequest.Salary;
            EmpFromDb.JobTitle = EmpFromRequest.JobTitle;
            EmpFromDb.ImageURL = EmpFromRequest.ImageURL;
            EmpFromDb.DepartmentID = EmpFromRequest.DepartmentID;

            EmployeeRepository.Update(EmpFromDb);
            EmployeeRepository.Save();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {

            //return View("Delete",EmployeeRepository.GetById(id));

            Employee employee = EmployeeRepository.GetById(id);

            if (employee != null)
            {

                return View(employee); // عرض صفحة التأكيد
            }

            
            return NotFound();


        }


        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var employee = EmployeeRepository.GetById(id);
            if (employee != null)
            {
                EmployeeRepository.Delete(id);
                EmployeeRepository.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        //public IActionResult Details(int id)
        //{
        //    string msg = "Hellow From Action";
        //    int temp = 50;
        //    List<string> branches = new List<string>();
        //    branches.Add("Assuit");
        //    branches.Add("Alex");
        //    branches.Add("Cairo");
        //    ViewData["Msg"] = msg;
        //    ViewData["Tem"] = temp;
        //    ViewData["brach"] = branches;
        //    Employee EmpModel = EmployeeRepository.GetById(id);//context.Employee.FirstOrDefault(e=>e.Id==id);

        //    return View("Details", EmpModel);
        //}
        //public IActionResult DetailsVM(int id)
        //{
        //    Employee EmpModel = EmployeeRepository.GetById(id);
        //    List<string> branches = new List<string>();
        //    branches.Add("Assuit");
        //    branches.Add("Alex");
        //    branches.Add("Cairo");
        //    EmpDeptColorTempMsgBranchViewModel EmpVM =
        //        new EmpDeptColorTempMsgBranchViewModel();
        //    EmpVM.EmpName = EmpModel.Name;
        //    EmpVM.DeptName = EmpModel.Department.Name;
        //    EmpVM.Color = "Red";
        //    EmpVM.Temp = 12;
        //    EmpVM.Msg = "Hello From VM";
        //    EmpVM.Branches = branches;

        //    return View("DetailsVM", EmpVM);
        //}
    }
}