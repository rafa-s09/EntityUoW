namespace EntityUoW.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
