using ProjectMVC.Models;
//using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        MVCContext context;
        public ProjectRepository(MVCContext _context)
        {
            context = _context;
        }

        public void Add(Project project)
        {
            context.Add(project);
        }

        public void Update(Project project)
        {
            context.Project.Update(project);
        }

        public void Delete(int id)
        {
            var project = GetById(id);
            if (project != null)
            {
                context.Remove(project);
            }
        }

        public List<Project> GetAll()
        {
            return context.Project.ToList();
        }
        public Project GetById(int id)
        {
            return context.Project.FirstOrDefault(p => p.Id == id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public List<Employee> GetAllEmployees()
        {
            return context.Employee.ToList(); // Adjust to match your DB context structure.
        }

    }
}
