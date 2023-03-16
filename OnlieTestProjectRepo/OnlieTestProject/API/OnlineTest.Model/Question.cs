using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class Question
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
        public DateTime CreateOn { set; get; }
        public bool IsDelete{ get; set;}


        [ForeignKey("TestNavigation")]          //FK
        public int TestId { get; set; }
        public Test TestNavigation { get; set; }
    }
}
