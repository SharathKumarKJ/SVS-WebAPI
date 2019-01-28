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
    public class FeeDetailsController : ApiController
    {
        private Repository _repository = new Repository();

        // GET: api/FeeDetails
        public IQueryable<FeeDetail> GetFeeDetails()
        {
            return _repository.FeeDetails.Include("Student").Where(x => x.IsActive);
        }

        [Route("api/FeesByClass/{classId}")]
        public IQueryable<FeeDetail> GetFeeDetailsByClass(long classId)
        {
            var fees = classId != default(long)
                ? _repository.FeeDetails.Include("Student").Where(x =>x.IsActive && x.Student.ClassDetailId == classId)
                : _repository.FeeDetails.Include("Student").Where(x=>x.IsActive);

            return fees;
        }

        // GET: api/FeeDetails/5
        [ResponseType(typeof(FeeDetail))]
        public async Task<IHttpActionResult> GetFeeDetail(long id)
        {
            FeeDetail feeDetail = await _repository.FeeDetails.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            if (feeDetail == null)
            {
                return NotFound();
            }

            return Ok(feeDetail);
        }

        // PUT: api/FeeDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFeeDetail(long id, FeeDetail feeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feeDetail.Id)
            {
                return BadRequest();
            }

            _repository.Entry(feeDetail).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeDetailExists(id))
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

        // POST: api/FeeDetails
        [ResponseType(typeof(FeeDetail))]
        public async Task<IHttpActionResult> PostFeeDetail(FeeDetail feeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.FeeDetails.Add(feeDetail);
            await _repository.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = feeDetail.Id }, feeDetail);
        }

        // DELETE: api/FeeDetails/5
        [ResponseType(typeof(FeeDetail))]
        public async Task<IHttpActionResult> DeleteFeeDetail(long id)
        {
            FeeDetail feeDetail = await _repository.FeeDetails.FindAsync(id);
            if (feeDetail == null)
            {
                return NotFound();
            }

            _repository.FeeDetails.Remove(feeDetail);
            await _repository.SaveChangesAsync();

            return Ok(feeDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeeDetailExists(long id)
        {
            return _repository.FeeDetails.Count(e => e.Id == id) > 0;
        }
    }
}