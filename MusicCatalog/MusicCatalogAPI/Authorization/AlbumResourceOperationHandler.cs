using Microsoft.AspNetCore.Authorization;
using MusicCatalogAPI.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Authorization
{
    public class AlbumResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, Album>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Album resource)
        {
            if (requirement.OperationType == OperationType.Create)
            {
                context.Succeed(requirement);
            }

            var supplierId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (resource.SupplierId == int.Parse(supplierId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
