using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class TypeProductRepository : ITypeProductRepository
    {
        private readonly ComercioElectronicoDbContext cntext;
        public TypeProductRepository(ComercioElectronicoDbContext context)
        { 
            cntext = context;
        
        }
        public async Task<ICollection<TypeProduct>> GetAsync()
        {
            return await cntext.TypeProducts.ToListAsync();
        }

        public async Task<List<TypeProduct>> GetAsync(string codigo)
        {
            var query = cntext.TypeProducts.AsQueryable();
            if (!string.IsNullOrEmpty(codigo))
            {
                query = from typeproduct in query
                        where typeproduct.Codigo.Contains(codigo) || typeproduct.Nombre.Contains(codigo)
                        select typeproduct;
            };
            return await query.ToListAsync();
        }
    }
}
