namespace BuildingBlocks.cs.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }

}
