using AsyncOperation.Runner.Parallel;
using AsyncOperation.Runner.Sequential;

namespace AsyncOperation.Runner
{
    /// <summary>
    /// Start point of building asynchronous operations invokation tree.
    /// </summary>
    public class Run
    {
        /// <summary>
        /// Operations followed this property will be executed in parallel.
        /// </summary>
        public static ParallelExecution InParallel
        {
            get
            {
                return new ParallelExecution(ParallelExecutionContext.Empty);
            }
        }

        /// <summary>
        /// Operations followed this property will be executed sequentially.
        /// </summary>
        public static SequentialExecution OneByOne
        {
            get
            {
                return new SequentialExecution(SequentialExecutionContext.Empty);
            }
        }
    }
}