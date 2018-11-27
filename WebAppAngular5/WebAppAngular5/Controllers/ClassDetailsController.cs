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

    public class ClassDetailsController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/ClassDetails

        public IQueryable<ClassDetail> GetClassDetails()
        {
            return _repository.ClassDetails;
        }

        // GET: api/ClassDetails/5
        [ResponseType(typeof(ClassDetail))]
        public async Task<IHttpActionResult> GetClassDetail(long id)
        {
            ClassDetail classDetail = await _repository.ClassDetails.FindAsync(id);
            if (classDetail == null)
            {
                return NotFound();
            }

            return Ok(classDetail);
        }

        // PUT: api/ClassDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClassDetail(long id, ClassDetail classDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classDetail.Id)
            {
                return BadRequest();
            }

            _repository.Entry(classDetail).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassDetailExists(id))
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

        // POST: api/ClassDetails
        [ResponseType(typeof(ClassDetail))]
        public async Task<IHttpActionResult> PostClassDetail(ClassDetail classDetail)
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            var UserName = identityClaims.FindFirst("Username").Value;

            classDetail.Created = DateTime.UtcNow;
            classDetail.CreatedBy = _repository.Users.FirstOrDefault(x => x.UserName == UserName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.ClassDetails.Add(classDetail);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = classDetail.Id }, classDetail);
        }

        // DELETE: api/ClassDetails/5
        [ResponseType(typeof(ClassDetail))]
        public async Task<IHttpActionResult> DeleteClassDetail(long id)
        {
            ClassDetail classDetail = await _repository.ClassDetails.FindAsync(id);
            if (classDetail == null)
            {
                return NotFound();
            }

            _repository.ClassDetails.Remove(classDetail);
            await _repository.SaveChangesAsync();

            return Ok(classDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassDetailExists(long id)
        {
            return _repository.ClassDetails.Count(e => e.Id == id) > 0;
        }
    }
}