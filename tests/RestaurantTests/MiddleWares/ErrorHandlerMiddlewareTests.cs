using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.MiddleWares.Tests
{
    public class ErrorHandlerMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsyncTest_withNoExceptions_ShouldCallNextdelegate()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var requestdelegate = new Mock<RequestDelegate>();

            //act
            await middleware.InvokeAsync(context, requestdelegate.Object);

            //assert
            requestdelegate.Verify(d => d.Invoke(context), Times.Once);
        }

        [Fact()]
        public async Task InvokeAsyncTest_withNotFoundException_ShouldReturn404()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var notFoundException = new NotFoundExceptionHandler(nameof(Restaurantt), "2");

            //act
            await middleware.InvokeAsync(context, _ => throw notFoundException);

            //assert
            context.Response.StatusCode.Should().Be(404);
        }

        [Fact()]
        public async Task InvokeAsyncTest_withForbiddenException_ShouldReturn403()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var forbiddenException = new ForbidException();

            //act
            await middleware.InvokeAsync(context, _ => throw forbiddenException);

            //assert
            context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async Task InvokeAsyncTest_witGenericException_ShouldReturn500()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var exception = new Exception();

            //act
            await middleware.InvokeAsync(context, _ => throw exception);

            //assert
            context.Response.StatusCode.Should().Be(500);
        }
    }
}