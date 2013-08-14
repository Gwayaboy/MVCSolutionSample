namespace Intrigma.DonorSpace.Core.ApplicationMessage
{
    /// <summary>
    /// Represents a enumeration of type of messages that can be displayed to the end user
    /// </summary>
    public enum MessageCategory
    {
        Success = 0,
        Information,
        Warning,

        BrokenBusinessRule,
        FatalException,
        AuthorisationException
    }
}