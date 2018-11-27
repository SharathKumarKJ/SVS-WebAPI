using System;

namespace WebAppAngular5.Models
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ClassDetail ClassDetail { get; set; }

        public string STSCode { get; set; }

        public string Gender { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public string FatherMobileNumber { get; set; }

        public string MotherMobileNumber { get; set; }
        public string CasteName { get; set; }

        public string Nationality { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public DateTime DateofBirth { get; set; }
    }
}