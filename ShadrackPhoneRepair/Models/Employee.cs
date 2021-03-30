using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShadrackPhoneRepair.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(30)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(30)]
        public string EmpSurname { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpEmail { get; set; }

        [Required]
        public string EmpPassword { get; set; }

        public double EmpRate { get; set; }

        public int EmpHours { get; set; }

        public double EmpWage { get; set; }

        [Required]
        [StringLength(25)]
        public string EmployeeRole { get; set; }

        public double GetEmployeeWage()
        {
            return (EmpRate * EmpHours);
        }
    }
}