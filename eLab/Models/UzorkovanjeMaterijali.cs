using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class UzorkovanjeMaterijali
    {
        public Guid Id { get; set; }
        public Uzorkovanje Uzorkovanje { get; set; }
        public Guid UzorkovanjeId { get; set; }
        public Materijali Materijali { get; set; }
        public Guid MaterijaliId { get; set; }
    }
}