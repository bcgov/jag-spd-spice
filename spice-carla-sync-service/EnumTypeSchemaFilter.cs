using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class EnumTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var typeInfo = context.Type.GetTypeInfo();

            if (typeInfo.IsEnum)
            {
                schema.Extensions.Add(
                    "x-ms-enum",
                    getOpenApiObject(typeInfo)
                );

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
