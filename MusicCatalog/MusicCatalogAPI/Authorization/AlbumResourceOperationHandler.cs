using Microsoft.AspNetCore.Authorization;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Authorization
{
    public class AlbumResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, AlbumDetailsDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, AlbumDetailsDto resource)
        {
            if (requirement.OperationType == OperationType.Create)
            {
                context.Succeed(requirement);
            }

            var supplierId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            

            if (resource.Supplier.Id == int.Parse(supplierId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
