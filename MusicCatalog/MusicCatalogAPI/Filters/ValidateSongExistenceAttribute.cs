using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicCatalogAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Filters
{
    public class ValidateSongExistenceAttribute : TypeFilterAttribute
    {
        public ValidateSongExistenceAttribute() : base(typeof(ValidateSongExistenceFilterImpl))
        {
        }

        private class ValidateSongExistenceFilterImpl : IAsyncActionFilter
        {
            private readonly ISongRepository songRepo;

            public ValidateSongExistenceFilterImpl(ISongRepository songRepo)
            {
                this.songRepo = songRepo;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("albumId") && context.ActionArguments.ContainsKey("songId"))
                {
                    var albumId = context.ActionArguments["albumId"] as int?;
                    var songId = context.ActionArguments["songId"] as int?;

                    if (albumId.HasValue && songId.HasValue)
                    {
                        if ((await songRepo.GetSongAsync(albumId.Value, songId.Value)) == null)
                        {
                            context.Result = new NotFoundObjectResult(songId.Value);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
