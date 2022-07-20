using Curso.ComercioElectronico.Aplicacion;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class BrandController : ControllerBase, IBrandAppService
    {
        private readonly IBrandAppService brandAppService;
        private readonly IValidator<CreateProductDto> validator;
        public BrandController(IBrandAppService bandAppService, IValidator<CreateProductDto> validator)
        {
            this.brandAppService = bandAppService;
            this.validator = validator;
        }
        /*
        [HttpGet]
        public async Task<ICollection<Brand>> GetAsync()
        {
            return await _brandAppService.GetAsync();
        }
        */
        [HttpGet]
        //[Authorize(Roles = "Admin,SupportTechnical")]
        //[Authorize(Policy = "EsEcuatoriano")]
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            return await brandAppService.GetAllAsync();
        }

        [HttpGet("Code/Name")]
        public async Task<List<BrandDto>> GetAsync(string Codigo)
        {
            return await brandAppService.GetAsync(Codigo);
        }
        [HttpGet("Pagination")]
        public Task<ResultadoPaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            return brandAppService.GetListaAsync(search, offset, limite, sort, order);
        }

        [HttpPost]
        public Task CreateAsync(CreateBrandDto brandDto)
        {   
                return brandAppService.CreateAsync(brandDto);
            
        }

        [HttpDelete("Code")]
        public Task<bool> DeleteAsync(string Codigo)
        {
            return brandAppService.DeleteAsync(Codigo);
        }

        [HttpPut("Code")]
        public Task UpdateAsync(string Codigo, CreatePutBrandDto brandDto)
        {
            return brandAppService.UpdateAsync(Codigo, brandDto);
        }
        
    }
}
