using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    [Table("Departments")]
    public class Department : BaseEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } 
    } 
}
