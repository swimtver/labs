using Shouldly;
using Worker.Promises;
using Xunit;

namespace Worker.Tests
{
    public class PromisesTests
    {
        [Fact]
        public void Can_invoke_handler()
        {
            IPromise promise = new Promise();
            var invoked = false;
            promise.Then(() => { invoked = true; });

            invoked.ShouldBeTrue();
        }
    }
}