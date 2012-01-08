namespace AsyncOperation.Runner.Tests.OperationMockups
{
    public class SuccessfulOperation<T> : TestOperation<T>
    {
        private readonly T _returnResult;

        public SuccessfulOperation(T returnResult)
        {
            _returnResult = returnResult;

            var action = ActionToExecute;
            ActionToExecute = delegate
                {
                    if (OnSuccess != null) OnSuccess(_returnResult);

                    action();
                };
        }
    }
}