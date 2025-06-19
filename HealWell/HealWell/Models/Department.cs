using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealWell.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Doctor?> Doctors { get; set; } = new();

    }
}
