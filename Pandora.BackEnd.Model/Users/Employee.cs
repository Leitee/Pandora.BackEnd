using Pandora.BackEnd.Model.AppEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandora.BackEnd.Model.Users
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string AppsUerId { get; set; }

        public GenderEnum Gender { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("AppsUerId")]
        public virtual AppUser AppUser { get; set; }

        public override string ToString()
        {
            return $"{AppUser.FirstName} {AppUser.LastName}";
        }
    }
}
