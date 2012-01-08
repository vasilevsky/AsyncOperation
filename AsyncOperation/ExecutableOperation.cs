namespace AsyncOperation
{
    /// <summary>
    /// Represents operation which can can execute some action.
    /// </summary>
    /// <typeparam name="T">Expected return type of operation when executed successfuly.</typeparam>
    public class ExecutableOperation<T> : BaseOperation<T>
    {
        /// <summary>
        /// Invokes action.
        /// </summary>
        public override void Execute()
        {
            if (ActionToExecute != null)
                ActionToExecute();
        }
    }
}