using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class TypeProductValidator : AbstractValidator<CreateTypeProductDto>
    {
        public TypeProductValidator()
        {
            RuleFor(x => x.Code).Length(3,4);
            RuleFor(x => x.Description).NotNull().Length(5,30);
        }
    }
}
