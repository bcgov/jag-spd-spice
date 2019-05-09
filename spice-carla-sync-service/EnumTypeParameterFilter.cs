﻿using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class EnumTypeParameterFilter : IParameterFilter
    {
        public void Apply(IParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum)
            {
                parameter.Extensions.Add("x-ms-enum", new { name = type.Name, modelAsString = false });
            }                
        }
    }    
}
