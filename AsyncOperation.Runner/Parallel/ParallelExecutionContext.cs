using System;
using System.Collections.Generic;

namespace AsyncOperation.Runner.Parallel
{
    public class ParallelExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Instance of empty execution context.
        /// </summary>
        public static readonly EmptyExecutionContext Empty = new EmptyExecutionContext();

        /// <summary>
        /// Keeps number of operations queued to run in parallel.
        /// </summary>
        private int _numberOfOperations;

        private List<IExecutable> _operations = new List<IExecutable>();

        public event Action ExecutionCompleted;

        public ParallelExecutionContext(IExecutionContext precendingContext)
        {
            PrecendingContext = precendingContext;
        }

        public IExecutionContext PrecendingContext { get; set; }

        public void Register(IExecutable operation)
        {
            _operations.Add(operation);

            _numberOfOperations = _operations.Count;

            // subscribing for ExecutionCompleted event
            // so we can know that each operation executed
            operation.ExecutionCompleted += delegate
                {
                    --_numberOfOperations;

                    if (_numberOfOperations > 0) return;

                    if (ExecutionCompleted != null) ExecutionCompleted();
                };
        }

        public void Execute()
        {
            if (PrecendingContext != Empty && PrecendingContext != this)
            {
                var precendingContext = PrecendingContext;
                PrecendingContext = null;

                precendingContext.Execute();
                return;
            }

            // if there are no operations quered just notify that execution completed
            if (_operations.Count == 0)
            {
                if (ExecutionCompleted != null)
                    ExecutionCompleted();

                return;
            }

            _operations.ForEach(m => m.Execute());
        }
    }
}