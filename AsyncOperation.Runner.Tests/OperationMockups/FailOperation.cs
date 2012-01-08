using System;

namespace AsyncOperation.Runner.Tests.OperationMockups
{
    public class FailOperation : TestOperation<Exception>
    {
        private readonly Exception _exception;

        public FailOperation(Exception exception)
        {
            _exception = exception;

            var action = ActionToExecute;
            ActionToExecute = delegate
                {
                    action();

                    if (OnError != null) OnError(_exception);
                };
        }
    }
}