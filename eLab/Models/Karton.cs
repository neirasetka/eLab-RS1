using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Karton
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public string Allergies { get; set; }
        public string Anamnesis { get; set; }
        public float Height { get; set; }
        public int Weight { get; set; }
        public KrvnaGrupa KrvnaGrupa { get; set; }
    }
}
