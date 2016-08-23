using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FlugDemo.Models
{
    /// <summary>
    /// Repräsentiert einen Flug
    /// </summary>
    public class Flug
    {
        /// <summary>
        /// Id des Fluges
        /// </summary>
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string AblugOrt { get; set; }

        [MaxLength(30)]
        [Required]
        public string ZielOrt { get; set; }

        public DateTime Datum { get; set; }

        [MaxLength(30)]
        [Required]
        public string FlugNr { get; set; }

        public List<Buchung> Buchungen { get; set; } = new List<Buchung>();
    }
}
