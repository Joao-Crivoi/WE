using PhrasesApi.Models;

namespace PhrasesApi.NovaPasta
{
    public class LoginRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
