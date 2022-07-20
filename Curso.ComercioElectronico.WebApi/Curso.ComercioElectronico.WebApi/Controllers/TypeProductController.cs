using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class TypeProductController : ControllerBase, ITypeProductAppService
    {
        private readonly ITypeProductAppService _typeProductAppService;
        public TypeProductController(ITypeProductAppService tpeProductAppService)
        {
            _typeProductAppService = tpeProductAppService;
        }
        [HttpGet]
        public Task<ICollection<TypeProductDto>> GetAllAsync()
        {
            return _typeProductAppService.GetAllAsync();
        }

        [HttpGet("Codigo")]
        public async Task<List<TypeProductDto>> GetAsync(string Codigo)
        {
            return await _typeProductAppService.GetAsync(Codigo);
        }

        [HttpPost]
        public Task CreateAsync(CreateTypeProductDto typeProductDto)
        {
            return _typeProductAppService.CreateAsync(typeProductDto);
        }
        [HttpPut("codigo")]
        public Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto)
        {
            return _typeProductAppService.UpdateAsync(id, putTypeProductDto);
        }
        [HttpDelete("codigo")]
        public Task<bool> DeleteAsync(string id)
        {
            return _typeProductAppService.DeleteAsync(id);
        }

        [HttpGet("Paginacion")]
        public Task<ResultadoPaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            return _typeProductAppService.GetListaAsync(search,offset,limite,sort,order);
        }

    }
}
