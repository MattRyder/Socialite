using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Enums;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class DeletePostCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public DeletePostCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _postRepositoryMock = new Mock<IPostRepository>();
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_GivenValidCommandAsync()
        {
            var postId = 1;

            var post = PostFactory.Create();

            post.Id = postId;

            var deletePostCmd = new DeletePostCommand(postId);

            _postRepositoryMock.Setup(p => p.FindAsync(postId)).Returns(Task.FromResult(post));

            _postRepositoryMock.Setup(p => p.Delete(It.IsAny<Post>()));

            _postRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(deletePostCmd, default(CancellationToken));

            Assert.Equal(DeleteCommandResult.Success, result);
        }

        [Fact]
        public async Task Handle_ReturnsNotFound_GivenInvalidId()
        {
            var deletePostCmd = new DeletePostCommand(1);

            _postRepositoryMock.Setup(p => p.FindAsync(It.IsAny<int>())).Returns(Task.FromResult((Post)null));

            var handler = new DeletePostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(deletePostCmd, default(CancellationToken));

            Assert.Equal(DeleteCommandResult.NotFound, result);
        }
    }
}