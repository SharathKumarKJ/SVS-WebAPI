using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppAngular5.Models;

namespace WebAppAngular5.Controllers
{
    public class StudentMarksSheetsController : ApiController
    {
        private Repository db = new Repository();

        // GET: api/StudentMarksSheets

        [HttpPost]
        [Route("api/GetStudentMarksSheets")]
        public IQueryable<StudentMarksSheet> GetStudentMarksSheets([FromBody] StudentMarksParam studentMarksParam)
        {
            if (studentMarksParam.ClassId != default(long) && studentMarksParam.StudentId != default(long))
            {
                return db.StudentMarksSheets
                    .Include("Subject")
                    .Where(x => x.IsActive && x.ClassDetailId == studentMarksParam.ClassId && x.StudentId == studentMarksParam.StudentId);
            }

            return db.StudentMarksSheets.Include("Subject");
        }

        // GET: api/StudentMarksSheets/5
        [ResponseType(typeof(StudentMarksSheet))]
        public async Task<IHttpActionResult> GetStudentMarksSheet(long id)
        {
            StudentMarksSheet studentMarksSheet = await db.StudentMarksSheets.FindAsync(id);

            if (studentMarksSheet == null)
            {
                return NotFound();
            }

            return Ok(studentMarksSheet);
        }

        // PUT: api/StudentMarksSheets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudentMarksSheet(long id, StudentMarksSheet studentMarksSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentMarksSheet.Id)
            {
                return BadRequest();
            }

            db.Entry(studentMarksSheet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentMarksSheetExists(id))
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

        // POST: api/StudentMarksSheets
        [ResponseType(typeof(StudentMarksSheet))]
        public async Task<IHttpActionResult> PostStudentMarksSheet(StudentMarksSheet studentMarksSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentMarksSheets.Add(studentMarksSheet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = studentMarksSheet.Id }, studentMarksSheet);
        }

        // DELETE: api/StudentMarksSheets/5
        [ResponseType(typeof(StudentMarksSheet))]
        public async Task<IHttpActionResult> DeleteStudentMarksSheet(long id)
        {
            StudentMarksSheet studentMarksSheet = await db.StudentMarksSheets.FindAsync(id);
            if (studentMarksSheet == null)
            {
                return NotFound();
            }

            db.StudentMarksSheets.Remove(studentMarksSheet);
            await db.SaveChangesAsync();

            return Ok(studentMarksSheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentMarksSheetExists(long id)
        {
            return db.StudentMarksSheets.Count(e => e.Id == id) > 0;
        }

        public class StudentMarksParam
        {
            public long ClassId { get; set; }

            public long StudentId { get; set; }


        }
    }
}