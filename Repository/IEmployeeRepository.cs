﻿using ProjectMVC.Models;

namespace ProjectMVC.Repository
{
    public interface IEmployeeRepository
    {
        public void Add(Employee obj);


        public void Update(Employee obj);


        public void Delete(int id);


        public List<Employee> GetAll();


        public Employee GetById(int id);


        public void Save();
        
    }
}
