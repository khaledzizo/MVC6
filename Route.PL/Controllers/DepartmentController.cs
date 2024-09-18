using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.DAL.Entities;
using Route.PL.Models;

namespace Route.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(
            /*IDepartmentRepository departmentRepository,*/ 
            ILogger<DepartmentController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            
        {
            //_departmentRepository = departmentRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            // var displayedDepartments = _mapper.Map<IEnumerable<DepartmentVM>>(departments);
            var displayedDepartments = _mapper.Map<IEnumerable<DepartmentVM>>(departments);
            return View(displayedDepartments);
        }

        public IActionResult Create()
        {
            return View(new DepartmentVM());
        }
        [HttpPost]
        public IActionResult Create(DepartmentVM department)
        {
            if (ModelState.IsValid)
            {
                //var addedDepartment = new Department()
                //{
                //    Code = department.Code,
                //    Name = department.Name,
                //    Id = department.Id,
                //    DateOfCreation = department.DateOfCreation
                //};

                var addedDepartments = _mapper.Map<Department>(department);

                _unitOfWork.DepartmentRepository.Add(addedDepartments);
                return RedirectToAction("Index");
            }
            else
                return View(department);
        }

        public IActionResult Details(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            var deptartmentDetails = _mapper.Map<DepartmentVM>(department);
            return View(deptartmentDetails);
        }

        public IActionResult Update(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //var departmentVM = new DepartmentVM()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    DateOfCreation = department.DateOfCreation
            //};
            var departmentVM = _mapper.Map<DepartmentVM>(department);
            return View(departmentVM);
        }
        [HttpPost]
        public IActionResult Update(DepartmentVM department)
        {
            if (ModelState.IsValid)
            {
                //var addedDepartment = new Department()
                //{
                //    Id = department.Id,
                //    Name = department.Name,
                //    Code = department.Code,
                //    DateOfCreation = department.DateOfCreation
                //};

                var updatedDepartment = _mapper.Map<Department>(department);
                _unitOfWork.DepartmentRepository.Update(updatedDepartment);
                return RedirectToAction("Index");
            }
            else
                return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            _unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
