using Curso.CursoElectronico.Dominio.Base;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //Listar todos los objetos de una entidad
        Task<ICollection<T>> GetAsync();

        //para paginacion
        Task<ICollection<T>> GetListaAsync(int limit = 10);

        //Obtener un objeto sobre su clave primaria 
        Task<T> GetAsync(string code);

        Task<T> AddProductOrder(T orderItem);

        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
