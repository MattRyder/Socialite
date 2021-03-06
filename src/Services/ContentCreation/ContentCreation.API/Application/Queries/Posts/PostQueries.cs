using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Posts
{
    public class PostQueries : IPostQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PostQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<PostViewModel>> FindAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findAllQuery = @"
                    SELECT Posts.Id, Posts.Title, Posts.Text, PostStates.Name as State, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostStates
                    ON Posts.StateId = PostStates.Id;";

                return await connection.QueryAsync<PostViewModel>(findAllQuery);
            }
        }

        public async Task<PostViewModel> FindAsync(Guid id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findByIdQuery = @"
                    SELECT Posts.Id, Posts.Title, Posts.Text, PostStates.Name as State, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostStates
                    ON Posts.StateId = PostStates.Id
                    WHERE Posts.Id = @Id";

                var result = await connection.QueryAsync<PostViewModel>(findByIdQuery, new { Id = id });

                if (result.Count() == 0)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}
