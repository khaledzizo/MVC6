using Route.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Employee> GetEmployeesByDepartmentId(int id);
        //Department GetById(int id);
        //IEnumerable<Department> GetAll();
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
