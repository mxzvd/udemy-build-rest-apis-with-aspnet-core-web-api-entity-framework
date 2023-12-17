using AutoMapper;
using NewZealand.Walks.Rest.Models.DataTransferObjects;
using NewZealand.Walks.Rest.Repositories;

namespace NewZealand.Walks.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly IRegionsRepository regionsRepository;
    private readonly IMapper mapper;

    public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
    {
        this.regionsRepository = regionsRepository;
        this.mapper = mapper;
    }

    // Get all regions
    [HttpGet]
    public async Task<IActionResult> GetRegionsAsync()
    {
        // Get the domain models from database
        var domainModelList = await regionsRepository.GetRegionsAsync();

        // Map the domain models to response dto

        // Option 1 (manual):
        // var response = domainModelList.Select(e => new GetRegionResponseDto()
        // {
        //     Id = e.Id,
        //     Code = e.Code,
        //     Name = e.Name,
        //     RegionImageUrl = e.RegionImageUrl,
        // });

        // Option 2 (auto-mapper):
        var response = mapper.Map<IEnumerable<GetRegionResponse>>(domainModelList);

        // Return the response dto
        return Ok(response);
    }

    // Get single region by region id
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetRegionByRegionIdAsync(Guid id)
    {
        // Get region domain model from database
        var domainModel = await regionsRepository.GetRegionByRegionIdAsync(id);

        if (domainModel == null)
            return NotFound();

        // Map region domain model to response dto

        // Option 1 (manual):
        // var response = new GetRegionResponseDto()
        // {
        //     Id = domainModel.Id,
        //     Code = domainModel.Code,
        //     Name = domainModel.Name,
        //     RegionImageUrl = domainModel.RegionImageUrl,
        // };

        // Option 2 (auto-mapper):
        var response = mapper.Map<GetRegionResponse>(domainModel);

        // Return response dto
        return Ok(response);
    }

    // Add a region
    [HttpPost]
    public async Task<IActionResult> AddRegionAsync([FromBody] AddRegionRequest request)
    {
        // Validate the input
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map the request dto to domain model

        // Option 1 (manual):
        // var domainModel = new Region
        // {
        //     Code = request.Code,
        //     Name = request.Name,
        //     RegionImageUrl = request.RegionImageUrl,
        // };

        // Option 2 (auto-mapper):
        var domainModel = mapper.Map<Region>(request);

        // Use the domain model to add region to the database
        await regionsRepository.AddRegionAsync(domainModel);

        // Map the domain model to response dto

        // Option 1 (manual):
        // var response = new GetRegionResponseDto
        // {
        //     Id = domainModel.Id,
        //     Code = domainModel.Code,
        //     Name = domainModel.Name,
        //     RegionImageUrl = domainModel.RegionImageUrl,
        // };

        // Option 2 (auto-mapper):
        var response = mapper.Map<GetRegionResponse>(domainModel);

        // Return a 201 created with the response dto
        return CreatedAtAction("GetRegionByRegionId", new { id = response.Id }, response);
    }

    // Update region
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRegionByRegionIdAsync(Guid id, [FromBody] UpdateRegionRequest request)
    {
        // Validate the input
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map request dto to a region domain model

        // Option 1 (manual):
        // var domainModel = new Region
        // {
        //     Code = request.Code,
        //     Name = request.Name,
        //     RegionImageUrl = request.RegionImageUrl,
        // };

        // Option 2 (auto-mapper):
        var domainModel = mapper.Map<Region>(request);

        domainModel = await regionsRepository.UpdateRegionByRegionIdAsync(id, domainModel);

        if (domainModel == null)
            return NotFound();

        // Convert the domain model to response dto

        // Option 1 (manual):
        // var response = new GetRegionResponseDto
        // {
        //     Id = domainModel.Id,
        //     Code = domainModel.Code,
        //     Name = domainModel.Name,
        //     RegionImageUrl = domainModel.RegionImageUrl,
        // };

        // Option 2 (auto-mapper):
        var response = mapper.Map<GetRegionResponse>(domainModel);

        return Ok(response);
    }

    // Delete a region
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegionByRegionIdAsync([FromRoute] Guid id)
    {
        var domainModel = await regionsRepository.DeleteRegionByRegionIdAsync(id);

        if (domainModel == null)
            return NotFound();

        // Option 1: Return 200 OK with the deleted region

        // Option 1.1 (manual):
        // var response = new GetRegionResponse
        // {
        //     Id = domainModel.Id,
        //     Code = domainModel.Code,
        //     Name = domainModel.Name,
        //     RegionImageUrl = domainModel.RegionImageUrl,
        // };

        // Option 1.2 (auto-mapper):
        var response = mapper.Map<GetRegionResponse>(domainModel);

        return Ok(response);

        // Option 2: Return only 200 OK response (Preferred)
        // return Ok();
    }
}
