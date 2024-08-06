using AutoMapper;
using Demo.BLL.Models;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Service
{
    public class EmployeeService : IEmployeeService
    {
        #region Prop

        private readonly IGenericRepossitory<Employee> _employeeService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public EmployeeService(IGenericRepossitory<Employee> _employeeService, IMapper _mapper)
        {
            this._employeeService = _employeeService;
            this._mapper = _mapper;
        }

        #endregion

        #region Actions

        public async Task<IEnumerable<EmployeeDTO>> getEmployeesAsync(int page, int pageSize)
        {
            var result = await _employeeService.GetAsync(a => a.IsActive == true, page, pageSize,false,
                a =>  a.Department);

            return _mapper.Map<IEnumerable<EmployeeDTO>>(result);
        }
        public async Task<EmployeeDTO> getEmployeeAsync(Guid id)
        {
            var result = await _employeeService.GetFirstOrDefaultAssync(a => a.IsActive == true && a.Id == id);
            return _mapper.Map<EmployeeDTO>(result);
        }

        public async Task CreateOrUpdateEmployeeAsync(EmployeeDTO employee)
        {
            var obj = _mapper.Map<Employee>(employee);
            await _employeeService.CreateOrUpdateAsync(obj);
        }

        public async Task DeleteEmployeeAsync(EmployeeDTO employee)
        {
            var obj = _mapper.Map<Employee>(employee);
            obj.IsActive = false;
            await _employeeService.DeleteAsync(obj);
        }

        #endregion
    }

    public interface IEmployeeService
    {
        // DTO : علشان controller هيكلم dto مش هيكلم Entity
        public Task<IEnumerable<EmployeeDTO>> getEmployeesAsync(int page , int pageSize);
        public Task<EmployeeDTO> getEmployeeAsync(Guid id);
        public Task CreateOrUpdateEmployeeAsync(EmployeeDTO employee);
        public Task DeleteEmployeeAsync(EmployeeDTO employee);


    }
}
