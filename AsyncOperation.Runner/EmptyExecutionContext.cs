using System;

namespace AsyncOperation.Runner
{
    public class EmptyExecutionContext : IExecutionContext
    {
        public event Action ExecutionCompleted;

        public void Execute()
        {
        }

        public IExecutionContext PrecendingContext { get; set; }
    }
}
