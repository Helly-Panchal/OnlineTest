using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Model
{
    public class MailOutBound
    {
        [Key]
        public int Id { get; set; }
        public string To { get; set; }
        public string Body { get; set; }

        [ForeignKey("TestEmailLink")]
        public int TestLinkId { get; set; }
        public TestEmailLink TestEmailLink { get; set; }

        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
    }
}
