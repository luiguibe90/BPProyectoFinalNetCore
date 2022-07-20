using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IBrandAppService
    {
        //Obtenemos las marcas de cada producto usando paginacion con ordenamiento y busquedas 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="offset"></param>
        /// <param name="limite"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<ResultadoPaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc");
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BrandDto>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<List<BrandDto>> GetAsync(string codigo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandDto"></param>
        /// <returns></returns>
        Task CreateAsync(CreateBrandDto brandDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="putBrandDto"></param>
        /// <returns></returns>
        Task UpdateAsync(string id,CreatePutBrandDto putBrandDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
        

    }
    public class ResultadoPaginacionBrand<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<T> Item { get; set; } = new List<T>();
    }
}
