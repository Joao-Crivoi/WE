using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhrasesApi.Data;
using PhrasesApi.Models;


namespace PhrasesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produtos produto)
        {
            // O Front deve enviar o IdUsuarioCadastro
            if (produto.IdUsuarioCadastro == 0) return BadRequest("Usuário criador obrigatório");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Produtos produtoInput)
        {
            var produtoDb = await _context.Produtos.FindAsync(id);
            if (produtoDb == null) return NotFound();

            // Atualiza campos
            produtoDb.Nome = produtoInput.Nome;
            produtoDb.Preco = produtoInput.Preco;
            produtoDb.Status = produtoInput.Status;

            // Registra QUEM alterou (O front deve enviar IdUsuarioUpdate com o ID do logado)
            if (produtoInput.IdUsuarioUpdate == 0) return BadRequest("Usuário que alterou é obrigatório");
            produtoDb.IdUsuarioUpdate = produtoInput.IdUsuarioUpdate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produtos>>> GetAll() => await _context.Produtos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Produtos>> GetById(int id)
        {
            var prod = await _context.Produtos.FindAsync(id);
            return prod == null ? NotFound() : prod;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
