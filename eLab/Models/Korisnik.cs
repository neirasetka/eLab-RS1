using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Korisnik
    {
        public Guid Id { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail Adresa nije validna")]
        public String Email { get; set; }
        public Guid TipKorisnikaId { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public Guid UstanovaId { get; set; }
        public Ustanova Ustanova { get; set; }
        public Guid CoreUserId { get; set; }
    }
}
