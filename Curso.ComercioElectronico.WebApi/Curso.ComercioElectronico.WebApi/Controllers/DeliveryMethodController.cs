using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : ControllerBase, IDeliveryMethodAppService
    {
        private readonly IDeliveryMethodAppService deliveryMethodAppService;
        public DeliveryMethodController(IDeliveryMethodAppService deliveryMethodAppService)
        {
            this.deliveryMethodAppService = deliveryMethodAppService;
        }
        [HttpGet]
        public async Task<ICollection<DeliveryMethodDto>> GetAllAsync()
        {
            return await deliveryMethodAppService.GetAllAsync();
        }
        [HttpGet("Code")]
        public async Task<DeliveryMethodDto> GetAsync(string codigo)
        {
            return await deliveryMethodAppService.GetAsync(codigo);
        }
        [HttpPost]
        public  Task CreateAsync(CreateDeliveryMethodDto deliveryMethodDto)
        {
            return deliveryMethodAppService.CreateAsync(deliveryMethodDto);
        }
        [HttpDelete("Code")]
        public  Task<bool> DeleteAsync(string id)
        {
            return deliveryMethodAppService.DeleteAsync(id);
        }
        [HttpPut("Code")]
        public  Task UpdateAsync(string id, CreateDeliveryMethodDto putDeliveryMethodDto)
        {
            return deliveryMethodAppService.UpdateAsync(id, putDeliveryMethodDto);
        }
    }
}
