using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductAppService
    {
        /// <summary>
        /// Metodo para obtener productos con paginacion, ordenamiento y busquedas 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="offset"></param>
        /// <param name="limite"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<ResultadoPaginacion<ProductDto>> GetListaAsync(string? search="",int offset=0,int limite=15, string sort = "Name",string order = "asc");
        /// <summary>
        /// Metodo para obtener todos losprodcutos
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ProductDto>> GetAllAsync();

        /// <summary>
        /// METODO PARA PAGINACION
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<ICollection<ProductDto>> GetListaAsync(int limit=15);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ProductDto> GetAsync(Guid Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task CreateAsync(CreateProductDto productDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="putproductDto"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, CreateProductDto putproductDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
        
    }

    public class ResultadoPaginacion<T>
    { 
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
