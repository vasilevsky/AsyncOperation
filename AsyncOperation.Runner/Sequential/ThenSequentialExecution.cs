using System;

namespace AsyncOperation.Runner.Sequential
{
    public class ThenSequentialExecution : IExecutable
    {
        private readonly SequentialExecutionContext _sequentialExecutionContext;

        public ThenSequentialExecution(SequentialExecutionContext sequentialExecutionContext)
        {
            _sequentialExecutionContext = sequentialExecutionContext;
            _sequentialExecutionContext.ExecutionCompleted += delegate
                {
                    if (ExecutionCompleted != null)
                        ExecutionCompleted();
                };



            ThenExecute = new ExecutionType(_sequentialExecutionContext);
        }

        public event Action ExecutionCompleted;

        public ExecutionType ThenExecute { get; set; }

        public ThenSequentialExecution Then(IExecutable operation)
        {
            _sequentialExecutionContext.Register(operation);

            return new ThenSequentialExecution(_sequentialExecutionContext);
        }

        public ThenSequentialExecution<T> Then<T>(IAsyncOperation<T> operation)
        {
            _sequentialExecutionContext.Register(operation);

            return new ThenSequentialExecution<T>(_sequentialExecutionContext);
        }

        public ThenSequentialExecution Then(object precendingResult, IExecutable operation)
        {
            _sequentialExecutionContext.Register(operation);

            return new ThenSequentialExecution(_sequentialExecutionContext);
        }

        public void Execute()
        {
            _sequentialExecutionContext.Execute();
        }
    }

    public class ThenSequentialExecution<T> : IExecutable
    {
        private readonly SequentialExecutionContext _sequentialExecutionContext;

        public ThenSequentialExecution(SequentialExecutionContext sequentialExecutionContext)
        {
            _sequentialExecutionContext = sequentialExecutionContext;
            _sequentialExecutionContext.ExecutionCompleted += delegate
            {
                if (ExecutionCompleted != null)
                    ExecutionCompleted();
            };

            ThenExecute = new ExecutionType(_sequentialExecutionContext);
        }

        public event Action ExecutionCompleted;

        public ExecutionType ThenExecute { get; set; }

        public void Execute()
        {
            _sequentialExecutionContext.Execute();
        }

        public ThenSequentialExecution Then(IExecutable operation)
        {
            _sequentialExecutionContext.Register(operation);

            return new ThenSequentialExecution(_sequentialExecutionContext);
        }
    }
}