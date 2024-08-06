using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        // string ==>> uniqidentfier1
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public float? Salary { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        // virtual زياده مش مهمه انك تكتبها كانت موجوده زمان

        [ForeignKey("CreatedBy")]
        public Employee? User { get; set; }
    }
}
