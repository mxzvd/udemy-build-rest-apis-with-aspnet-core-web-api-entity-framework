using AutoMapper;
using NewZealand.Walks.Rest.Models.DataTransferObjects;
using NewZealand.Walks.Rest.Repositories;

namespace NewZealand.Walks.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalksController : ControllerBase
{
    private readonly IWalksRepository walksRepository;
    private readonly IMapper mapper;

    public WalksController(IWalksRepository walksRepository, IMapper mapper)
    {
        this.walksRepository = walksRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetWalksAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
    {
        // Get the domain models from database
        var domainModelList = await walksRepository.GetWalksAsync(filterOn, filterQuery);

        // Map the domain models to response dto
        var response = mapper.Map<IEnumerable<GetWalkResponse>>(domainModelList);

        // Return the response dto
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetWalkByWalkIdAsync(Guid id)
    {
        // Get walk domain model from database
        var domainModel = await walksRepository.GetWalkByWalkIdAsync(id);

        if (domainModel == null)
            return NotFound();

        // Map walk domain model to response dto
        var response = mapper.Map<GetWalkResponse>(domainModel);

        // Return response dto
        return Ok(response);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest request)
    {
        // Map the request dto to domain model
        var domainModel = mapper.Map<Walk>(request);

        // Use the domain model to add walk to the database
        await walksRepository.AddWalkAsync(domainModel);

        // Map the domain model to response dto
        var response = mapper.Map<GetWalkResponse>(domainModel);

        // Return a 201 created with the response dto
        return CreatedAtAction("GetWalkByWalkId", new { id = response.Id }, response);
    }

    [HttpPut]
    [ValidateModel]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateWalkByWalkIdAsync(Guid id, [FromBody] UpdateWalkRequest request)
    {
        // Map request dto to a walk domain model
        var domainModel = mapper.Map<Walk>(request);

        domainModel = await walksRepository.UpdateWalkByWalkIdAsync(id, domainModel);

        if (domainModel == null)
            return NotFound();

        // Convert the domain model to response dto
        var response = mapper.Map<GetWalkResponse>(domainModel);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteWalkByWalkIdAsync([FromRoute] Guid id)
    {
        var domainModel = await walksRepository.DeleteWalkByWalkIdAsync(id);

        if (domainModel == null)
            return NotFound();

        // Option 1: Return 200 OK with the deleted Walk
        var response = mapper.Map<GetWalkResponse>(domainModel);

        return Ok(response);

        // Option 2: Return only 200 OK response (Preferred)
        // return Ok();
    }
}
