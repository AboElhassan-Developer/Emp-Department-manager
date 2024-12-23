using ProjectMVC.Models;

namespace ProjectMVC.Repository
{
    public class DepartmentRepository: IDepartmentRepositroy
    {
        MVCContext context;
        public DepartmentRepository(MVCContext _context)
        {
            context = _context;//new MVCContext();
        }
        //CRUD
        public void Add(Department obj)
        {
            context.Add(obj);
        }
        public void Update(Department obj) 
        { 
            context.Update(obj);
        }
        public void Delete(int id) 
        { 
        Department dept=GetById(id);
            context.Remove(dept);
        }
        public List<Department> GetAll()
        {
            return context.Department.ToList();
        }
        public Department GetById(int id)
        {
        return context.Department.FirstOrDefault(d=>d.Id==id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
