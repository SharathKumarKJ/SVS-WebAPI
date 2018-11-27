using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppAngular5.Models;

namespace WebAppAngular5.Controllers
{
    public class TeacherSubjectDetailsController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/TeacherSubjectDetails
        public IQueryable<TeacherSubjectDetail> GetTeacherSubjectDetails()
        {
            return _repository.TeacherSubjectDetails;
        }

        // GET: api/TeacherSubjectDetails/5
        [ResponseType(typeof(TeacherSubjectDetail))]
        public async Task<IHttpActionResult> GetTeacherSubjectDetail(long id)
        {
            TeacherSubjectDetail teacherSubjectDetail = await _repository.TeacherSubjectDetails.FindAsync(id);
            if (teacherSubjectDetail == null)
            {
                return NotFound();
            }

            return Ok(teacherSubjectDetail);
        }

        // PUT: api/TeacherSubjectDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeacherSubjectDetail(long id, TeacherSubjectDetail teacherSubjectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacherSubjectDetail.Id)
            {
                return BadRequest();
            }

            _repository.Entry(teacherSubjectDetail).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherSubjectDetailExists(id))
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

        // POST: api/TeacherSubjectDetails
        [ResponseType(typeof(TeacherSubjectDetail))]
        public async Task<IHttpActionResult> PostTeacherSubjectDetail(TeacherSubjectDetail teacherSubjectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.TeacherSubjectDetails.Add(teacherSubjectDetail);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teacherSubjectDetail.Id }, teacherSubjectDetail);
        }

        // DELETE: api/TeacherSubjectDetails/5
        [ResponseType(typeof(TeacherSubjectDetail))]
        public async Task<IHttpActionResult> DeleteTeacherSubjectDetail(long id)
        {
            TeacherSubjectDetail teacherSubjectDetail = await _repository.TeacherSubjectDetails.FindAsync(id);
            if (teacherSubjectDetail == null)
            {
                return NotFound();
            }

            _repository.TeacherSubjectDetails.Remove(teacherSubjectDetail);
            await _repository.SaveChangesAsync();

            return Ok(teacherSubjectDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherSubjectDetailExists(long id)
        {
            return _repository.TeacherSubjectDetails.Count(e => e.Id == id) > 0;
        }
    }
}