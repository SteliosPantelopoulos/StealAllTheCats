namespace StealAllTheCats.Domain.DTOs.Entities;

public class BaseDTO
{
    /// <summary>
    /// ID of Cat in the DB
    /// </summary>
    public int ID { get; set; }   
    /// <summary>
    /// Date of Cat import in the DB
    /// </summary>
    public DateTime Created { get; set; }
}
