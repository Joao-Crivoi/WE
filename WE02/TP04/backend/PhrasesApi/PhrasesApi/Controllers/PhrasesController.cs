using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhrasesApi.Data;
using PhrasesApi.Models;
using PhraseUtils;

namespace PhrasesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhrasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhrasesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Phrases.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null) return NotFound();
            return Ok(phrase);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phrase phrase)
        {
            phrase.Content = TextProcessor.Normalize(phrase.Content);

            if (!TimeoutChecker.ShouldUpdate())
                return BadRequest("Espere 10 segundos para enviar outra frase.");

            _context.Phrases.Add(phrase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = phrase.Id }, phrase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Phrase phrase)
        {
            var existing = await _context.Phrases.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Content = TextProcessor.Normalize(phrase.Content);
            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null) return NotFound();

            _context.Phrases.Remove(phrase);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
