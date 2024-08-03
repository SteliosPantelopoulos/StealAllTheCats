namespace StealAllTheCats.Domain.DTOs.Entities;

/// <summary>
/// Cat entity
/// </summary>
public class CatDTO : BaseDTO
{
    /// <summary>
    /// ID of Cat in https://thecatapi.com/
    /// </summary>
    public string CatID { get; set; } = null!;
    /// <summary>
    /// Width of image
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// Height of image
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// Image url to download file
    /// </summary>
    public string ImageUrl { get; set; } = null!;
    /// <summary>
    /// Tags of Cat
    /// </summary>
    public List<TagDTO> Tags { get; set; } = new();
}
