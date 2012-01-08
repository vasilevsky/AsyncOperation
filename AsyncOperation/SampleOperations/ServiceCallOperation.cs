using System;

namespace AsyncOperation.SampleOperations
{
    /// <summary>
    /// Represents operation to call method of service asynchronously.
    /// It decorates another operation by providing extra activity on error.
    /// </summary>
    /// <typeparam name="T">Expacted type of result.</typeparam>
    public class ServiceCallOperation<T> : BaseWrapperOperation<T>
    {
        /// <summary>
        /// Asynchronous operation.
        /// </summary>
        private readonly IAsyncOperation<T> _innerOperation;

        /// <summary>
        /// Initializes with other asynchronous operation.
        /// </summary>
        /// <param name="innerOperation">Another operation.</param>
        public ServiceCallOperation(IAsyncOperation<T> innerOperation)
        {
            if (_innerOperation == null) throw new ArgumentNullException();

            _innerOperation = innerOperation;
        }

        /// <summary>
        /// Adds extra handling of errors to the source operation and invokes it.
        /// </summary>
        public override void Execute()
        {
            _innerOperation.ActionToExecute = ActionToExecute;

            // adding extra error handling by displaying error popup
            if (_innerOperation.OnError != null)
                _innerOperation.OnError = exception =>
                    {
                        if (OnError != null) OnError(exception);
                    };

            _innerOperation.OnSuccess = OnSuccess;

            _innerOperation.Execute();
        }
    }
}
