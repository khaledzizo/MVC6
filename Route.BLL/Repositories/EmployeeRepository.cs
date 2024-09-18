using Microsoft.EntityFrameworkCore;
using Route.BLL.Interfaces;
using Route.DAL.Context;
using Route.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly RouteDbContext _context;

        public EmployeeRepository(RouteDbContext context) : base(context)
        {
            _context = context;
        }
        
        public string GetImageByIdWithNoTracking(int id)
            => _context.Employees.AsNoTracking().FirstOrDefault(emp => emp.Id == id).ImageName;
        
        

        public IEnumerable<Employee> Search(string searchValue)
            => _context.Employees.Where(emp => emp.Name.Trim().ToLower().Contains(searchValue.Trim().ToLower()));

        //public int Add(Employee employee)
        //{
        //    _context.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _context.Employees.ToList();

        //public Employee GetById(int id)
        //    => _context.Employees.FirstOrDefault(emp => emp.Id == id);

        //public int Update(Employee employee)
        //{
        //    _context.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
