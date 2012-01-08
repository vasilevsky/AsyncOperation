using AsyncOperation.Runner.Parallel;
using AsyncOperation.Runner.Sequential;

namespace AsyncOperation.Runner
{
    /// <summary>
    /// Enumerates provided ways to execute operations.
    /// </summary>
    public class ExecutionType
    {
        /// <summary>
        /// Initializes with prescending async operation
        /// execution of which is used as trigger of other operations.
        /// </summary>
        /// <param name="executionContext">Precending execution context.</param>
        public ExecutionType(IExecutionContext executionContext)
        {
            InParallel = new ParallelExecution(executionContext);
            OneByOne = new SequentialExecution(executionContext);
        }

        /// <summary>
        /// Operations followed this property will be executed in parallel.
        /// </summary>
        public ParallelExecution InParallel { get; private set; }

        /// <summary>
        /// Operations followed this property will be executed sequentially.
        /// </summary>
        public SequentialExecution OneByOne { get; private set; }
    }
}