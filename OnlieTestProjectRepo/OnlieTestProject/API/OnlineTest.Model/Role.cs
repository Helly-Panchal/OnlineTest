using System.ComponentModel.DataAnnotations;
namespace OnlineTest.Model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
