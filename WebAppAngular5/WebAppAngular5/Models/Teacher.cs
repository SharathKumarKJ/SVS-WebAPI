using System;

namespace WebAppAngular5.Models
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AadharNumber { get; set; }

        public string PANNumber { get; set; }

        public string Qulification { get; set; }

        public string SpecialistIn { get; set; }

        public int Experience { get; set; }

        public string SubjectsHandle { get; set; }

        public DateTimeOffset DateofBirth { get; set; }

        public string CasteName { get; set; }

        public string Nationality { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Gender { get; set; }

        public bool IsActive { get; set; }
    }
}