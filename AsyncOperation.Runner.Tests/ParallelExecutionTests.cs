using System;

using AsyncOperation.Runner.Tests.OperationMockups;
using AsyncOperation.SampleOperations;

using NUnit.Framework;

namespace AsyncOperation.Runner.Tests
{
    [TestFixture]
    public class ParallelExecutionTests
    {
        [Test]
        public void NoExecutionIfExecuteNotCalledTest()
        {
            var testOperation = new SuccessfulOperation<bool>(true);

            Run.InParallel.This(testOperation);

            Assert.That(testOperation.Executed, Is.False);
        }

        [Test]
        public void ExecuteDoesActualExecutionTest()
        {
            var testOperation = new SuccessfulOperation<bool>(true);

            Run.InParallel
                .This(testOperation)
                .Execute();

            Assert.That(testOperation.Executed);
        }

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

            Run.InParallel
                .This(first)
                .And(second)
                .And(third)
                .Execute();

            Assert.That(aggregate, Is.EqualTo(0x111));
        }

        [Test]
        public void NotifiesWhenAllCompleted()
        {
            var first = new SuccessfulOperation<int>(1);
            var second = new SuccessfulOperation<bool>(true);
            var third = new SuccessfulOperation<double>(0.3);

            var operations = Run.InParallel
                                .This(first)
                                .And(second)
                                .And(third);

            var operationCompleted = false;
            operations.ExecutionCompleted += delegate { operationCompleted = true; };
            operations.Execute();

            Assert.That(operationCompleted, Is.True);
        }

        [Test]
        public void EachOperationGetsItsOwnExceptionIfOccuredTest()
        {
            var first = new FailOperation(new ArgumentException());
            var second = new FailOperation(new InvalidOperationException());
            var noException = new SuccessfulOperation<int>(5);

            var noExceptionNeverInvoked = true;
            first.OnError = error => Assert.That(error, Is.TypeOf<ArgumentException>());
            second.OnError = error => Assert.That(error, Is.TypeOf<InvalidOperationException>());
            noException.OnError = error => noExceptionNeverInvoked = false;

            var operations = Run.InParallel
                .This(first)
                .And(second)
                .And(noException);

            operations.Execute();

            Assert.That(first.Executed, Is.True);
            Assert.That(second.Executed, Is.True);
            Assert.That(noException.Executed, Is.True);
            Assert.That(noExceptionNeverInvoked, Is.True);
        }

        [Test]
        public void DoesNotCatchExceptionsDuringExecitionTest()
        {
            var operation = new SimpleOperation();
            operation.ActionToExecute = delegate
                {
                    throw new AggregateException();
                };

            Assert.Throws<AggregateException>(
                () =>
                Run.InParallel
                    .This(operation)
                    .Execute());
        }

        [Test]
        public void CanHandleErrorDuringExecutionTest()
        {
            var aggregateException = new AggregateException();
            var failOperation = new FailOperation(aggregateException);

            var errorHandlerExecuted = false;
            failOperation.OnError = exception =>
                {
                    errorHandlerExecuted = true;
                    Assert.That(exception, Is.EqualTo(aggregateException));
                };

            Run.InParallel.This(failOperation).Execute();

            Assert.That(errorHandlerExecuted, Is.True);
        }
    }
}