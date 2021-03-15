using ProfileBook.Enums;

namespace ProfileBook.Services.Sorting
{
    public interface ISortingService
    {
        SortTypes GetCurrentSortType { get; }
    }
}
