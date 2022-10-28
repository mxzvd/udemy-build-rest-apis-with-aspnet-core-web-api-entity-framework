using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Validators;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly ITokenHandler tokenHandler;

    public AuthController(IMapper mapper, IUserRepository userRepository, ITokenHandler tokenHandler)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.tokenHandler = tokenHandler;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        // Validate the incoming request:
        var validator = new LoginRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        var user = await userRepository.AuthenticateAsync(request.Username, request.Password);

        if (user == null)
        {
            return BadRequest("Invalid username or password.");
        }

        var token = tokenHandler.CreateToken(user);
        return Ok(token);
    }
}
