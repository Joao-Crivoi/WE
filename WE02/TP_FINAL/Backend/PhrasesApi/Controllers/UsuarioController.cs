using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhrasesApi.Data;
using PhrasesApi.Models;
using PhrasesApi.NovaPasta;

namespace PhrasesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // Rota: GET /api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Rota: GET /api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // Rota: POST /api/usuarios/auth
        [HttpPost("auth")]
        public async Task<ActionResult> Authenticate([FromBody] LoginRequest login)
        {
            if (string.IsNullOrEmpty(login.Nome) || string.IsNullOrEmpty(login.Senha))
                return BadRequest("Nome e senha obrigatórios.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Nome == login.Nome &&
                    u.Senha == login.Senha &&
                    u.Status);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = Guid.NewGuid().ToString();

            return Ok(new
            {
                user = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Status
                },
                token
            });
        }

        [HttpPost]
        // O atributo [FromBody] força o ASP.NET a ler o JSON inteiro do corpo
        // da requisição e desserializar (converter) diretamente para o objeto Usuario.
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] Usuario usuario)
        {
            // 1. Garante que o ID é zero para que o banco de dados gere o novo ID
            usuario.Id = 0;

            // Adiciona ao contexto
            _context.Usuarios.Add(usuario);

            // 2. Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // Retorna 201 CreatedAtAction, indicando sucesso
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }


        // Rota: PUT /api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("O ID na rota não corresponde ao ID no corpo da requisição.");
            }

            // Busca o usuário existente para atualização (boas práticas)
            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            try
            {
                // Atualiza apenas os campos permitidos
                usuarioExistente.Nome = usuario.Nome;

                // Se a senha foi enviada no corpo, atualiza
                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    usuarioExistente.Senha = usuario.Senha;
                }

                usuarioExistente.Status = usuario.Status;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 No Content para PUT bem-sucedido
        }


        // Rota: DELETE /api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

