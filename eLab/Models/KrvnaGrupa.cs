using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class KrvnaGrupa
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String BloodType { get; set; }
        public char RhFactor { get; set; }
    }
}
