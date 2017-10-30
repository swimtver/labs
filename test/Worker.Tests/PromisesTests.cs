using System;
using Shouldly;
using Worker.Promises;
using Xunit;

namespace Worker.Tests
{
    public class PromisesTests
    {
        [Fact]
        public void Should_register_handler_and_invoke_after_resolving()
        {
            var promise = new Promise<Unit>();
            var invoked = false;
            promise.Then(x => { invoked = true; });

            invoked.ShouldBeFalse();
            promise.Resolve(Unit.Value);
            invoked.ShouldBeTrue();
        }

        [Fact]
        public void Should_invoke_handler_for_allready_resolved_promise()
        {
            var promise = new Promise<Unit>();
            var invoked = false;
            promise.Resolve(Unit.Value);
            promise.Then(x => { invoked = true; });
            invoked.ShouldBeTrue();
        }

        [Fact]
        public void Should_invoke_chain_of_handlers()
        {
            var promise = new Promise<Unit>();
            var invoked = false;
            var invokeAgain = false;
            promise.Resolve(Unit.Value);
            promise
                .Then(x => { invoked = true; })
                .Then(x => { invokeAgain = true; });
            invoked.ShouldBeTrue();
            invokeAgain.ShouldBeTrue();
        }

        [Fact]
        public void Should_invoke_handler_for_rejected_promise()
        {
            var promise = new Promise<Unit>();
            var resolved = false;
            var rejected = false;
            var message = "error";
            promise.Reject(new Exception(message));
            promise
                .Then(x => { resolved = true; }, error =>
                {
                    message.ShouldBeSameAs(error.Message);
                    rejected = true;
                });
            resolved.ShouldBeFalse();
            rejected.ShouldBeTrue();
        }

        [Fact]
        public void Should_register_chain_of_handlers_and_invoke_after_resolving()
        {
            var promise = new Promise<Unit>();
            var invoked = 0;

            promise
                .Then((x) => { invoked++; })
                .Then((x) => { invoked++; });

            promise.Resolve(Unit.Value);
            invoked.ShouldBe(2);
        }

        [Fact]
        public void Should_invoke_chain_of_handlers_with_transform()
        {
            var promise = new Promise<int>();
            var invoked = false;
            var expected = 1;
            promise
                .Then(x =>
                {
                    x.ShouldBe(expected);
                    return expected.ToString();
                })
                .Then(x =>
                {
                    invoked = true;
                    x.ShouldBe("1");
                });
            promise.Resolve(1);
            invoked.ShouldBeTrue();
        }

        [Fact]
        public void Should_handle_exception_in_handler()
        {
            var promise = new Promise<Unit>();
            var catched = false;
            var message = "error";
            promise.Resolve(Unit.Value);

            promise
                .Then(x => throw new Exception(message))
                .Then(null, error =>
                {
                    message.ShouldBeSameAs(error.Message);
                    catched = true;
                });
            catched.ShouldBeTrue();
        }
    }
}