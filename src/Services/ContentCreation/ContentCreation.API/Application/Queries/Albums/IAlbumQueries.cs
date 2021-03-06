using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Albums
{
    public interface IAlbumQueries
    {
        Task<IEnumerable<AlbumViewModel>> FindAllAsync();

        Task<AlbumViewModel> FindAsync(Guid id);
    }
}