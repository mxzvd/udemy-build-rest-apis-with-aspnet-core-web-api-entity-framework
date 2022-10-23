using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllRegions()
    {
        // Return DTO regions:
        var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regionRepository.GetAll());
        return Ok(regionsDTO);
    }
}
