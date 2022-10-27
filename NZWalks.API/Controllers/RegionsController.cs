using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RegionsController : Controller
{
    private readonly IRegionRepository regionRepository;
    private readonly IMapper mapper;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        this.regionRepository = regionRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegionsAsync()
    {
        var regions = await regionRepository.GetAllAsync();
        // Return DTO regions:
        var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
        return Ok(regionsDTO);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ActionName("GetRegionAsync")]
    public async Task<IActionResult> GetRegionAsync(Guid id)
    {
        var region = await regionRepository.GetAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        var regionDTO = mapper.Map<Models.DTO.Region>(region);

        return Ok(regionDTO);
    }

    [HttpPost]
    public async Task<IActionResult> AddRegionAsync(AddRegionRequest request)
    {
        // Validate the request:
        if (!ValidateAddRegionAsync(request))
        {
            return BadRequest(ModelState);
        }

        var region = new Models.Domain.Region()
        {
            Code = request.Code,
            Area = request.Area,
            Lat = request.Lat,
            Long = request.Long,
            Name = request.Name,
            Population = request.Population
        };

        var response = await regionRepository.AddAsync(region);

        var regionDTO = new Models.DTO.Region
        {
            Id = region.Id,
            Code = region.Code,
            Area = region.Area,
            Lat = region.Lat,
            Long = region.Long,
            Name = region.Name,
            Population = region.Population
        };

        return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRegionAsync(Guid id)
    {
        var region = await regionRepository.DeleteAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        var regionDTO = new Models.DTO.Region
        {
            Id = region.Id,
            Code = region.Code,
            Area = region.Area,
            Lat = region.Lat,
            Long = region.Long,
            Name = region.Name,
            Population = region.Population
        };

        return Ok(regionDTO);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest request)
    {
        // Validate the request:
        if (!ValidateUpdateRegionAsync(request))
        {
            return BadRequest(ModelState);
        }

        // Convert DTO to Domain model:
        var region = new Models.Domain.Region()
        {
            Code = request.Code,
            Area = request.Area,
            Lat = request.Lat,
            Long = request.Long,
            Name = request.Name,
            Population = request.Population
        };

        // Update Region using Repository:
        region = await regionRepository.UpdateAsync(id, region);

        // If null return NotFound:
        if (region == null)
        {
            return NotFound();
        }

        // Convert Domain back to DTO:
        var regionDTO = new Models.DTO.Region
        {
            Id = region.Id,
            Code = region.Code,
            Area = region.Area,
            Lat = region.Lat,
            Long = region.Long,
            Name = region.Name,
            Population = region.Population
        };

        // Return Ok response:
        return Ok(regionDTO);
    }

    #region Validations

    private bool ValidateAddRegionAsync(AddRegionRequest request)
    {
        if (request == null)
        {
            ModelState.AddModelError(nameof(request), $"Request can not be empty.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(request.Code))
        {
            ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} must not be null or empty or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} must not be null or empty or whitespace.");
        }

        if (request.Area <= 0)
        {
            ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} must be positive.");
        }

        if (request.Population < 0)
        {
            ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} must be non-negative.");
        }

        if (ModelState.ErrorCount > 0)
        {
            return false;
        }

        return true;
    }

    private bool ValidateUpdateRegionAsync(UpdateRegionRequest request)
    {
        if (request == null)
        {
            ModelState.AddModelError(nameof(request), $"Request can not be empty.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(request.Code))
        {
            ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} must not be null or empty or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} must not be null or empty or whitespace.");
        }

        if (request.Area <= 0)
        {
            ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} must be positive.");
        }

        if (request.Population < 0)
        {
            ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} must be non-negative.");
        }

        if (ModelState.ErrorCount > 0)
        {
            return false;
        }

        return true;
    }

    #endregion
}
