using MarkelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimsContext _context;

        public ClaimsController(ClaimsContext context)
        {
            _context = context;
        }

        // We need an endpoint that will give me the details of one claim.We need a property to be returned that tells us how old the claim is in days
        [HttpGet("{ucr}")]
        public async Task<ActionResult<ClaimDetails>> GetClaim(string ucr)
        {
            if (_context.Claims == null) return NotFound();

            var claim = await _context.Claims.FindAsync(ucr);

            if (claim == null) return NotFound();

            return new ClaimDetails(claim);
        }

        // We need an endpoint that will allow us to update a claim
        [HttpPut("{ucr}")]
        public async Task<IActionResult> UpdateClaim(string ucr, Claims claim)
        {
            if (ucr != claim.UCR) return BadRequest();

            _context.Entry(claim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Claims?.Any(a => a.UCR == ucr)).GetValueOrDefault())
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
