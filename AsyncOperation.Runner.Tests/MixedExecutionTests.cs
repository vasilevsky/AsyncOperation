using AsyncOperation.Runner.Tests.OperationMockups;

using NUnit.Framework;

namespace AsyncOperation.Runner.Tests
{
    [TestFixture]
    public class MixedExecutionTests
    {
        [Test]
        public void SequentialExecutionStartsAfterParallelAndViceVersaTest()
        {
            var first = new SuccessfulOperation<int>(0x1);
            var second = new SuccessfulOperation<int>(0x10);
            var third = new SuccessfulOperation<int>(0x100);
            var fourth = new SuccessfulOperation<int>(0x1000);
            var fifth = new SuccessfulOperation<int>(0x10000);

            var aggregate = 0x0;
            first.OnSuccess = result => aggregate += result;
            second.OnSuccess = result => aggregate += result;

            third.OnSuccess = result =>
                {
                    Assert.That(aggregate, Is.EqualTo(0x11));
                    aggregate += result;
                };

            fourth.OnSuccess = result => aggregate += result;

            fifth.OnSuccess = result => Assert.That(aggregate, Is.EqualTo(0x1111));

            Run.InParallel
                    .This(first)
                    .And(second)

                .ThenExecute
                .OneByOne
                    .First(third)
                    .Then(fourth)

                .ThenExecute
                .InParallel
                    .This(fifth)

                .Execute();
        }

        [Test]
        public void ParallelExecutionStartsAfterSequentialAndViceVersaTest()
        {
            var first = new SuccessfulOperation<int>(0x1);
            var second = new SuccessfulOperation<int>(0x10);
            var third = new SuccessfulOperation<int>(0x100);
            var fourth = new SuccessfulOperation<int>(0x1000);
            var fifth = new SuccessfulOperation<int>(0x10000);

            var aggregate = 0x0;
            first.OnSuccess = result => aggregate += result;
            second.OnSuccess = result => aggregate += result;

            third.OnSuccess = result =>
            {
                Assert.That(aggregate, Is.EqualTo(0x11));
                aggregate += result;
            };

            fourth.OnSuccess = result => aggregate += result;

            fifth.OnSuccess = result => Assert.That(aggregate, Is.EqualTo(0x1111));

            Run.OneByOne
                    .First(first)
                    .Then(second)

                .ThenExecute
                .InParallel
                    .This(third)
                    .And(fourth)

                .ThenExecute
                .OneByOne
                    .First(fifth)

                .Execute();
        }
    }
}
