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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly RouteDbContext _context;

        public DepartmentRepository(RouteDbContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id)
            => _context.Employees.Where(emp => emp.DepartmentId == id).ToList();

        //public int Add(Department department)
        //{
        //    _context.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Remove(department);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //    => _context.Departments.ToList();

        //public Department GetById(int id)
        //    => _context.Departments.FirstOrDefault(dept => dept.Id == id);

        //public int Update(Department department)
        //{
        //    _context.Update(department);
        //    return _context.SaveChanges();
        //}
    }
}
