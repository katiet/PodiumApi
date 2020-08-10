using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podium.Models;

namespace Podium.Controllers
{
    [Route("api/lenders")]
    [ApiController]
    public class LendersController : ControllerBase
    {
        private readonly MortgageContext _context;
        private readonly ApplicantsController _applicantsController; 

        public LendersController(MortgageContext context, ApplicantsController applicantsController)
        {
            _context = context;
            _applicantsController = applicantsController;
        }

        // GET: api/Lenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lender>>> GetLenders()
        {
            return await _context.Lenders.ToListAsync();
        }

        // GET: api/Lenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lender>> GetLender(int id)
        {
            var lender = await _context.Lenders.FindAsync(id);

            if (lender == null)
            {
                return NotFound();
            }

            return lender;
        }

        // PUT: api/Lenders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLender(int id, Lender lender)
        {
            if (id != lender.Id)
            {
                return BadRequest();
            }

            _context.Entry(lender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LenderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lenders
        [HttpPost]
        public async Task<ActionResult<Lender>> PostLender(Lender lender)
        {
            _context.Lenders.Add(lender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLender", new { id = lender.Id }, lender);
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Lender>>> QueryLenders([FromBody] MortgageRequirement mortgageRequirement)
        {
            const int minimumAge = 18;

            // LTV ratio is calculated by dividing the amount borrowed by the appraised value of the property, expressed as a percentage
            var LTVratio = ((mortgageRequirement.PropertyValue - mortgageRequirement.DepositAmount) / mortgageRequirement.PropertyValue) * 100;

            var applicant = _context.Applicants.FirstOrDefault(x => x.Id == mortgageRequirement.ApplicantId);
     
            // if an incorrect applicant id is given or the applicant is under 18, no lenders are returned
            if(applicant is null || DateTime.Today.Year - applicant.DateOfBirth.Year < minimumAge)
            {
                return new EmptyResult();
            }
         
            return await _context.Lenders
                .Where(x => LTVratio < x.LoanToValue)
                 .ToListAsync();
        }

        // DELETE: api/Lenders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lender>> DeleteLender(int id)
        {
            var lender = await _context.Lenders.FindAsync(id);
            if (lender == null)
            {
                return NotFound();
            }

            _context.Lenders.Remove(lender);
            await _context.SaveChangesAsync();

            return lender;
        }

        private bool LenderExists(int id)
        {
            return _context.Lenders.Any(e => e.Id == id);
        }
    }
}
