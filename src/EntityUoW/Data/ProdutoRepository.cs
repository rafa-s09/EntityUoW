using EntityUoW.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityUoW.Data
{
    public class ProdutoRepository(DbContext dbContext)
    {
        private readonly DbContext _context = dbContext;

        public void BatchInsert(List<Produto> produtos)
        {
            using RepositoryBase<Produto> repository = new(_context);
            repository.BatchInsert(produtos);
        }

        public List<Produto> All()
        {
            using RepositoryBase<Produto> repository = new(_context);
            return repository.All().ToList();
        }

        public void UpdateValue(int id, decimal value)
        {
            using RepositoryBase<Produto> repository = new(_context);
            var prod = repository.GetById(id);
            if (prod != null)
            {
                prod.Preco = value;
                repository.Update(prod);
            }
        }

        public void Delete(int id)
        {
            using RepositoryBase<Produto> repository = new(_context);
            repository.DeleteById(id);
        }

        public void ClearDatabase()
        {
            using RepositoryBase<Produto> repository = new(_context);
            var list = repository.All().ToList();
            repository.BatchDelete(list);
        }

    }
}
