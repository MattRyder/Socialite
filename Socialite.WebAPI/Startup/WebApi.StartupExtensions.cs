using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Events.Statuses;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Repositories;
using Socialite.Infrastructure.Responses;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Commands.Statuses;
using Socialite.WebAPI.Application.Queries.Albums;
using Socialite.WebAPI.Application.Queries.Posts;
using Socialite.WebAPI.Extensions;
using Socialite.WebAPI.Queries.Posts;
using Socialite.WebAPI.Queries.Statuses;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddSocialiteWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Socialite");

            services
            .AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            })
            .AddDbContext<SocialiteDbContext>(opts => {
                opts.UseMySql(connectionString);

                opts.EnableSensitiveDataLogging();
            });

            services.AddTransient<IStatusRepository, StatusRepository>()
                    .AddTransient<IRequestHandler<CreateStatusCommand, BasicResponse>, CreateStatusCommandHandler>()
                    .AddTransient<IStatusQueries, StatusQueries>();

            services.AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                    .AddTransient<IPostQueries, PostQueries>();

            services.AddTransient<IAlbumRepository, AlbumRepository>()
                    .AddTransient<IRequestHandler<CreateAlbumCommand, bool>, CreateAlbumCommandHandler>()
                    .AddTransient<IRequestHandler<UploadPhotoCommand, bool>, UploadPhotoCommandHandler>()
                    .AddTransient<IAlbumQueries, AlbumQueries>();
        }

        public static void UseSocialiteWebApi(this IApplicationBuilder app)
        {
            app.ApplyMigrations<SocialiteDbContext>();
        }
    }
}