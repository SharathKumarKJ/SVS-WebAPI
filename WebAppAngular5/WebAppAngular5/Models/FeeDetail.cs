using System;

namespace WebAppAngular5.Models
{
    public class FeeDetail: BaseEntity
    {

        public Student Student { get; set; }
        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PaidDate { get; set; }

        public decimal Balance { get; set; }
    }
}