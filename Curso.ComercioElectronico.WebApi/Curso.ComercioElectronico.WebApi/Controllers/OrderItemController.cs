using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemController : ControllerBase, IOrderItemAppServices
    {

        private readonly IOrderItemAppServices _OrderItemAppService;

        public OrderItemController(IOrderItemAppServices orderItemAppService)
        {
            _OrderItemAppService = orderItemAppService;
        }
        [HttpPost("CreateOrder")]
        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderItem)
        {
            return await _OrderItemAppService.AddProductAsync(createOrderItem);
        }

        [HttpPut("{orderId}")]
        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            return await _OrderItemAppService.CancelAsync(orderId);

        }

        [HttpGet("{orderId}")]
        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            return await _OrderItemAppService.GetByIdAsync(orderId);
        }

        [HttpPut("{orderId}/Delete/{productId}")]
        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            return await _OrderItemAppService.PayAsync(orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await _OrderItemAppService.RemoveProductAsync(orderId, productId);
        }

        [HttpPut("{orderId}/Update")]
        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderItem)
        {
            return await _OrderItemAppService.UpdateProductAsync(orderId, orderItem);
        }
    }
}
