using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Uputnica
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String Name { get; set; }
        public Korisnik Korisnik { get; set; }
        public Guid KorisnikId { get; set; }
        public TipUzorka TipUzorka { get; set; }
        public Guid TipUzorkaId { get; set; }
        public Uzorkovanje Uzorkovanje { get; set; }
        public Guid UzorkovanjeId { get; set; }
        public Pacijent Pacijent { get; set; }
        public Guid PacijentId { get; set; }

    }
}
