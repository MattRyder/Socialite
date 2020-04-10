using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Posts;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class CreatePostCommandHandlerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        private readonly Mock<ILogger<CreatePostCommandHandler>> _loggerMock;

        public CreatePostCommandHandlerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();

            _loggerMock = new Mock<ILogger<CreatePostCommandHandler>>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var post = PostFactory.Create();

            var createPostCmd = new CreatePostCommand(post.AuthorId, post.Title, post.Text);

            _postRepositoryMock.Setup(p => p.Add(post)).Returns(post);

            _postRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreatePostCommandHandler(_postRepositoryMock.Object, _loggerMock.Object);

            var result = await handler.Handle(createPostCmd, default);

            Assert.True(result);
        }
    }
}