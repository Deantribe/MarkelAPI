using MarkelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ClaimsContext _context;
        public CompanyController(ClaimsContext context)
        {
            _context = context;
        }

        // We need an endpoint that will give me a single company. We need a property to be returned that will tell us if the company has an active insurance policy 
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDetails>> GetCompany(int id)
        {
            if (_context.Companies == null) return NotFound();

            var company = await _context.Companies.FindAsync(id);

            if (company == null) return NotFound();

            return new CompanyDetails(company);
        }
    }
}
