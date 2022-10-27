using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Validators;

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
        var validator = new AddRegionRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
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
        var validator = new UpdateRegionRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
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
}
