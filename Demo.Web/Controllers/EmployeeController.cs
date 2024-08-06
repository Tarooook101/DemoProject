using AutoMapper;
using Demo.BLL;
using Demo.BLL.Models;
using Demo.BLL.Service;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Web.Controllers
{
    public class EmployeeController : Controller
    {
        #region Props

        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        #endregion

        #region ctor
        public EmployeeController(IEmployeeService _employeeService, IDepartmentService _departmentService)
        {
            this._employeeService = _employeeService;
            this._departmentService = _departmentService;
        }


        #endregion

        #region Actions
        // Action
        public async Task<IActionResult> Index()
        {
            var emps = await _employeeService.getEmployeesAsync(1,10);
            return View(emps);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var emp = await _employeeService.getEmployeeAsync(id);
            return View(emp);
        }

        [HttpGet]
        public async Task<IActionResult> create()
        {
            var depts = await _departmentService.GetAsync(a => a.IsActive == true);
            ViewBag.DepartmentList = new SelectList(depts, "Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(EmployeeDTO emp)
        {
            try
            {
                if (ModelState.IsValid) //  == True
                {
                    await _employeeService.CreateOrUpdateEmployeeAsync(emp);
                    return RedirectToAction("Index");
                }
            } 
            catch(Exception ex)
            {
                // LOG
                //Handle
            }

            // If there have error make it to show again department select list.
            ViewBag.DepartmentList = new SelectList(await _departmentService
                .GetAsync(a => a.IsActive == true),"Id","Name");
            return View(emp);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var emp = await _employeeService.getEmployeeAsync(id);
            ViewBag.DepartmentList = new SelectList(await _departmentService
                .GetAsync(a => a.IsActive == true), "Id", "Name", emp.DepartmentId);

            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeDTO employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.CreateOrUpdateEmployeeAsync(employee);
                    return RedirectToAction("Index");
                }
            } 
            catch(Exception ex)
            {

            }
            ViewBag.DepartmentList = new SelectList(await _departmentService
                .GetAsync(a => a.IsActive == true), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var emp = await _employeeService.getEmployeeAsync(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeDTO employee)
        {
            await _employeeService.DeleteEmployeeAsync(employee);
            return RedirectToAction("Index");
        }

        #endregion

        #region Ajax Calls
        #endregion
    }
}
