using Curso.ComercioElectronico.Aplicacion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IOrderItemAppServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createOrderProduct"></param>
        /// <returns></returns>
        public Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderProduct);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderProduct);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<bool> RemoveProductAsync(Guid orderId, Guid productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<OrderDto> GetByIdAsync(Guid orderId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<OrderDto> PayAsync(Guid orderId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<OrderDto> CancelAsync(Guid orderId);

    }
}
