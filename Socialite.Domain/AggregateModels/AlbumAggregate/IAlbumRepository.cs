using System.Threading.Tasks;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.AlbumAggregate
{
    public interface IAlbumRepository : IRepository<Album>
    {
        ValueTask<Album> FindAsync(int id);

        Album Add(Album album);

        void Delete(Album album);
    }
}