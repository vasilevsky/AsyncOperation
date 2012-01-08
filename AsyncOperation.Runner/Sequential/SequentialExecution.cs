using System;

namespace AsyncOperation.Runner.Sequential
{
    public class SequentialExecution : IExecutable
    {
        private readonly SequentialExecutionContext _sequentialExecutionContext;

        public SequentialExecution(IExecutionContext executionContext)
        {
            _sequentialExecutionContext = new SequentialExecutionContext(executionContext);
        }

        public ThenSequentialExecution First(IExecutable operation)
        {
            _sequentialExecutionContext.Register(operation);

            return new ThenSequentialExecution(_sequentialExecutionContext);
        }

        public event Action ExecutionCompleted;

        public void Execute()
        {
            _sequentialExecutionContext.Execute();
        }
    }
}