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
    public class TypeProductAppService : ITypeProductAppService
    {
        private readonly IGenericRepository<TypeProduct> gnericRepository;
        private readonly ITypeProductRepository type;
        private readonly IMapper mapper;

        public TypeProductAppService(IGenericRepository<TypeProduct> genericRepository, ITypeProductRepository type, IMapper mapper)
        {
            gnericRepository = genericRepository;
            this.type = type;
            this.mapper = mapper;
        }
        public async Task<ICollection<TypeProductDto>> GetAllAsync()
        {
            var query = await type.GetAsync();

            var listTypeDto = mapper.Map<List<TypeProductDto>>(query);
            return listTypeDto.ToList();
        }

        public async Task<List<TypeProductDto>> GetAsync(string codigo)
        {
            var entity = await type.GetAsync(codigo);
            var listTypeDto = mapper.Map<List<TypeProductDto>>(entity);
            return listTypeDto;

        }

        public async Task CreateAsync(CreateTypeProductDto typeProductDto)
        {
            var typeProduct = new TypeProduct {
                Codigo = typeProductDto.Code,
                Nombre = typeProductDto.Description,
                CreationDate = DateTime.Now
        };

            await gnericRepository.CreateAsync(typeProduct);
        }
        public async Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto)
        {
            var type = await gnericRepository.GetAsync(id);

            type.Codigo = putTypeProductDto.Code;
            type.Nombre = putTypeProductDto.Description;
            type.CreationDate = DateTime.Now;

            await gnericRepository.UpdateAsync(type);            
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var type = await gnericRepository.GetAsync(id);
            await gnericRepository.DeleteAsync(type);

            return true;
        }
    
        public async Task<ResultadoPaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            var query = gnericRepository.GetQueryable();
            
            query = query.Where(x => x.IsDeleted == false);

            //0. BUsqueda
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
                //Soporta por el nombre

                //sort => nombre, precio si es otro lanza excepcion 
                switch (sort.ToUpper())
                {
                    case "CODE":
                        query = query.OrderBy(x => x.Codigo);
                        break;
                    
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new TypeProductDto
            {
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                CreateDate = x.CreationDate
            });
            var items = await result.ToListAsync();
            var resultado = new ResultadoPaginacionTypeProduct<TypeProductDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }

        
    }
}
