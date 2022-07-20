using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class OrderItemAppService : IOrderItemAppServices
    {
        private readonly IOrderRepository context;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public OrderItemAppService(IOrderRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Ad productos nuevos
        /// </summary>
        /// <param name="createOrderItem"></param>
        /// <returns></returns>
        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderItem)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(createOrderItem);
            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(await context.AddProductAsync(orderItem));
            return orderItemDto;

        }

        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            IQueryable<Order> query = await context.CancelAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            return orderDto;
        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            var query = context.GetByIdAsync(orderId);
            if (await query.SingleOrDefaultAsync() == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            return await GetResult(query);

        }

        public async Task<OrderDto> GetResult(IQueryable<Order> query)
        {
            decimal total = 0;
            OrderDto orderDto = await query.Select(o => new OrderDto()
            {
                Code = o.Id,
                DeliveryMethodId = o.DeliveryMethodId,
                DeliveryMethod = o.DeliveryMethod,
                orderItems = o.orderItems.Select(op => new OrderItemResultDto()
                {
                    Product = op.Product.Nombre,
                    Price = op.Product.Precio,
                    QuantityProduct = op.QuantityProduct,
                    Total = op.Total
                }).ToList(),
                Subtotal = o.Subtotal,
                TotalPrice = o.TotalPrice
            }).SingleOrDefaultAsync();
            foreach (var product in orderDto.orderItems)
            {
                total += product.Total;
            }
            orderDto.Subtotal = total - (total * (decimal)0.12);
            orderDto.TotalPrice = total;
            return orderDto;
        }
        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = await context.PayAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            await context.UpdateOrderAsync(orderId, orderDto.Subtotal, orderDto.TotalPrice);
            return orderDto;

        }

        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await context.RemoveProductAsync(orderId, productId);

        }
        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderItemDto)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(orderItemDto);
            OrderItemDto orderItemResultDto = mapper.Map<OrderItemDto>(await context.UpdateProductAsync(orderId, orderItem));
            return orderItemResultDto;
        }
    }
}
