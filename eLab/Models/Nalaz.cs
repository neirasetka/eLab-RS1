using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Nalaz
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Guid PacijentId { get; set; }
        public Pacijent Pacijent { get; set; }
        public Guid AnalizaId { get; set; }
        public Analiza Analiza { get; set; }
        public bool IsUrgent { get; set; }
    }
}