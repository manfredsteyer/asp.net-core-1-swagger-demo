using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Model;
using Microsoft.AspNetCore.Authorization;

namespace FlugDemo.Swagger
{
    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var controllerAuth = context
                                    .ApiDescription
                                    .GetControllerAttributes()
                                    .OfType<AuthorizeAttribute>()
                                    .Any();

            var actionAuth = context
                                    .ApiDescription
                                    .GetActionAttributes()
                                    .OfType<AuthorizeAttribute>()
                                    .Any();

            if (!controllerAuth && !actionAuth) return;

            operation.Summary += " (OAuth2)";


            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new [] { "voucher" } }
                };

            operation.Security.Add(oAuthRequirements);
        }
    }
}
