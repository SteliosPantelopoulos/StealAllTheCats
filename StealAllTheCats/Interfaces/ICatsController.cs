using Microsoft.AspNetCore.Mvc;

namespace StealAllTheCats.Interfaces;

public interface ICatsController
{
    Task<ActionResult> Fetch();
    Task<ActionResult> GetByID(int ID);
    Task<ActionResult> GetAll(int PageNumber = 1, int PageSize = 10, string? TagName = null);
}
