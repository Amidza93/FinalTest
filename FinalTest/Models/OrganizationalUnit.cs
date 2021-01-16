using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class OrganizationalUnit
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(2010,2019)]
        public int EstablishmentYear { get; set; }

    }
}