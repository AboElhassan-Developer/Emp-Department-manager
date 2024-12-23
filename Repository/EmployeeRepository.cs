using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;

namespace ProjectMVC.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {

        MVCContext context;
        public EmployeeRepository(MVCContext _context)
        {
            context = _context; //new MVCContext();
        }
        //CRUD
        public void Add(Employee obj)
        {
            context.Employee.Add(obj);
        }
        public void Update(Employee obj)
        {
            context.Update(obj);
           
        }
        public void Delete(int id)
        {
            Employee Emp = GetById(id);
            context.Remove(Emp);
        }
        public List<Employee> GetAll()
        {
            return context.Employee.ToList();
        }
        public Employee GetById(int id)
        {
            return context.Employee.FirstOrDefault(e => e.Id == id);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
