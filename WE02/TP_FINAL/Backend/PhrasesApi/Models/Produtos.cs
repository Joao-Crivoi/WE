namespace PhrasesApi.Models
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Status { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public int? IdUsuarioUpdate { get; set; }
    }
}
