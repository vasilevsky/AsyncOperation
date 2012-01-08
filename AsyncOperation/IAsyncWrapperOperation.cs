using System;

namespace AsyncOperation
{
    /// <summary>
    /// Provides possibility to invoke handling on error or success.
    /// This can be handy when wrapping different asynchronous actions.
    /// </summary>
    /// <typeparam name="T">Expected type of result.</typeparam>
    public interface IAsyncWrapperOperation<T> : IAsyncOperation<T>
    {
        /// <summary>
        /// Executes OnSuccess action and return True if there was result and action.
        /// </summary>
        /// <param name="result">Resulting object for action.</param>
        /// <returns>true if OnSuccess was executed if specified.</returns>
        bool ExecuteOnSuccessAndReturn(T result);

        /// <summary>
        /// Executes OnError action and return True if there was exception and action.
        /// </summary>
        /// <param name="error">Error for OnError action.</param>
        /// <returns>true if OnError was executed if specified.</returns>
        bool ExecuteOnErrorAndReturn(Exception error);
    }
}
