using System;

namespace AsyncOperation
{
    /// <summary>
    /// Base class for asynchronous operation which used as wrapper for various asynchronous activity.
    /// </summary>
    /// <typeparam name="T">Expected return type of operation when executed successfuly.</typeparam>
    public abstract class BaseWrapperOperation<T> : BaseOperation<T>, IAsyncWrapperOperation<T>
    {
        /// <summary>
        /// Executes OnError action and return True if there was exception and action.
        /// </summary>
        /// <param name="error">Error for OnError action.</param>
        /// <returns>true if OnError was executed if specified.</returns>
        public bool ExecuteOnErrorAndReturn(Exception error)
        {
            if (error == null) return false;

            if (OnError == null) return false;

            OnError(error);

            return true;
        }

        /// <summary>
        /// Executes OnSuccess action and return True if there was result and action.
        /// </summary>
        /// <param name="result">Resulting object for action.</param>
        /// <returns>true if OnSuccess was executed if specified.</returns>
        public bool ExecuteOnSuccessAndReturn(T result)
        {
            if (OnSuccess == null) return false;

            OnSuccess(result);

            return true;
        }
    }
}
