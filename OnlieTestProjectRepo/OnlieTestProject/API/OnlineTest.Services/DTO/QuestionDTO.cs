using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;

namespace OnlineTest.Services.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string Que { get; set; }
        public int Type { get; set; }
        public int Weightage { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { set; get; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedOn { set; get; }
        public bool IsDelete { get; set; }
        public int TestId { get; set; }
    }
}
