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

        // GET: api/StudentAttendances
        public IQueryable<StudentAttendance> GetStudentAttendances()
        {
            return _repository.StudentAttendances.Where(x=>x.IsActive);
        }

        // GET: api/StudentAttendances/5
        [ResponseType(typeof(StudentAttendance))]
        public async Task<IHttpActionResult> GetStudentAttendance(long id)
        {
            StudentAttendance studentAttendance = await _repository.StudentAttendances.FirstOrDefaultAsync(x=>x.Id==id && x.IsActive);
            if (studentAttendance == null)
            {
                return NotFound();
            }

            return Ok(studentAttendance);
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

            foreach (var studentId in studentAttendanceInfo.StudentIds.Split(','))
            {
                if (long.TryParse(studentId, out long result))
                {
                    studentAarrayIds.Add(result);
                }
            }

            var existingStudentAttendance = _repository
                .StudentAttendances.Where(x => x.IsActive && x.AttendanceDate == systemDate);

            var toBeDeleted = existingStudentAttendance
                .Where(x => !studentAarrayIds.Contains(x.StudentId));


            if (toBeDeleted.Any())
            {
                foreach (var student in toBeDeleted)
                {
                  
                    student.IsActive = false;
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

            var students = _repository.Students.Where(x => studentAarrayIds.Contains(x.Id)
            && x.ClassDetailId == studentAttendanceInfo.ClassId
            && x.IsActive).ToList();

            if (existingStudentAttendance.Any())
            {
                IQueryable<long> existingIds = existingStudentAttendance.Select(y => y.StudentId);
                students = students.Where(x => !existingIds.Contains(x.Id)).ToList();
            }

            var studentAttendances = new List<StudentAttendance>();

            foreach (var student in students)
            {

                var entity = new StudentAttendance
                {
                    Student = student,
                    AttendanceDate = systemDate,
                    IsActive = true,
                    IsPresent = true,
                    Created = DateTime.UtcNow,
                    CreatedBy = createdBy

                };

                studentAttendances.Add(entity);
            }

            _repository.StudentAttendances.AddRange(studentAttendances);
            await _repository.SaveChangesAsync();
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

        private bool StudentAttendanceExists(long id)
        {
            return _repository.StudentAttendances.Count(e => e.Id == id) > 0;
        }
    }

    public class StudentAttendanceInfo
    {
        public long ClassId { get; set; }

        public string StudentIds { get; set; }
    }
}