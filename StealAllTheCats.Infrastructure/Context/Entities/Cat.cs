namespace StealAllTheCats.Infrastructure.Context.Entities;

public class Cat : BaseEntity
{
    public string CatID { get; set; } = null!;
    public int Width { get; set; }
    public int Height { get; set; }
    public string ImageUrl { get; set; } = null!;
    public virtual List<Tag> Tags { get; set; }
}
