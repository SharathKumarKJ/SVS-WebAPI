using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAngular5.Models;

namespace WebAppAngular5.Base
{
    public interface IRepository
    {
         DbSet<Student> Students { get; set; }

         DbSet<ClassDetail> ClassDetails { get; set; }

         DbSet<Teacher> Teachers { get; set; }

         DbSet<Subject> Subjects { get; set; }

         DbSet<FeeDetail> FeeDetails { get; set; }

         DbSet<TeacherSubjectDetail> TeacherSubjectDetails { get; set; }


    }
}
