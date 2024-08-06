using Demo.BLL.Models;
using Demo.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Demo.DAL.Database;
using Demo.DAL.Entities;
namespace Demo.Web.Controllers
{
    public class DepartmentController : Controller
    {
        #region Props

        // Tightly Coupling : class (DepartmentService)
        // DepartmentService _department;

        // Loossly Coupling : Interface (IDepartmentService)
        private readonly IDepartmentService _department;

        private readonly IMapper mapper;

        #endregion

        #region ctor
        public DepartmentController(IDepartmentService _department, IMapper mapper)
        {
            this._department = _department;
            this.mapper = mapper;
        }
        // Transient
        // Scoped     // Recommended.
        // SingleTone

        #endregion

        #region Actions
        // Action
        public async Task<IActionResult> Index(string? SearchValue)
        {
            if (SearchValue == null)
            {
                // لو مفيش نتيجة بحث هيجيب كل البيانات اللي موجوده
                var depts = await _department.GetAsync(x => x.Name != null);

                // Auto Mapper
                var result = mapper.Map<List<DepartmentDTO>>(depts);

                return View(result);

            }
            else
            {
                var depts = await _department.GetAsync(x => x.Name.Contains(SearchValue)
                || x.Code.Contains(SearchValue) || x.Id.ToString() == SearchValue);

                var result = mapper.Map<List<DepartmentDTO>>(depts);
                return View(result);
            }


        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _department.GetByIdAsync(a => a.Id == id);
            var result = mapper.Map<DepartmentDTO>(department);
            return View(result);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> create(DepartmentDTO department)
        {
            if (ModelState.IsValid) //  == True
            {
                var result = mapper.Map<Department>(department);
                await _department.CreateAsync(result);
                return RedirectToAction("Index");
            }
            else
            {
                // رجعلي نفس الصفحة ثاني بنفس البيانات 
                return View(department);
            }
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var department = await _department.GetByIdAsync(a => a.Id == id);
            var result = mapper.Map<DepartmentDTO>(department);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> update(DepartmentDTO department)
        {

            if (ModelState.IsValid)
            {
                var result = mapper.Map<Department>(department);
                await _department.UpdateAsync(result);
                return RedirectToAction("Index");
            }
            else
                return View(department);
        }

        // Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var department = await _department.GetByIdAsync(a => a.Id == id);
            var result = mapper.Map<DepartmentDTO>(department);
            return View(result);
        }

        [HttpPost]
        //[ActionName("Delete")]
        // and name of function make it : CondirmDelete(int id)
        public async Task<IActionResult> Delete(DepartmentDTO department)
        {
            var result = mapper.Map<Department>(department);
            await _department.DeleteAsync(result);
            return RedirectToAction("Index");
        }

        public IActionResult Test()
        {
            return View();
        }

        // Consume API
        // مثلا port 123 بيبقي متغير وانت عايز تعدل عليه
        // بعد متعمل build للمشروع
        // so add port "123" in confgFile Json File 
        // public IActionResult Connect("123","docs.ITShare.com/getData")   ❎
         
        // public IActionResult Connect(ConfgFile.Port,"docs.ITShare.com/getData") ✅
        // {
        //   
        // }


        #endregion

        #region Ajax Calls
        #endregion
    }
}
