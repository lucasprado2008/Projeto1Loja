namespace Projeto1Loja.Models
{
    public class Produto
    {
        public int idProduto { get; set; }
        public string? nome { get; set; }
        public string? descricao { get; set; }
        public decimal preco { get; set; }
        public decimal quantidade { get; set; }
    }
}
