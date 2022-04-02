using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class ParametriUputnica
    {
        public Guid Id { get; set; }
        public Parametri Parametri { get; set; }
        public Guid ParametriId { get; set; }
        public Uputnica Uputnica { get; set; }
        public Guid UputnicaId { get; set; }
    }
}
