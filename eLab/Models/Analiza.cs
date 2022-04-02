using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Models
{
    public class Analiza
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime Timestamp { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public IEnumerable<Parametri> Parametri { get; set; }
    }
}