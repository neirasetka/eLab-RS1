using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class DijagnozaUputnica
    {
        public Guid Id { get; set; }
        public Dijagnoza Dijagnoza { get; set; }
        public Guid DijagnozaId { get; set; }
        public Uputnica Uputnica { get; set; }
        public Guid UputnicaId { get; set; }
    }
}
