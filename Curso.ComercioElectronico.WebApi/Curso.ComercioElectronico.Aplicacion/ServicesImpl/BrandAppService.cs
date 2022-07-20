using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IGenericRepository<Brand> gnericRepository;
        private readonly IBrandRepository brand;
        private readonly IValidator<CreateBrandDto> validator;
        private readonly IMapper mapper;

        public BrandAppService(IGenericRepository<Brand> genericRepository, IValidator<CreateBrandDto> validator, IBrandRepository brand, IMapper mapper)
        {
            gnericRepository = genericRepository;
            this.validator = validator;
            this.brand = brand;
            this.mapper = mapper;
        }
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            var query = await gnericRepository.GetAsync();

            var result = query.Select(x => new BrandDto
            {
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                CreateDate = x.CreationDate
            });
            return result.ToList();
        }

        /// <summary>
        /// Obtiene las marcas por id
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public async Task<List<BrandDto>> GetAsync(string codigo)
        {
            var entity = await brand.GetAsync(codigo);
            var listBrandsDto = mapper.Map<List<BrandDto>>(entity);
            return listBrandsDto;
        }

        /// <summary>
        /// creacion de marcas
        /// </summary>
        /// <param name="brandDto"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            await validator.ValidateAndThrowAsync(brandDto);
            
            var brand = new Brand {
                Codigo = brandDto.Code,
                Nombre = brandDto.Description,
                CreationDate = DateTime.Now
            };
            await gnericRepository.CreateAsync(brand);
        }

        /// <summary>
        /// Borra MArcas ingresadas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var bran = await gnericRepository.GetAsync(id);

            await gnericRepository.DeleteAsync(bran);
            return true;
        }

        /// <summary>
        /// Actualiza marcas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="putBrandDto"></param>
        /// <returns></returns>
        public async Task UpdateAsync(string id, CreatePutBrandDto putBrandDto)
        {
            var bran = await gnericRepository.GetAsync(id);

            bran.Codigo = putBrandDto.Code;
            bran.Nombre = putBrandDto.Description;
            bran.CreationDate = DateTime.Now;

            await gnericRepository.UpdateAsync(bran);
        }

        public async Task<ResultadoPaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            var query = gnericRepository.GetQueryable();
            query = query.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Codigo.ToUpper().Contains(search)
                );
            }

            //1. Total 
            var total = await query.CountAsync(); 

            //2. Paginacion 
            query = query.Skip(offset).Take(limite);

            //3. Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToUpper())
                {
                    case "CODE":
                        query = query.OrderBy(x => x.Codigo);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new BrandDto
            {
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                CreateDate = x.CreationDate
            });

            var items = await result.ToListAsync();

            var resultado = new ResultadoPaginacionBrand<BrandDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }
    }
}

