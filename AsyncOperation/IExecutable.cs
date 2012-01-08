using System;

namespace AsyncOperation
{
    /// <summary>
    /// Represent async operation which can be invoked and notify about it's completeness.
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Fires when execution of asynchronous operation was completed.
        /// </summary>
        event Action ExecutionCompleted;

        /// <summary>
        /// Invokes operation.
        /// </summary>
        void Execute();
    }
}