using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppAngular5.Models;

namespace WebAppAngular5.Controllers
{
    public class StudentAttendancesController : ApiController
    {
        private Repository _repository = new Repository();

        #region Public Methods
        // GET: api/StudentAttendances
        public IEnumerable<StudentAttendance> GetStudentAttendances()
        {
            var result = _repository.StudentAttendances
                .Include(x => x.Student)
                .Where(x => x.IsActive).ToList();

            return result;
        }

        // GET: api/StudentAttendances/5
        [ResponseType(typeof(StudentAttendance))]
        public async Task<IHttpActionResult> GetStudentAttendance(long id)
        {
            StudentAttendance studentAttendance = await _repository.StudentAttendances.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            if (studentAttendance == null)
            {
                return NotFound();
            }

            return Ok(studentAttendance);
        }

        [HttpPost]
        [Route("api/GetAttendanceByParam")]
        public IEnumerable<StudentAttendance> GetAttendanceByParams([FromBody] AttendanceParam attendanceParam)
        {
            if (attendanceParam == null)
            {
                var currentSystemDate = DateTime.Now;

                return _repository.StudentAttendances
                    .Where(x => x.IsActive && x.AttendanceDate == currentSystemDate).ToList();
            }

            return _repository.StudentAttendances
                 .Include(x => x.Student)
                 .Include(x => x.Student.ClassDetail)
                 .Where(x => x.IsActive
                 && (attendanceParam.AttendanceDate == null || x.AttendanceDate == attendanceParam.AttendanceDate)
                 && (attendanceParam.Name == null || x.Student.FirstName.Contains(attendanceParam.Name))
                 && (attendanceParam.ClassName == null || x.Student.ClassDetail.ClassName.Contains(attendanceParam.ClassName))).ToList();

        }
        // GET: api/GetAttendanceByDate

        [Route("api/GetAttendanceByDate/{date}")]
        public IQueryable<StudentAttendance> GetAttendanceByDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime dateTime))
            {
                return _repository.StudentAttendances.Include("Student")
                    .Where(x => x.IsActive && x.AttendanceDate == dateTime);
            }
            return _repository.StudentAttendances.Include("Student").Where(x => x.IsActive);
        }

        // GET: api/StudentsByClass/5

        [Route("api/GetAttendanceByClass/{classId}")]
        public IQueryable<StudentAttendance> GetAttendanceByClass(long classId)
        {
            var studentAttendances = classId != default(long)
                ? _repository.StudentAttendances.Include(x => x.Student).Where(x => x.IsActive && x.Student.ClassDetailId == classId)
                : _repository.StudentAttendances.Include(x => x.Student).Where(x => x.IsActive);

            return studentAttendances;
        }

        // PUT: api/StudentAttendances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudentAttendance(long id, StudentAttendance studentAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentAttendance.Id)
            {
                return BadRequest();
            }

            _repository.Entry(studentAttendance).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentAttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/StudentAttendances

        [HttpPost]
        [Route("api/StudentAttendanceByClass")]
        [ResponseType(typeof(StudentAttendance))]
        public async Task<IHttpActionResult> PostStudentAttendanceByClass(StudentAttendanceInfo studentAttendanceInfo)
        {
            var studentAarrayIds = new List<long>();

            DateTime systemDate = DateTime.Now.Date;

            var userName = ((ClaimsIdentity)User.Identity).FindFirst("Username").Value;

            var createdBy = _repository.Users.FirstOrDefault(x => x.UserName == userName && x.IsActive);

            ParseStudent(studentAttendanceInfo, studentAarrayIds);

            var existingStudentAttendance = _repository
                .StudentAttendances.Where(x => x.IsActive 
                && x.AttendanceDate == systemDate
                && x.Student.ClassDetailId == studentAttendanceInfo.ClassId).ToList();

          
            if (existingStudentAttendance.Any())
            {
                var absentStudents = existingStudentAttendance
                   .Where(x => x.IsPresent && !studentAarrayIds.Contains(x.StudentId)).ToList();

                if (absentStudents.Any())
                {
                    await UpdateAttendanceStatus(createdBy, absentStudents, false);
                }

                var presentStudents = existingStudentAttendance.Where(x=>studentAarrayIds.Contains(x.StudentId)).Except(absentStudents);
                if (presentStudents.Any())
                {
                    await UpdateAttendanceStatus(createdBy, presentStudents, true);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

            var students = _repository.Students.Where(x => x.ClassDetailId == studentAttendanceInfo.ClassId
       && x.IsActive).ToList();

            var studentsArePresent = students.Where(x => studentAarrayIds.Contains(x.Id));

            var studentsAreAbsent = students.Except(studentsArePresent);

            var studentAttendances = new List<StudentAttendance>();

            if (studentsArePresent.Any())
            {
                InsertStudentAttendance(systemDate, createdBy, studentsArePresent, studentAttendances, isPresent: true);
            }
            if (studentsAreAbsent.Any())
            {
                InsertStudentAttendance(systemDate, createdBy, studentsAreAbsent, studentAttendances, isPresent: false);
            }

            if (studentAttendances.Any())
            {
                _repository.StudentAttendances.AddRange(studentAttendances);

                await _repository.SaveChangesAsync();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentAttendances
        [ResponseType(typeof(StudentAttendance))]
        public async Task<IHttpActionResult> PostStudentAttendance(StudentAttendance studentAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.StudentAttendances.Add(studentAttendance);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = studentAttendance.Id }, studentAttendance);
        }

        // DELETE: api/StudentAttendances/5
        [ResponseType(typeof(StudentAttendance))]
        public async Task<IHttpActionResult> DeleteStudentAttendance(long id)
        {
            StudentAttendance studentAttendance = await _repository.StudentAttendances.FindAsync(id);
            if (studentAttendance == null)
            {
                return NotFound();
            }

            _repository.StudentAttendances.Remove(studentAttendance);
            await _repository.SaveChangesAsync();

            return Ok(studentAttendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods 
        private bool StudentAttendanceExists(long id)
        {
            return _repository.StudentAttendances.Count(e => e.Id == id) > 0;
        }
        private static void InsertStudentAttendance(DateTime systemDate, User createdBy, IEnumerable<Student> students, List<StudentAttendance> studentAttendances, bool isPresent)
        {
            foreach (var student in students)
            {
                var entity = new StudentAttendance
                {
                    Student = student,
                    AttendanceDate = systemDate,
                    IsActive = true,
                    IsPresent = isPresent,
                    Created = DateTime.UtcNow,
                    CreatedBy = createdBy

                };

                studentAttendances.Add(entity);
            }
        }
        private async Task UpdateAttendanceStatus(User createdBy, IEnumerable<StudentAttendance> students, bool IsPresent)
        {
            if (students.Any())
            {
                foreach (var student in students)
                {

                    student.IsActive = true;
                    student.IsPresent = IsPresent;
                    student.Updated = DateTime.UtcNow;
                    student.UpdatedBy = createdBy;
                    _repository.Entry(student).State = EntityState.Modified;
                }

                try
                {
                    await _repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
        }
        private static void ParseStudent(StudentAttendanceInfo studentAttendanceInfo, List<long> studentAarrayIds)
        {
            foreach (var studentId in studentAttendanceInfo.StudentIds.Split(','))
            {
                if (long.TryParse(studentId, out long result))
                {
                    studentAarrayIds.Add(result);
                }
            }
        }

        #endregion
    }

    #region Local Classes 
    public class StudentAttendanceInfo
    {
        public long ClassId { get; set; }

        public string StudentIds { get; set; }
    }

    public class AttendanceParam
    {
        public string ClassName { get; set; }

        public string Name { get; set; }

        public DateTime? AttendanceDate { get; set; }
    
    }
    #endregion
}