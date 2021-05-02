﻿using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistAsync(int artistId);
        Task<IEnumerable<Artist>> GetArtistsAsync();
    }
}