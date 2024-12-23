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
            return View("_EmpCard",EmployeeRepository.GetById(id));
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
        public IActionResult SaveNew(Employee EmpFromRequest)
        {
            if(EmpFromRequest.Name != null && EmpFromRequest.Salary >= 6000)
            {
                //save
                if (ModelState.IsValid)
                {
                    EmployeeRepository.Add(EmpFromRequest);
                    EmployeeRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            ViewData["DeptList"] = DepartmentRepository.GetAll();
            return View("New",EmpFromRequest);
        }
        public IActionResult Index()
        {
            return View("Index",EmployeeRepository.GetAll());
        }
        //Handel Link
        public IActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }
            Employee EmpModel=
               EmployeeRepository.GetById(id);
            List<Department> DepartmentList = DepartmentRepository.GetAll();

            //......Create View Model Maping
            

                EmpWhthDeptListViewModel EmpViewModel = new EmpWhthDeptListViewModel();
                EmpViewModel.Id = EmpModel.Id;
                EmpViewModel.Name = EmpModel.Name;
                EmpViewModel.Address = EmpModel.Address;
                EmpViewModel.ImageURL = EmpModel.ImageURL;
                EmpViewModel.JobTitle = EmpModel.JobTitle;
                EmpViewModel.Salary = EmpModel.Salary;
                EmpViewModel.DepartmentID = EmpModel.DepartmentID;

                EmpViewModel.DeptList = DepartmentList;


                return View("Edit", EmpViewModel);
            
        }
        [HttpPost]
        public IActionResult SaveEdit(EmpWhthDeptListViewModel EmpFromRequest,int id)
        {
            // if (EmpFromRequest.Name != null)
            if (ModelState.IsValid)
            {
                Employee EmpFromDb = EmployeeRepository.GetById(id); //context.Employee.FirstOrDefault(e => e.Id == id);
                EmpFromDb.Address= EmpFromRequest.Address;
                EmpFromDb.Name= EmpFromRequest.Name;
                EmpFromDb.Salary= EmpFromRequest.Salary;
                EmpFromDb.JobTitle= EmpFromRequest.JobTitle;
                EmpFromDb.ImageURL= EmpFromRequest.ImageURL;
                EmpFromDb.DepartmentID= EmpFromRequest.DepartmentID;
                EmpFromDb.Id= EmpFromRequest.Id;
                EmployeeRepository.Update(EmpFromDb);
                EmployeeRepository.Save();
                //context.SaveChanges();
                return RedirectToAction("Index");
            }
            //All Emp Date List <Depatment>
            EmpFromRequest.DeptList = DepartmentRepository.GetAll();//context.Department.ToList();
            return View("Edit",EmpFromRequest);

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

            // إذا لم يتم العثور على الموظف
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


        public IActionResult Details(int id)
        {
            string msg = "Hellow From Action";
            int temp = 50;
            List<string>branches = new List<string>();
            branches.Add("Assuit");
            branches.Add("Alex");
            branches.Add("Cairo");
            ViewData["Msg"] = msg;
            ViewData["Tem"] = temp;
            ViewData["brach"]=branches;
            Employee EmpModel = EmployeeRepository.GetById(id);//context.Employee.FirstOrDefault(e=>e.Id==id);

            return View("Details",EmpModel);
        }
        public IActionResult DetailsVM(int id)
        {
            Employee EmpModel=EmployeeRepository.GetById(id);
            List<string> branches = new List<string>();
            branches.Add("Assuit");
            branches.Add("Alex");
            branches.Add("Cairo");
            EmpDeptColorTempMsgBranchViewModel EmpVM =
                new EmpDeptColorTempMsgBranchViewModel();
            EmpVM.EmpName = EmpModel.Name;
            EmpVM.DeptName= EmpModel.Department.Name;
            EmpVM.Color = "Red";
            EmpVM.Temp = 12;
            EmpVM.Msg = "Hello From VM";
            EmpVM.Branches = branches;

            return View("DetailsVM", EmpVM);
        }
    }
}
