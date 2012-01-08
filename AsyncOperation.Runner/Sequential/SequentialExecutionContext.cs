using System;

namespace AsyncOperation.Runner.Sequential
{
    public class SequentialExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Instance of empty execution context.
        /// </summary>
        public static readonly EmptyExecutionContext Empty = new EmptyExecutionContext();

        /// <summary>
        /// Points to the first operation to be invoked.
        /// </summary>
        private IExecutable _first;

        /// <summary>
        /// Points to the last operation to be invoked.
        /// </summary>
        private IExecutable _last;

        public SequentialExecutionContext(IExecutionContext precendingContext)
        {
            PrecendingContext = precendingContext;
        }

        public IExecutionContext PrecendingContext { get; set; }

        public void Register(IExecutable operation)
        {
            if (_first == null)
            {
                _first = operation;
                _last = operation;
                _last.ExecutionCompleted += LastOperationCompletanceHandler;
                return;
            }

            // reseting full sequence execution completed handler
            // because last operation will no more 
            _last.ExecutionCompleted -= LastOperationCompletanceHandler;

            // set invocation of operation after execution of previous operation
            _last.ExecutionCompleted += operation.Execute;
            _last = operation;

            // refreshing full sequence execution completed handler by subscribing 
            // to the invocation list tail ExecutionCompleted event
            _last.ExecutionCompleted += LastOperationCompletanceHandler;
        }

        public event Action ExecutionCompleted;

        public void Execute()
        {
            if (PrecendingContext != Empty)
            {
                var precendingContext = PrecendingContext;
                PrecendingContext = null;

                precendingContext.Execute();
                return;
            }

            if (_first != null)
                _first.Execute();
        }

        private void LastOperationCompletanceHandler()
        {
            if (ExecutionCompleted != null)
                ExecutionCompleted();
        }
    }
}