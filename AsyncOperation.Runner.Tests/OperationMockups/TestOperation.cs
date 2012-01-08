using System;

namespace AsyncOperation.Runner.Tests.OperationMockups
{
    public class TestOperation<T> : IAsyncOperation<T>
    {
        public TestOperation()
        {
            ActionToExecute = delegate
                {
                    Executed = true;

                    if (ExecutionCompleted != null) ExecutionCompleted();
                };
        }

        public bool Executed { get; set; }

        public int ExecutedTimes { get; set; }

        public event Action ExecutionCompleted;

        public void Execute()
        {
            if (ActionToExecute != null) ActionToExecute();
        }

        public Action ActionToExecute { get; set; }

        public Action<Exception> OnError { get; set; }

        public Action<T> OnSuccess { get; set; }
    }
}
