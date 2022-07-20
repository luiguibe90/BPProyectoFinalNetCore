using Curso.CursoElectronico.Dominio.Base;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ComercioElectronicoDbContext _context;

        public GenericRepository(ComercioElectronicoDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //METODO PARA PAGINACION 

        /*Si tengo 20 elementos 
        5 elementos/pag  == pageSize o limit
        si tengo 4 paginas con 5 elemntos 
        en la pagina 4 tendria 16 al 20 elemnto 
        */
        public async Task<ICollection<T>> GetListaAsync(int limit = 10)
        {
            var consulta = _context.Set<T>()
                            .Take(limit);

            return await consulta.ToListAsync();
        }
        public async Task<T> GetAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();   //esta linea se hace para SIEMPRE confirmar el ccambio
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
        public async Task<IQueryable<Order>> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = _context.Orders.Where(o => o.Id == orderId).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            //order.Status = Status.Pagado.ToString();
            order.CreationDate = DateTime.Now;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return query;
        }

        public Task<T> AddProductOrder(T orderItem)
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
        /*
Task<T> IGenericRepository<T>.CreateAsync(T entity)
{
   throw new NotImplementedException();
}

Task<T> IGenericRepository<T>.UpdateAsync(T entity)
{
   throw new NotImplementedException();
}
*/
    }
}
