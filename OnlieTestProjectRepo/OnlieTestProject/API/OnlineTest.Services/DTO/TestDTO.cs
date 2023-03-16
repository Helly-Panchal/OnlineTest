using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;

namespace OnlineTest.Services.DTO
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpireOn { get; set; }

        public int TechnologyId { get; set; }   //fk

        public bool IsDelete { get; set; }
    }
}