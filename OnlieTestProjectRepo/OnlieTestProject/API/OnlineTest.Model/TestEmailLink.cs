using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace OnlineTest.Model
{
    public class TestEmailLink
    {
        [Key]
        public int Id { get; set; }
        public Guid Token { get; set; }

        [ForeignKey("TestNavigation")] 
        public int TestId { get; set; }
        public Test TestNavigation { get; set; }

        [ForeignKey("UserNavigation")]
        public int UserId { get; set; }
        public User UserNavigation { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? AccessOn { get; set; }

        public int AccessCount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? SubmittedOn { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ExpireOn { get; set; }

        public bool IsActive { get; set; }
    }
}
