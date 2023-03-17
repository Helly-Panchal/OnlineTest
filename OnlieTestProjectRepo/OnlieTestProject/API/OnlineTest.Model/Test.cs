using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class Test
    {
        public int Id { get; set; }


        [ForeignKey("TechnologyNavigation")]
        public int TechnologyId { get; set; }   //fk
        public Technology TechnologyNavigation { get; set; }

        public string TestName { get; set; }
        public string Description { get; set; }

        public int CreatedBy { get; set; }    

        [Column(TypeName ="DateTime")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? ExpireOn { get; set; }

        public bool IsActive { get; set; }
  
    }
}
