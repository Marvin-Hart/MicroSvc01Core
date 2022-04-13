using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroSvc01Core.Context;
using MicroSvc01Core.Models;

namespace MicroSvc01Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogContext _context;
        private readonly ILogger<SystemInfoController> _logger;
        private readonly IWebHostEnvironment _env;

        public DogsController(DogContext context, ILogger<SystemInfoController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        // ------------------------------------------------------------------------------------------
        // Crud - (Create)
        // ------------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog(Dog dog)
        {
            _context.Dog.Add(dog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDog", new { id = dog.ID }, dog);
        }

        // ------------------------------------------------------------------------------------------
        // cRud - (Read List)
        // ------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDog()
        {
            return await _context.Dog.ToListAsync();
        }

        // ------------------------------------------------------------------------------------------
        // cRud - (Read)
        // ------------------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(int id)
        {
            var dog = await _context.Dog.FindAsync(id);

            if (dog == null)
            {
                return NotFound();
            }

            return dog;
        }

        // ------------------------------------------------------------------------------------------
        // crUd - (Update)
        // ------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog(int id, Dog dog)
        {
            if (id != dog.ID)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
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

        // ------------------------------------------------------------------------------------------
        // crud - (Delete)
        // ------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog(int id)
        {
            var dog = await _context.Dog.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }

            _context.Dog.Remove(dog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DogExists(int id)
        {
            return _context.Dog.Any(e => e.ID == id);
        }
    }
}
