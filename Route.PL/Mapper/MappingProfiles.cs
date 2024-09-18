using AutoMapper;
using Route.DAL.Entities;
using Route.PL.Models;

namespace Route.PL.Mapper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles() 
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
        }
    }
}
