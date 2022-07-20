using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class BrandValidator : AbstractValidator<CreateBrandDto>
    {
        public BrandValidator()
        {
            
            RuleFor(x => x.Code).Matches("^[a-zA-Z0-9-]*$").WithMessage("El codigo no cumple con las condiciones");
                
            RuleFor(x => x.Description).NotNull().MaximumLength(256).WithMessage("La descripcion no puede ser vacia!");

            RuleFor(x => x.Description).Must(a => ValidateDescription(a))
                .WithMessage("La descripcion debe contener al menos 10 letras"); 
        }
        private bool ValidateDescription(string desc) 
        {
            if (desc.Length<10 && desc.Length > 30)
            {
                return false;
            }
            return true;
        }
    }
}
