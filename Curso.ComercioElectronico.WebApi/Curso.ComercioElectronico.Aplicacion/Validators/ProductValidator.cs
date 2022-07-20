using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(4,50);
            RuleFor(x => x.Description).NotNull().Length(5, 50);
            RuleFor(x => x.Price).NotNull();
        }
    }
}
