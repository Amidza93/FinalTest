using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(70)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }
        [Range(1960, 1999)]
        public int? BirthYear { get; set; }
        [Required]
        [Range(2010, 2020)]
        public int EmploymentYear { get; set; }
        [Required]
        [Range(typeof(decimal), "251", "9999")]
        public decimal Sallary { get; set; }
        public OrganizationalUnit OrganizationalUnit { get; set; }
        public int OrganizationalUnitId { get; set; }
    }
}