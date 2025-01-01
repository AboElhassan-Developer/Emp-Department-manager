using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Repository;
using ProjectMVC.ViewModel;

namespace ProjectMVC.Controllers
{
    public class ProjectController : Controller
    {
        IProjectRepository ProjectRepo;

        public ProjectController(IProjectRepository projectRepo)
        {
            ProjectRepo = projectRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Project> projectList = ProjectRepo.GetAll();
            return View("Index", projectList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //var viewModel = new AddProjectModel
            //{
            //    DeptList = GetDepartments()
            //};

            //ViewData["ProjList"] = ProjectRepository.GetAll();//1

            //return View("Add");
            var viewModel = new AddProjectModel();
            ViewBag.EmployeeList = ProjectRepo.GetAllEmployees(); // Assuming a method to fetch employees.
            return View("Add",viewModel);
        }
        [HttpPost]
        public IActionResult SaveAdd(AddProjectModel ProjectFromRequest)
        {
            if (ModelState.IsValid)
            {
              

                var project = new Project
                {
                    Name = ProjectFromRequest.Name,
                    Description = ProjectFromRequest.Description,
                    StartDate = ProjectFromRequest.StartDate,
                    EndDate = ProjectFromRequest?.EndDate,
                    EmployeeId = ProjectFromRequest.EmployeeId,
                    Budget = ProjectFromRequest.Budget
                };

               
                ProjectRepo.Add(project);
                ProjectRepo.Save();
                return RedirectToAction("Index");
            }
            
            ViewBag.EmployeeList = ProjectRepo.GetAllEmployees(); // Repopulate employee list for the view.
            return View("Add", ProjectFromRequest);
        }

        //[HttpGet]
        public IActionResult Edit(int id)
        {
            Project projectModel = ProjectRepo.GetById(id);
            if (projectModel == null)
            {
                return NotFound();
            }

            var projectViewModel = new EditProjectModel // Create a specific ViewModel for Department
            {
                Id = projectModel.Id,
                Name = projectModel.Name,
                Description = projectModel.Description,
                StartDate = projectModel.StartDate,
                EndDate = projectModel?.EndDate,
                EmployeeId = projectModel.EmployeeId,
                Budget = projectModel.Budget,


            };
            //ViewBag.DeptList = ProjectRepo.GetAll();

            //return View("Edit", projectModel);
            ViewBag.EmployeeList = ProjectRepo.GetAllEmployees(); // Populate employee list for editing.
            return View("Edit", projectViewModel);
        }

        [HttpPost]
        public IActionResult SaveEdit(EditProjectModel projectFromRequest)
        {
            if (ModelState.IsValid)
            {
                Project projectFromDb = ProjectRepo.GetById(projectFromRequest.Id);
                if (projectFromDb == null)
                {
                    return NotFound();
                }

                projectFromDb.Name = projectFromRequest.Name;
                projectFromDb.StartDate = projectFromRequest.StartDate;
                projectFromDb.EndDate = projectFromRequest.EndDate;
                projectFromDb.Budget = projectFromRequest.Budget;
                projectFromDb.Description = projectFromRequest.Description;

                ProjectRepo.Update(projectFromDb);
                ProjectRepo.Save();

                return RedirectToAction("Index");
            }
            //return View("Edit", projectFromRequest);
            ViewBag.EmployeeList = ProjectRepo.GetAllEmployees(); // Repopulate employee list for the view.
            return View("Edit", projectFromRequest);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Project projectModel = ProjectRepo.GetById(id);
            if (projectModel == null)
            {
                return NotFound();
            }
            return View("Details", projectModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Project projectModel = ProjectRepo.GetById(id);
            if (projectModel == null)
            {
                return NotFound();
            }
            return View("Delete", projectModel);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            Project projectModel = ProjectRepo.GetById(id);
            if (projectModel == null)
            {
                return NotFound();
            }

            ProjectRepo.Delete(id);
            ProjectRepo.Save();

            return RedirectToAction("Index");
        }
    }
}
