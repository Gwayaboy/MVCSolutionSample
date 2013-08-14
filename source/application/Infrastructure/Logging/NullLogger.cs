namespace Intrigma.DonorSpace.Infrastructure.Logging
{
    public class NullLogger : ILogger
    {
        public static readonly NullLogger Instance = new NullLogger();


        public void Info(string toString) { }
    }
}