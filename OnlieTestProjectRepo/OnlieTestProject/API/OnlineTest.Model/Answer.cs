using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Model
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Ans { get; set; }
        public int CreatedBy { set; get; }

        [Column(TypeName = "datetime")]
        public DateTime CreateOn { set; get; }
        public bool IsActive { get; set; }
    }
}
