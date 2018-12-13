using System;

namespace WebAppAngular5.Models
{
    public class FeeDetail : BaseEntity
    {
        public Student Student { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        public FeeStatusValue FeeStatus { get; set; }

        public bool IsActive { get; set; }

    }

    public enum FeeStatusValue { Pending, NotPaid, Paid, PartiallyPaid };
}