using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface ITypeProductAppService
    {
        /// <summary>
        /// Metodo para obtener tipos de productos con paginacion, ordenamiento y busquedas 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="offset"></param>
        /// <param name="limite"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<ResultadoPaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc");
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TypeProductDto>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<List<TypeProductDto>> GetAsync(string codigo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeProductDto"></param>
        /// <returns></returns>
        Task CreateAsync(CreateTypeProductDto TypeProductDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="putTypeProductDto"></param>
        /// <returns></returns>
        Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
    }
    public class ResultadoPaginacionTypeProduct<T>
    {
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
