using System;

namespace AsyncOperation.Runner.Parallel
{
    public class ParallelExecution : IExecutable
    {
        ParallelExecutionContext _executionContext;


        public ParallelExecution(IExecutionContext executionContext)
        {
            _executionContext = new ParallelExecutionContext(executionContext);
        }

        public AndParallelExecution This(IExecutable operation)
        {
            _executionContext.Register(operation);

            return new AndParallelExecution(_executionContext);
        }

        public event Action ExecutionCompleted;

        public void Execute()
        {
            _executionContext.Execute();
        }
    }
}