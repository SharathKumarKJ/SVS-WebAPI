using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppAngular5.Models;

namespace WebAppAngular5.Controllers
{

    public class StudentsController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return _repository.Students.Include("ClassDetail");
         
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> GetStudent(long id)
        {
            Student student = await _repository.Students.Include("ClassDetail").FirstOrDefaultAsync(x => x.Id == id);
           
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }



        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(long id, Student student)
        {
            var userName = ((ClaimsIdentity)User.Identity).FindFirst("Username").Value;
            student.Updated = DateTime.UtcNow;
            student.UpdatedBy = _repository.Users.FirstOrDefault(x => x.UserName == userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }
            _repository.Entry(student).State = EntityState.Modified;
         


            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            var userName = ((ClaimsIdentity)User.Identity).FindFirst("Username").Value;
            student.Created = DateTime.UtcNow;
            student.CreatedBy = _repository.Users.FirstOrDefault(x => x.UserName == userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Students.Add(student);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> DeleteStudent(long id)
        {
            Student student = await _repository.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _repository.Students.Remove(student);
            await _repository.SaveChangesAsync();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(long id)
        {
            return _repository.Students.Count(e => e.Id == id) > 0;
        }
    }
}