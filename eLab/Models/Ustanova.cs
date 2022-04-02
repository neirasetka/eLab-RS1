using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Ustanova
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String Name { get; set; }
        public String Adresa { get; set; }
        public Guid GradId { get; set; }
        public Grad Grad { get; set; }
        public String Telefon { get; set; }
    }
}
