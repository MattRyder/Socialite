using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.DTO;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Controllers;
using Socialite.WebAPI.Queries.Status;
using Xunit;
using Socialite.WebAPI.Application.Commands.Statuses;
using System.Net;
using System.Threading;
using System;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.UnitTests.Controllers
{
    public class StatusControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IStatusQueries> _statusQueries;

        public StatusControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _statusQueries = new Mock<IStatusQueries>();
        }

        [Fact]
        public async void Get_ReturnsOk()
        {
            var statusList = StatusFactory.CreateList();

            IEnumerable<StatusDTO> statusDTOList = statusList.ConvertAll<StatusDTO>(s => StatusDTO.FromModel(s));

            _statusQueries.Setup(sq => sq.FindAllAsync()).Returns(Task.FromResult(statusDTOList));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Get() as OkObjectResult;

            var resultValue = actionResult.Value as IEnumerable<StatusDTO>;

            Assert.NotNull(actionResult);

            Assert.NotNull(resultValue);

            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResult.StatusCode.GetValueOrDefault());

            Assert.Equal(statusList.Count, resultValue.Count());
        }

        [Fact]
        public async void Get_ReturnsStatus_GivenAnId()
        {
            var expectedStatusId = 1;

            var status = StatusDTO.FromModel(StatusFactory.Create());

            status.Id = expectedStatusId;

            _statusQueries.Setup(sq => sq.FindStatus(expectedStatusId)).Returns(Task.FromResult(status));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Get(expectedStatusId) as OkObjectResult;

            var resultObject = actionResult.Value as StatusDTO;

            Assert.NotNull(actionResult);

            Assert.NotNull(resultObject);

            Assert.Equal(status, resultObject);

            Assert.NotNull(actionResult);
        }

        [Fact]
        public async void Get_ReturnsNotFound_GivenInvalidId()
        {
            var statusId = 1;

            _statusQueries.Setup(sq => sq.FindStatus(statusId)).Throws(new KeyNotFoundException());

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Get(statusId) as NotFoundResult;

            Assert.NotNull(actionResult);

            Assert.Equal((int)HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsOk_GivenValidStatus()
        {
            var status = StatusFactory.Create();

            var createStatusCmd = new CreateStatusCommand(status.Mood, status.Text);

            _mediator.Setup(m => m.Send(It.IsAny<CreateStatusCommand>(), new CancellationToken())).Returns(Task.FromResult(true));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Post(createStatusCmd) as OkResult;

            Assert.Equal((int)HttpStatusCode.OK, actionResult.StatusCode);
        }

        [Fact]
        public async void Post_ReturnsBadRequest_GivenInvalidBody()
        {
            _mediator.Setup(m => m.Send(It.IsAny<CreateStatusCommand>(), new CancellationToken())).Returns(Task.FromResult(false));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Post(null) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsOk_GivenValidId()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DeleteStatusCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.Success));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Delete(1) as OkResult;

            Assert.Equal((int)HttpStatusCode.OK, actionResult.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsNotFound_GivenInvalidId()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DeleteStatusCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.NotFound));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Delete(1) as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [Fact]
        public async void Delete_ReturnsBadRequest_WhenCommandFails()
        {
            _mediator.Setup(m => m.Send(It.IsAny<DeleteStatusCommand>(), default(CancellationToken))).Returns(Task.FromResult(DeleteCommandResult.Failure));

            var controller = new StatusesController(_mediator.Object, _statusQueries.Object);

            var actionResult = await controller.Delete(1) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }
    }
}