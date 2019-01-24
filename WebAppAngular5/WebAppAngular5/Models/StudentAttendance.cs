using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppAngular5.Models
{
    public class StudentAttendance : BaseEntity
    {
        [ForeignKey("Student")]
        public long StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }

        public bool IsActive { get; set; }
    }
}