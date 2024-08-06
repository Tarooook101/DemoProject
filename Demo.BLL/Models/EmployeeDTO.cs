using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models
{
    public class EmployeeDTO : BaseEntityDTO
    {

        public EmployeeDTO()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public float? Salary { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentId { get; set; }
        //public virtual Department? Department { get; set; }
        public string? DepartmentName { get; set; }
    }
}
