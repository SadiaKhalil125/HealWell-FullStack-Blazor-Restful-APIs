using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DoctorLoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }   
        public bool RememberMe { get; set; }
    }
}
