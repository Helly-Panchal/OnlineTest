using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class QuestionAnswerMap
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TestNavigation")]
        public int TestId { get; set; }
        public Test TestNavigation { get; set; }


        [ForeignKey("QuestionNavigation")]
        public int QuestionId { get; set; }
        public Question QuestionNavigation { get; set; }


        [ForeignKey("AnswerNavigation")]
        public int AnswerId { get; set; }
        public Answer AnswerNavigation { get; set; }
        public int CreatedBy { set; get; }


        [Column(TypeName = "datetime")]
        public DateTime CreateOn { set; get; }

        public bool IsActive { get; set; }
        //public bool IsAnswer { get; set; }
    }
}
