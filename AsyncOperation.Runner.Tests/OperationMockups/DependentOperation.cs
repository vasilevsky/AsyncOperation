using System;

namespace AsyncOperation.Runner.Tests.OperationMockups
{
    /// <summary>
    /// Represent async operation which can be invoked and notify about it's completion.
    /// </summary>
    public interface IDependentInvokable<T,P>
    {
        /// <summary>
        /// Fires when execution of asynchronous operation was completed.
        /// </summary>
        event Action ExecutionCompleted;

        Action<P> OnSuccess { get; set; }

        /// <summary>
        /// Triggers operation execution.
        /// </summary>
        void Execute(T parameter);
    }

    /// <summary>
    /// Represent async operation which can be invoked and notify about it's completion.
    /// </summary>
    public interface IDependentInvokable<T>
    {
        /// <summary>
        /// Fires when execution of asynchronous operation was completed.
        /// </summary>
        event Action ExecutionCompleted;

        /// <summary>
        /// Triggers operation execution.
        /// </summary>
        void Execute(T parameter);
    }

    public class DependentOperation<T,P> : IDependentInvokable<T,P>
    {
        public event Action ExecutionCompleted;

        public Action<P> OnSuccess { get; set; }

        public void Execute(T parameter)
        {
        }
    }

    public class DependentOperation<T> : IDependentInvokable<T>
    {
        public event Action ExecutionCompleted;

        public void Execute(T parameter)
        {
        }
    }
}
