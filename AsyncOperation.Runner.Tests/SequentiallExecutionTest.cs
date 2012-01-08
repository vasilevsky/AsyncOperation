using AsyncOperation.Runner.Tests.OperationMockups;

using NUnit.Framework;

namespace AsyncOperation.Runner.Tests
{
    [TestFixture]
    public class SequentiallExecutionTest
    {
        [Test]
        public void EachExecutedOnceTest()
        {
            var first = new SuccessfulOperation<int>(0x1);
            var second = new SuccessfulOperation<int>(0x10);
            var third = new SuccessfulOperation<int>(0x100);

            var aggregate = 0x0;
            first.OnSuccess = result => aggregate += result;
            second.OnSuccess = result => aggregate += result;
            third.OnSuccess = result => aggregate += result;

            Run.OneByOne
                .First(first)
                .Then(second)
                .Then(third)
                .Execute();

            Assert.That(aggregate, Is.EqualTo(0x111));
        }

        [Test]
        public void NotifiesWhenAllCompleted()
        {
            var first = new SuccessfulOperation<int>(1);
            var second = new SuccessfulOperation<bool>(true);
            var third = new SuccessfulOperation<double>(0.3);

            var operations = Run.OneByOne
                                .First(first)
                                .Then(second)
                                .Then(third);

            var operationCompleted = false;
            operations.ExecutionCompleted += delegate { operationCompleted = true; };
            operations.Execute();

            Assert.That(operationCompleted, Is.True);
        }

        [Test]
        public void ExecutesInCorrectOrderTest()
        {
            var first = new SuccessfulOperation<int>(0x1);
            var second = new SuccessfulOperation<int>(0x10);
            var third = new SuccessfulOperation<int>(0x100);

            var aggregate = 0x0;
            first.OnSuccess = result =>
                {
                    Assert.That(aggregate, Is.EqualTo(0x0));
                    aggregate += result;
                };

            second.OnSuccess = result =>
                {
                    Assert.That(aggregate, Is.EqualTo(0x1));
                    aggregate += result;
                };

            third.OnSuccess = result => Assert.That(aggregate, Is.EqualTo(0x11));

            Run.OneByOne
                .First(first)
                .Then(second)
                .Then(third)
                .Execute();
        }
    }
}