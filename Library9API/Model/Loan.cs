using Library9API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library9API.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookCopiesId { get; set; }

        public string? MembersId { get; set; }

        public string? EmployeesId { get; set; }

        [Required]
        public DateTime BorrowTime { get; set; }

        [Required]
        public DateTime DueTime { get; set; }

        public DateTime? ReturnTime { get; set; } 

        public bool IsDelivered { get; set; } // (Default değer = False )

        [ForeignKey(nameof(MembersId))]
        public Member? Member { get; set; }

        [ForeignKey(nameof(EmployeesId))]
        public Employee? Employee { get; set; }
       
    }
}
