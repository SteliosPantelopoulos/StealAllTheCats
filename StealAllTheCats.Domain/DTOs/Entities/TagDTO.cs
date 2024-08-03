namespace StealAllTheCats.Domain.DTOs.Entities;

/// <summary>
/// Tag entity
/// </summary>
public class TagDTO : BaseDTO
{
    /// <summary>
    /// ID of Tag in DB
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Name of Tag
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Date of Tag import in the DB
    /// </summary>
    public DateTime Created { get; set; }
}
