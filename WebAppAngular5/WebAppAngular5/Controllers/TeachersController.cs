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

    public class TeachersController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/Teachers
        public IQueryable<Teacher> GetTeachers()
        {
            return _repository.Teachers.Where(x=>x.IsActive);
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> GetTeacher(long id)
        {
            Teacher teacher = await _repository.Teachers.FirstOrDefaultAsync(x=>x.Id == id && x.IsActive);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeacher(long id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return BadRequest();
            }

            _repository.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> PostTeacher(Teacher teacher)
        {
            var userName = ((ClaimsIdentity)User.Identity).FindFirst("Username").Value;
            teacher.Created = DateTime.UtcNow;
            teacher.CreatedBy = _repository.Users.FirstOrDefault(x => x.UserName == userName && x.IsActive);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Teachers.Add(teacher);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> DeleteTeacher(long id)
        {
            Teacher teacher = await _repository.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _repository.Teachers.Remove(teacher);
            await _repository.SaveChangesAsync();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(long id)
        {
            return _repository.Teachers.Count(e => e.Id == id) > 0;
        }
    }
}