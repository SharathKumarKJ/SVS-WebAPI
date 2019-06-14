using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppAngular5.Models
{
    public class StudentMarksSheet : BaseEntity
    {

        [ForeignKey("Student")]
        public long StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Subject")]
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey("ClassDetail")]
        public long ClassDetailId { get; set; }
        public ClassDetail ClassDetail { get; set; }

        public ExamTypeValues ExamType { get; set; }

        public ResultStatusValues ResultStatus { get; set; }

        public long TotalMarks { get; set; }

        public long MarksObtained { get; set; }

        public bool IsActive { get; set; }

        public GradeValues Grade { get; set; }


    }

    public enum ExamTypeValues
    {
        Test_1, Test_2, MidTerm, Test_3, Test_4, AnnualExam
    }

    public enum ResultStatusValues
    {
        Pass,Fail
    }

    public enum GradeValues
    {
        A,B,C,D
    }
}