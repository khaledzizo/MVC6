using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Route.BLL.Interfaces;
using Route.DAL.Entities;
using Route.PL.Helper;
using Route.PL.Models;

namespace Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index(int departmentId = 0, string searchValue = "")
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeVM> employeesVM;
            if (departmentId != 0)
            {
                employees = _unitOfWork.DepartmentRepository.GetEmployeesByDepartmentId(departmentId);
                ViewBag.DepartmentId = departmentId;
                employeesVM = _mapper.Map<IEnumerable<EmployeeVM>>(employees);
                return View(employeesVM);
            }

            if (searchValue == "")
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.Search(searchValue);

            employeesVM = _mapper.Map<IEnumerable<EmployeeVM>>(employees);
            return View(employeesVM);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(new EmployeeVM());
        }
        [HttpPost]
        public IActionResult Create(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {
                var addedEmployee = _mapper.Map<Employee>(employee);
                addedEmployee.ImageName = DocumentsSettings.UploadFile(employee.Image, "Images");
                _unitOfWork.EmployeeRepository.Add(addedEmployee);
                return RedirectToAction("Index");
            }
            else
                return View(employee);
        }

        public IActionResult Details(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            ViewBag.Department = _unitOfWork.DepartmentRepository.GetById(employee.DepartmentId);
            var employeeDetails = _mapper.Map<EmployeeVM>(employee);
            return View(employeeDetails);
        }

        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            var updatedEmployee = _mapper.Map<EmployeeVM>(employee);
            return View(updatedEmployee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {

                string oldImage = _unitOfWork.EmployeeRepository.GetImageByIdWithNoTracking(employee.Id);
                var updatedEmployee = _mapper.Map<Employee>(employee);

                if (employee.Image == null)
                    updatedEmployee.ImageName = oldImage;
                else
                {
                    bool flag = DocumentsSettings.RemoveFile(oldImage, "Images");
                    if(flag)
                        updatedEmployee.ImageName = DocumentsSettings.UploadFile(employee.Image, "Images");
                }

                //updatedEmployee = (employee.Image == null) ? new Employee()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Salary = employee.Salary,
                //    Address = employee.Address,
                //    IsActive = employee.IsActive,
                //    Email = employee.Email,
                //    HireDate = employee.HireDate,
                //    DepartmentId = employee.DepartmentId,
                //    ImageName = updatedEmployee.ImageName

                //} : _mapper.Map<Employee>(employee);

                _unitOfWork.EmployeeRepository.Update(updatedEmployee);
                return RedirectToAction("Index");
            }
            else
                return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            bool flag = DocumentsSettings.RemoveFile(employee.ImageName, "Images");
            if (flag)
                _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
