using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate
{
    public class AlbumDomainException : Exception
    {
        public AlbumDomainException(string message) : base(message)
        {
        }
    }
}