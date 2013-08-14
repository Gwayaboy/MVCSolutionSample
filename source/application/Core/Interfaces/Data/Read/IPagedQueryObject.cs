namespace Intrigma.DonorSpace.Core.Interfaces.Data.Read
{
    public interface IPagedQueryObject
    {
        int PageIndex { get; }

        int RecordsCount { get; }

        string Sort { get; }
    }
}