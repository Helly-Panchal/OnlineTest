using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
        
    }
}
