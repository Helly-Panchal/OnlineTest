using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;

namespace OnlineTest.Services.DTO
{
    public class TechnologyDTO
    {
        public int Id { get; set; }
        public string TechName { get; set; }
        public int CreatedBy { get; set; }  //fk //add
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }       //fk //put
        public DateTime? ModifiedOn { get; set; }
    }
}
