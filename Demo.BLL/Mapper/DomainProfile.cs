using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;
using AutoMapper;
using Demo.BLL.Models;

namespace Demo.BLL.Mapper
{
    // Domain mean Entity
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<EmployeeDTO, Employee>().ReverseMap()
            .ForPath(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            // كده جايب كل بيانات الموظف مع 1 بيانات من القسم

            #region Comments

            // mean Entity is Model
            // .ReverseMap() : CreateMap<Department, DepartmentDTO>() (Get) and
            // CreateMap<DepartmentDTO, Department>() (Create - Update - Delete) Two Lines of Code



            // For Example
            // CreateMap<EmployeeDTO, Employee>().ReverseMap()
            //.ForPath(dest => dest._DepartmentId_FK, opt => opt.MapFrom(src => src.Department.Id))
            //.ForPath(dest => dest._DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            //.ForPath(dest => dest._DepartmentCode, opt => opt.MapFrom(src => src.Department.Code));

            // Id
            // Name
            // Age
            // Address
            // Department Department 
            // كده هجيب بيانات  Department كلها 
            // انا عايز 3 بيانات فقط من Department
            // فأكتب الكود اللي تحت ده وخد البيانات اللي عايزها


            #endregion
        }
    }
}
