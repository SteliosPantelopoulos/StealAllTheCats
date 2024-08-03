using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StealAllTheCats.Domain.DTOs.Entities;
using StealAllTheCats.Domain.Interfaces;
using StealAllTheCats.Domain.Shared.Enums;
using StealAllTheCats.Infrastructure.Shared.Paging;
using StealAllTheCats.Interfaces;

namespace StealAllTheCats.Controllers;

[ApiController]
[Route("[controller]")]
public class CatsController(ICatsService service) : ControllerBase, ICatsController
{
    /// <summary>
    /// Endpoint to fetch new cats in the DB
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]")]
    [ProducesDefaultResponseType(typeof(bool))]
    public async Task<ActionResult> Fetch()
    {
        var data = await service.FetchAsync();

        if (data.Succeeded)
        {
            return Ok();
        }
        else
        {
            if (data.ErrorCode != ErrorCodes.Default)
                return BadRequest(data.ErrorCode);
            else
                return StatusCode(500);
        }
    }

    /// <summary>
    /// Endpoint to retrieve a cat based on it's ID
    /// </summary>
    /// <param name="ID">ID of the cat you want to retrieve</param>
    /// <returns></returns>
    [HttpGet("{ID}")]
    [ProducesDefaultResponseType(typeof(CatDTO))]
    public async Task<ActionResult> GetByID(int ID)
    {
        var data = await service.GetByIDAsync(ID);

        if (data is null)
            return NotFound();
        else
            return Ok(data);
    }

    /// <summary>
    /// Endpoint to retrieve cats (pagination)
    /// </summary>
    /// <param name="PageNumber">Page number with acceptable values : 1-n</param>
    /// <param name="PageSize">The number of cats in each page with acceptable values: 0-n </param>
    /// <param name="TagName">The tag you want to retrieve, if null or empty string is parsed all cats will be returned</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesDefaultResponseType(typeof(PagedList<CatDTO>))]
    public async Task<ActionResult> GetAll(int PageNumber = 1, int PageSize = 10, string? TagName = null)
    {
        var data = await service.GetAllAsync(PageNumber, PageSize, TagName);

        return Ok(data);
    }

}
