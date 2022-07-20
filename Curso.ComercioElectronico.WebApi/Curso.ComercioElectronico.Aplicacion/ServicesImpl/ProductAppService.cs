using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Infraestructura.Repositories;
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
    public class ProductAppService : IProductAppService
    {
        private readonly IGenericRepository<Product> gnericRepository;
        private readonly IMapper mapper;

        public ProductAppService(IGenericRepository<Product> genericRepository, IMapper mapper)
        {            
            gnericRepository = genericRepository;
            this.mapper = mapper;
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var query = gnericRepository.GetQueryable();
            var result = query.Select(x => new ProductDto
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                Stock =x.Stock,
                TypeProduct = x.TypeProduct.Nombre,
                Brand = x.Brand.Nombre
            });
            return await result.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDto> GetAsync(Guid id)
        {
            var query = gnericRepository.GetQueryable();
            //Filtrando la informacion por el ID
            query = query.Where(x => x.Id == id);

            var result = query.Select(x => new ProductDto
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                Stock = x.Stock,
                TypeProduct = x.TypeProduct.Nombre,
                Brand = x.Brand.Nombre
            });
            return await result.SingleOrDefaultAsync();
            /////Utilizamos el Single por que al menos uno existe en la base de datos 
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Nombre = productDto.Name,
                Descripcion = productDto.Description,
                Precio = productDto.Price,
                Stock = productDto.Stock,
                BrandId = productDto.BrandId,
                TypeProductId = productDto.TypeProductId,
                CreationDate = DateTime.Now
            };            
            await gnericRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(Guid id, CreateProductDto putProductDto)
        {
            var product = await gnericRepository.GetAsync(id);

            product.Nombre = putProductDto.Name;
            product.Descripcion = putProductDto.Description;
            product.Precio = putProductDto.Price;
            product.Stock = putProductDto.Stock;
            product.BrandId = putProductDto.BrandId;
            product.TypeProductId = putProductDto.TypeProductId;

            await gnericRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await gnericRepository.GetAsync(id);
            await gnericRepository.DeleteAsync(product);
            return true;

        }

        /// <summary>
        /// IMPLEMENTACION DEL METODO DE PAGINACION
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<ICollection<ProductDto>> GetListaAsync(int limit = 10)
        {
            var consulta = await gnericRepository.GetListaAsync(limit);
            var resultLINQUDto = consulta.Select(x => new ProductDto()
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                Stock = x.Stock
            });
            return resultLINQUDto.ToList();
        }

        public async Task<ResultadoPaginacion<ProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Name", string order = "asc")
        {
            var query = gnericRepository.GetQueryable();
            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted==false);

            //0. BUsqueda
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Nombre.ToUpper().Contains(search)
                
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
                    case "NAME":
                        query = query.OrderBy(x => x.Nombre);
                        break;
                    case "PRICE":
                        query = query.OrderBy(x => x.Precio);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new ProductDto
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                Stock = x.Stock,
                TypeProduct = x.TypeProduct.Nombre,
                Brand = x.Brand.Nombre
            });
            var items = await result.ToListAsync();
            var resultado = new ResultadoPaginacion<ProductDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }
    }
}
