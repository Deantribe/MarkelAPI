using MarkelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsForCompanyController : ControllerBase
    {
        private readonly ClaimsContext _context;

        public ClaimsForCompanyController(ClaimsContext context)
        {
            _context = context;
        }

        // We need an endpoint that will give me a list of claims for one company 
        [HttpGet("{companyId}")]
        public async Task<ActionResult<IEnumerable<Claims>>> GetClaimsForCompany(int companyId)
        {
            if (_context.Claims == null) return NotFound();

            return await _context.Claims.Where(a => a.CompanyID == companyId).ToListAsync();
        }
    }
}
