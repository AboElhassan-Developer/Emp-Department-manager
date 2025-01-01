using ProjectMVC.Models;
namespace ProjectMVC.Repository
{
    public interface IProjectRepository
    {
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
        List<Project> GetAll();
        Project GetById(int id);
        void Save();
        // Additional method to retrieve employees or other related data
        List<Employee> GetAllEmployees(); // Assuming you have an Employee entity
    }
}
