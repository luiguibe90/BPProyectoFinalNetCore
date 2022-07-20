using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{

    public class DeliveryMethodAppServices : IDeliveryMethodAppService
    {
        private readonly IGenericRepository<DeliveryMethod> gnericRepository;
        public DeliveryMethodAppServices(IGenericRepository<DeliveryMethod> gnericRepository)
        {
            this.gnericRepository = gnericRepository;
        }
        public async Task<ICollection<DeliveryMethodDto>> GetAllAsync()
        {
            var query = await gnericRepository.GetAsync();

            var result = query.Select(x => new DeliveryMethodDto
            {
                Code = x.Codigo,
                Name = x.Name,
                Description = x.Description,
                CreateDate = x.CreationDate
            });
            return result.ToList();
        }
        public async Task<DeliveryMethodDto> GetAsync(string codigo)
        {
            var entity = await gnericRepository.GetAsync(codigo);
            return new DeliveryMethodDto
            {
                Code = entity.Codigo,
                Name = entity.Name,
                Description = entity.Description,
                CreateDate = entity.CreationDate
            };
        }
        public async Task CreateAsync(CreateDeliveryMethodDto deliveryMethodDto)
        {
            var deliveryMethod = new DeliveryMethod
            {
                Codigo = deliveryMethodDto.Code,
                Name = deliveryMethodDto.Name,
                Description = deliveryMethodDto.Description
                
            };
            await gnericRepository.CreateAsync(deliveryMethod);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deliveryMethod = await gnericRepository.GetAsync(id);

            await gnericRepository.DeleteAsync(deliveryMethod);
            return true;
        }

        public async Task UpdateAsync(string id, CreateDeliveryMethodDto putDeliveryMethodDto)
        {
            var deliveryMethod = await gnericRepository.GetAsync(id);

            deliveryMethod.Codigo = putDeliveryMethodDto.Code;
            deliveryMethod.Name = putDeliveryMethodDto.Name;
            deliveryMethod.Description = putDeliveryMethodDto.Description;

            await gnericRepository.UpdateAsync(deliveryMethod);
        }
    }
}
