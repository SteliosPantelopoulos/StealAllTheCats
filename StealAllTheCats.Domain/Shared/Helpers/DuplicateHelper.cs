using StealAllTheCats.Infrastructure.Context.Entities;

namespace StealAllTheCats.Domain.Shared.Helpers;

public static class DuplicateHelper
{
    public static List<Cat> GetNewCats<T>(List<Cat> NewEntries, List<Cat> ExistingEntires)
    {
        var nonExisting = NewEntries.Where(e => !ExistingEntires.Any(x => x.CatID == e.CatID)).ToList();

        return nonExisting;
    }

    public static List<Tag> GetNewTags<T>(List<Tag> NewEntries, List<Tag> ExistingEntires)
    {
        var nonExisting = NewEntries.Where(e => !ExistingEntires.Any(x => x.Name == e.Name)).ToList();

        return nonExisting;
    }
}
