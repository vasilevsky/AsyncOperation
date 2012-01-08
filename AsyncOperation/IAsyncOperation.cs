using System;

namespace AsyncOperation
{
    /// <summary>
    /// Represents asyncronous operation.
    /// </summary>
    /// <typeparam name="T">Expected return type of operation when executed successfuly.</typeparam>
    public interface IAsyncOperation<T> : IAsyncOperation
    {
        /// <summary>
        /// Action to be done when operation succeeded. The parameter of action is result of execution.
        /// </summary>
        Action<T> OnSuccess { get; set; }
    }

    /// <summary>
    /// Represents asyncronous operation.
    /// </summary>
    public interface IAsyncOperation : IExecutable
    {
        /// <summary>
        /// An action to be called with Execute method.
        /// </summary>
        Action ActionToExecute { get; set; }

        /// <summary>
        /// Action to be done when operation succeeded. The parameter of action is Exception object 
        /// occurred during execution.
        /// </summary>
        Action<Exception> OnError { get; set; }
    }
}