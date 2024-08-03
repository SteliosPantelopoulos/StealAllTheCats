namespace StealAllTheCats.Infrastructure.Context.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual List<Cat> Cats { get; set; }
}
