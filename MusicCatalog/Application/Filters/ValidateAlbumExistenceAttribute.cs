using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Repositories;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class ValidateAlbumExistenceAttribute : TypeFilterAttribute
    {
        public ValidateAlbumExistenceAttribute() : base(typeof(ValidateAlbumExistenceFilterImpl))
        {
        }

        private class ValidateAlbumExistenceFilterImpl : IAsyncActionFilter
        {
            private readonly IAlbumRepository albumRepo;

            public ValidateAlbumExistenceFilterImpl(IAlbumRepository albumRepo)
            {
                this.albumRepo = albumRepo;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("albumId"))
                {
                    var albumId = context.ActionArguments["albumId"] as int?;
                    if (albumId.HasValue)
                    {
                        if ((await albumRepo.GetAlbumAsync(albumId.Value)) == null)
                        {
                            context.Result = new NotFoundObjectResult(albumId.Value);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
