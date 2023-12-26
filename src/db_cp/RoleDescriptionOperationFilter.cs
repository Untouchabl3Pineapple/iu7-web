using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace db_cp
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleDescriptionAttribute : Attribute
    {
        public string Role { get; }
        public string Description { get; }

        public RoleDescriptionAttribute(string role, string description)
        {
            Role = role;
            Description = description;
        }
    }

    public class RoleDescriptionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                var methodInfo = controllerActionDescriptor.MethodInfo;
                var roleDescriptionAttributes = methodInfo.GetCustomAttributes<RoleDescriptionAttribute>(true);

                if (roleDescriptionAttributes.Any())
                {
                    var roleDescriptions = roleDescriptionAttributes
                        .Select(attr => $"<b>{attr.Role}</b>");
                    var roleDescriptionsHtml = string.Join(", ", roleDescriptions);

                    // Используем HTML для выделения ролей
                    operation.Description += $"\n\nRoles: {roleDescriptionsHtml}";
                }
            }
        }
    }
}
