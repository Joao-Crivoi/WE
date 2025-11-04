using System.ComponentModel.DataAnnotations;

namespace ProdutosApp.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = "";

        [Display(Name = "Descrição")]
        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "Preço deve ser maior que 0")]
        public decimal Preco { get; set; }

        [Display(Name = "Quantidade em estoque")]
        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser 0 ou maior")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
