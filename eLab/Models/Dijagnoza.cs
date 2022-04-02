using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Dijagnoza
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String LatinskiNaziv { get; set; }
        public String LokalniNaziv { get; set; }
        public String SifraMKB10 { get; set; }
    }
}
