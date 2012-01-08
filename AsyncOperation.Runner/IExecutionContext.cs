namespace AsyncOperation.Runner
{
    public interface IExecutionContext : IExecutable
    {
        IExecutionContext PrecendingContext { get; set; }
    }
}