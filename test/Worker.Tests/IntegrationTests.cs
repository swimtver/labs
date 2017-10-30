using System;
using System.Threading;
using Shouldly;
using Worker.Execution;
using Xunit;

namespace Worker.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void Try_to_make_request()
        {
            var running = true;
            var timer = new Timer(state =>
            {
                running = false;
            }, null, -1, -1);

            IWorker worker = new Worker(new HttpRequestExecutor());

            var invoked = false;
            var errorOccured = false;

            var promise = worker.MakeRemoteRequest(new Request(new Uri("http://ya.ru"), "GET"));
            promise.Then(
                res =>
                {
                    invoked = true;
                    running = false;
                    res.StatusCode.ShouldBe(200);
                },
                ex =>
                {
                    errorOccured = true;
                    running = false;
                });
            timer.Change(2000, -1);
            while (running)
            {
                Thread.Sleep(10);
            }
            timer.Dispose();
            (invoked || errorOccured).ShouldBeTrue();
        }
    }
}