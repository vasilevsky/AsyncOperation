using System;

namespace AsyncOperation
{
    /// <summary>
    /// Base class for asynchronous operation.
    /// </summary>
    /// <typeparam name="T">Expected return type of operation when executed successfuly.</typeparam>
    public abstract class BaseOperation<T> : IAsyncOperation<T>
    {
        /// <summary>
        /// Fires when <see cref="ActionToExecute"/> was completed.
        /// </summary>
        public event Action ExecutionCompleted;

        /// <summary>
        /// An action to be called with Execute method.
        /// </summary>
        public Action ActionToExecute { get; set; }

        /// <summary>
        /// Action to be done when operation succeeded. The parameter of action is Exception object 
        /// occurred during execution.
        /// </summary>
        public Action<Exception> OnError { get; set; }

        /// <summary>
        /// Action to be done when operation succeeded. The parameter of action is result of execution.
        /// </summary>
        public Action<T> OnSuccess { get; set; }

        /// <summary>
        /// Invokes operation.
        /// </summary>
        public abstract void Execute();
    }
}
