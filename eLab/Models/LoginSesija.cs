using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class LoginSesija
    {
        public Guid Id { get; set; }
        public Guid KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
