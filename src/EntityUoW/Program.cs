using EntityUoW.Data;
using EntityUoW.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Security.Cryptography;

Console.WriteLine("Entity com Unity of Work!");

// Configura o banco de dados em memória
var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "MemoryDatabase").Options;

List<Produto> produtos = new List<Produto>()
{
    new Produto { Id = 1, CodigoBarras = "7890123456789", Nome = "Camiseta", Preco = 29.90m },
    new Produto { Id = 2, CodigoBarras = "4567890123456", Nome = "Calça Jeans", Preco = 99.90m },
    new Produto { Id = 3, CodigoBarras = "1234567890123", Nome = "Tênis", Preco = 149.90m },
    new Produto { Id = 4, CodigoBarras = "9876543210987", Nome = "Livro", Preco = 49.90m },
    new Produto { Id = 5, CodigoBarras = "3210987654321", Nome = "Celular", Preco = 1999.90m },
    new Produto { Id = 6, CodigoBarras = "0123456789012", Nome = "Notebook", Preco = 3999.90m },
    new Produto { Id = 7, CodigoBarras = "2345678901234", Nome = "Mouse", Preco = 49.90m },
    new Produto { Id = 8, CodigoBarras = "5678901234567", Nome = "Teclado", Preco = 99.90m },
    new Produto { Id = 9, CodigoBarras = "8901234567890", Nome = "Monitor", Preco = 899.90m },
    new Produto { Id = 10, CodigoBarras = "1098765432109", Nome = "Fones de Ouvido", Preco = 199.90m }
};

using (var context = new AppDbContext(options))
{
    ProdutoRepository produtoRepository = new(context);
    produtoRepository.BatchInsert(produtos);
}

bool quit = false;
List<Produto> eProdutos = new();

while (quit != true)
{
    eProdutos.Clear();
    Console.Clear();
    Console.WriteLine("Entity com Unity of Work! \n");

    using (var context = new AppDbContext(options))
    {
        ProdutoRepository produtoRepository = new(context);
        eProdutos = produtoRepository.All();
    }

    foreach (var produto in eProdutos)
        Console.WriteLine($"Id: {produto.Id} | Codigo de Barras: {produto.CodigoBarras} | Nome: {produto.Nome} | Preco: {produto.Preco}");

    Console.WriteLine("\nEscolha uma opição: ");
    Console.WriteLine("1 - Alterar Preço");
    Console.WriteLine("2 - Remover ");
    Console.WriteLine("3 - Resetar ");
    Console.WriteLine("4 - Sair ");
    string opt = Console.ReadLine();

    switch (opt.ToLower())
    {
        case "1":
            {
                int sId = 0;
                decimal value = 0;
                Console.WriteLine("\nInforme o ID: ");
                sId = int.Parse(Console.ReadLine());
                Console.WriteLine("Qual o novo Preço:");
                value = decimal.Parse(Console.ReadLine());

                using (var context = new AppDbContext(options))
                {
                    ProdutoRepository produtoRepository = new(context);
                    produtoRepository.UpdateValue(sId, value);
                }
                break;
            }
        case "2":
            {
                int dId = 0;
                decimal value = 0;
                Console.WriteLine("\nInforme o ID: ");
                dId = int.Parse(Console.ReadLine());

                using (var context = new AppDbContext(options))
                {
                    ProdutoRepository produtoRepository = new(context);
                    produtoRepository.Delete(dId);
                }
                break;
            }
        case "3":
            {
                using (var context = new AppDbContext(options))
                {
                    ProdutoRepository produtoRepository = new(context);
                    produtoRepository.ClearDatabase();
                    produtoRepository.BatchInsert(produtos);
                }
                break;
            }
        case "4":
            {
                quit = true;
                break;
            }
        default:
            {
                break;
            }
    }

}