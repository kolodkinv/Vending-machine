using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vending_Machine.Models.Product;

namespace Vending_Machine.Repositories.EF
{
    public class ProductRepository: IRepository<Product>
    {
        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}