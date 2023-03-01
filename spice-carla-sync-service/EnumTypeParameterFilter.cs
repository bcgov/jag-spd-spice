using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class EnumTypeParameterFilter : IParameterFilter
    {

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum)
            {
                parameter.Extensions.Add("x-ms-enum", getOpenApiObject(type));
            }                
        }

        // https://stackoverflow.com/a/60536660
        private IOpenApiAny getOpenApiObject(Type type)
        {
            return new OpenApiObject
            {
                [ "name" ] = new OpenApiString( type.Name),
                [ "modelAsString" ] = new OpenApiBoolean(true)
            };

        }
    }    
}
