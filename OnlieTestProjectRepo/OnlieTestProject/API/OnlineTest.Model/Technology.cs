using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Technology name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Technology name can not be longer than 50 characters.")]
        public string TechName { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public int CreatedBy { get; set; }          

        [Column(TypeName ="DateTime")]
        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }       

        [Column(TypeName = "DateTime")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
