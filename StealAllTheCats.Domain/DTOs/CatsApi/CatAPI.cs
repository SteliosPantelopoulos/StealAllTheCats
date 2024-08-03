namespace StealAllTheCats.Domain.DTOs.CatsApi;

public class CatAPI
{
    public string ID { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Breed> Breeds { get; set; }
}
