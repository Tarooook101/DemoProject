using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models
{
    // Model : فيه تفاصيل Entity

    // حلقة الوصل بين Entity and UI ( Controller )

    // اخوه التوأم لي Entity نفس كل البينات الموجودة تبقي موجودة هنا
    // And Secuirty and validations 
    public class DepartmentDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Department Name Required !")]
        [MinLength(2, ErrorMessage = "Min Length 2 char.")]
        [MaxLength(10, ErrorMessage = "Max Length 10 char.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Code Required !")]
        [Range(1,1000,ErrorMessage ="Code Must in 1 : 1000")]

        public string Code { get; set; }

    }
}
