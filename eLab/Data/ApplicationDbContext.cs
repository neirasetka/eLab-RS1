using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using eLab.Models;

namespace eLab.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<eLab.Models.TipKorisnika> TipKorisnika { get; set; }
        public DbSet<eLab.Models.Grad> Grad { get; set; }
        public DbSet<eLab.Models.Ustanova> Ustanova { get; set; }
        public DbSet<eLab.Models.Materijali> Materijali { get; set; }
        public DbSet<eLab.Models.KrvnaGrupa> KrvnaGrupa { get; set; }
        public DbSet<eLab.Models.Karton> Karton { get; set; }
        public DbSet<eLab.Models.ReferentneVrijednosti> ReferentneVrijednosti { get; set; }
        public DbSet<eLab.Models.Korisnik> Korisnik { get; set; }
        public DbSet<eLab.Models.LoginSesija> LoginSesija { get; set; }
        public DbSet<eLab.Models.Dijagnoza> Dijagnoza { get; set; }
        public DbSet<eLab.Models.Parametri> Parametri { get; set; }
        public DbSet<eLab.Models.Analiza> Analiza { get; set; }
        public DbSet<eLab.Models.Nalaz> Nalaz { get; set; }
        public DbSet<eLab.Models.Pacijent> Pacijent { get; set; }
        public DbSet<eLab.Models.TipUzorka> TipUzorka { get; set; }
        public DbSet<eLab.Models.Uzorkovanje> Uzorkovanje { get; set; }
        public DbSet<eLab.Models.UzorkovanjeMaterijali> UzorkovanjeMaterijali { get; set; }
        public DbSet<eLab.Models.Uputnica> Uputnica { get; set; }
        public DbSet<eLab.Models.DijagnozaUputnica> DijagnozaUputnica { get; set; }
        public DbSet<eLab.Models.ParametriUputnica> ParametriUputnica { get; set; }
    }
}
