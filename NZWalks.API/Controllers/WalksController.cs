using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WalksController : Controller
{
    private readonly IWalkRepository walkRepository;
    private readonly IMapper mapper;

    public WalksController(IWalkRepository walkRepository, IMapper mapper)
    {
        this.walkRepository = walkRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWalksAsync()
    {
        var walksDomain = await walkRepository.GetAllAsync();
        var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
        return Ok(walksDTO);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName("GetWalkAsync")]
    public async Task<IActionResult> GetWalkAsync(Guid id)
    {
        var walkDomain = await walkRepository.GetAsync(id);

        if (walkDomain == null)
        {
            return NotFound();
        }

        var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
        return Ok(walkDTO);
    }

    [HttpPost]
    public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest request)
    {
        // Convert DTO to domain object:
        var walkDomain = new Models.Domain.Walk
        {
            Length = request.Length,
            Name = request.Name,
            RegionId = request.RegionId,
            WalkDifficultyId = request.WalkDifficultyId
        };

        // Pass domain object to repository to persist it:
        walkDomain = await walkRepository.AddAsync(walkDomain);

        // Convert the domain object back to DTO:
        var walkDTO = new Models.DTO.Walk
        {
            Id = walkDomain.Id,
            Length = walkDomain.Length,
            Name = walkDomain.Name,
            RegionId = walkDomain.RegionId,
            WalkDifficultyId = walkDomain.WalkDifficultyId
        };

        // Send DTO response back to Client:
        return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest request)
    {
        // Convert the DTO to domain object:
        var walkDomain = new Models.Domain.Walk{
            Length = request.Length,
            Name = request.Name,
            RegionId = request.RegionId,
            WalkDifficultyId = request.WalkDifficultyId
        };

        // Pass the details to repository:
        walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

        // Handle null (not found):
        if (walkDomain == null)
        {
            return NotFound();
        }

        // Convert back domain to DTO:
        var walkDTO = new Models.DTO.Walk
        {
            Id = walkDomain.Id,
            Length = walkDomain.Length,
            Name = walkDomain.Name,
            RegionId = walkDomain.RegionId,
            WalkDifficultyId = walkDomain.WalkDifficultyId
        };

        // Return response:
        return Ok(walkDTO);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
    {
        // Call repository to delete object:
        var walkDomain = await walkRepository.DeleteAsync(id);

        if (walkDomain == null)
        {
            return NotFound();
        }

        var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
        return Ok(walkDTO);
    }
}
