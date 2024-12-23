using ProjectMVC.Models;

namespace ProjectMVC.Repository
{
    public interface IDepartmentRepositroy
    {
        public void Add(Department obj);

        public void Update(Department obj);


        public void Delete(int id);


        public List<Department> GetAll();


        public Department GetById(int id);


        public void Save();
      
    }
}
