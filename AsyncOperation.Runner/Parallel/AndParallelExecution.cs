using System;

namespace AsyncOperation.Runner.Parallel
{
    public class AndParallelExecution : IExecutable
    {
        private readonly ParallelExecutionContext _executionContext;

        public AndParallelExecution(ParallelExecutionContext executionContext)
        {
            _executionContext = executionContext;
            _executionContext.ExecutionCompleted += delegate
                {
                    if (ExecutionCompleted != null)
                        ExecutionCompleted();
                };

            ThenExecute = new ExecutionType(_executionContext);
        }

        public event Action ExecutionCompleted;

        public ExecutionType ThenExecute { get; private set; }

        public AndParallelExecution And(IExecutable operation)
        {
            _executionContext.Register(operation);

            return new AndParallelExecution(_executionContext);
        }

        public void Execute()
        {
            _executionContext.Execute();
        }
    }
}