using Curso.ComercioElectronico.Aplicacion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IDeliveryMethodAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<DeliveryMethodDto>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<DeliveryMethodDto> GetAsync(string codigo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryMethodDto"></param>
        /// <returns></returns>
        Task CreateAsync(CreateDeliveryMethodDto deliveryMethodDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="putDeliveryMethodDto"></param>
        /// <returns></returns>
        Task UpdateAsync(string id, CreateDeliveryMethodDto putDeliveryMethodDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
    }
}
