using System;

namespace AsyncOperation.SampleOperations
{
    public class SimpleOperation : IAsyncOperation
    {
        public Action ActionToExecute { get; set; }

        public Action<Exception> OnError { get; set; }

        public event Action ExecutionCompleted;

        public void Execute()
        {
            if (ActionToExecute != null) ActionToExecute();
        }
    }
}
