using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ComercioElectronicoDbContext cntext;

        public ProductRepository(ComercioElectronicoDbContext context)
        {
            cntext = context;
        }


        public async Task<ICollection<Product>> GetAsync()
        {

            return await cntext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid ID)
        {
            return await cntext.Products.FindAsync(ID);
        }

        public async Task<IQueryable<Product>> GetQueryableByIdAsync(Guid id)
        {
            var query = cntext.Products.Where(x => x.Id == id);
            Product productExist = await query.SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"No existe producto con id: {id}");
            }
            return query;
        }

        public async Task<IQueryable<Product>> PostAsync(Product product)
        {
            var query = cntext.Products.Where(b => b.Nombre == product.Nombre);
            if (product.Stock < 0)
                throw new ArgumentException("El stock debe ser mayor igual a 0");

            await cntext.Products.AddAsync(product);
            await cntext.SaveChangesAsync();
            return query;

        }

        public async Task<IQueryable<Product>> PutAsync(Product product)
        {
            var query = cntext.Products.Where(b => b.Id == product.Id);
            bool productExist = cntext.Products.Any(b => b.Id == product.Id );
            if (!productExist)
                throw new ArgumentException($"No existe producto con id: {product.Id}");

            if (product.Stock < 0)
                throw new ArgumentException("El stock debe ser mayor igual a 0");

            product.ModifiedDate = DateTime.Now;
            await cntext.SaveChangesAsync();
            return query;

        }
        public  async Task<bool> DeleteByIdAsync(Guid id)
        {
            Product productExist = await cntext.Products.Where(b => b.Id == id ).SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"No existe producto con id: {id}");
            }
            productExist.ModifiedDate = DateTime.Now;

            cntext.Products.Update(productExist);
            await cntext.SaveChangesAsync();
            return true;

        }
    }
}
