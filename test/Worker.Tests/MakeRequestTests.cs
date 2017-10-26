using System;
using Moq;
using Shouldly;
using Worker.Execution;
using Xunit;

namespace Worker.Tests
{
    public class MakeRequestTests
    {
        [Fact]
        public void Should_return_non_nulluble_promise()
        {
            IWorker worker = new Worker(Mock.Of<IRequestExecutor>());
            var promise = worker.MakeRemoteRequest(new Request());
            promise.ShouldNotBeNull();
        }

        [Fact]
        public void Should_customize_request_execution()
        {
            var executorMock = new Mock<IRequestExecutor>();
            executorMock.Setup(x => x.Send(It.IsAny<Request>()));
            IWorker worker = new Worker(executorMock.Object);
            var request = new Request();
            worker.MakeRemoteRequest(request);

            executorMock.Verify(x => x.Send(request));
        }

        [Fact]
        public void Should_catch_all_errors_occured_in_execution()
        {
            var executorMock = new Mock<IRequestExecutor>();
            executorMock.Setup(x => x.Send(It.IsAny<Request>())).Throws(new Exception());
            IWorker worker = new Worker(executorMock.Object);
            Should.NotThrow(() => worker.MakeRemoteRequest(new Request()));
        }
    }
}
