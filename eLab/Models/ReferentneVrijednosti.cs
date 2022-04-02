using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class ReferentneVrijednosti
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public String Unit { get; set; }
    }
}