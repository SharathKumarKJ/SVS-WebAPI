using System;
using System.Collections.Generic;
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

    public class SubjectsController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/Subjects
        public IQueryable<Subject> GetSubjects()
        {
            return _repository.Subjects.Where(x=>x.IsActive).Include("User");
        }

        // GET: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> GetSubject(long id)
        {
            Subject subject = await _repository.Subjects.FirstOrDefaultAsync(x=>x.Id == id && x.IsActive);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubject(long id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Id)
            {
                return BadRequest();
            }

            _repository.Entry(subject).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> PostSubject(Subject subject)
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            var UserName = identityClaims.FindFirst("Username").Value;

            subject.Created = DateTime.UtcNow;
            subject.CreatedBy = _repository.Users.FirstOrDefault(x => x.UserName == UserName && x.IsActive);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Subjects.Add(subject);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> DeleteSubject(long id)
        {
            Subject subject = await _repository.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _repository.Subjects.Remove(subject);
            await _repository.SaveChangesAsync();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(long id)
        {
            return _repository.Subjects.Count(e => e.Id == id) > 0;
        }
    }
}