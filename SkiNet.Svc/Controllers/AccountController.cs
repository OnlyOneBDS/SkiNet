using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SkiNet.Core.Entities.Identity;
using SkiNet.Core.Interfaces;
using SkiNet.Svc.DTOs;
using SkiNet.Svc.Errors;

namespace SkiNet.Svc.Controllers;

public class AccountController : BaseApiController
{
  private readonly UserManager<AppUser> _userManager;
  private readonly SignInManager<AppUser> _signInManager;
  private readonly ITokenService _tokenService;
  private readonly IMapper _mapper;

  public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
    _mapper = mapper;
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<UserDto>> GetCurrentUser()
  {
    var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

    return new UserDto
    {
      DisplayName = user.DisplayName,
      Email = user.Email,
      Token = _tokenService.CreateToken(user)
    };
  }

  [HttpGet("emailexists")]
  public async Task<ActionResult<bool>> CheckEmailExistsAsync(string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }

  [Authorize]
  [HttpGet("address")]
  public async Task<ActionResult<AddressDto>> CheckUserAddress()
  {
    var user = await _userManager.FindByEmailWithAddressAsync(User);

    return _mapper.Map<AddressDto>(user.Address);
  }

  [Authorize]
  [HttpPut("address")]
  public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
  {
    var user = await _userManager.FindByEmailWithAddressAsync(User);

    user.Address = _mapper.Map<Address>(address);

    var result = await _userManager.UpdateAsync(user);

    if (result.Succeeded)
    {
      return Ok(_mapper.Map<Address, AddressDto>(user.Address));
    }

    return BadRequest("Problem updating the user");
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    if (user == null)
    {
      return Unauthorized(new ApiResponse(401));
    }

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

    if (!result.Succeeded)
    {
      return Unauthorized(new ApiResponse(401));
    }

    return new UserDto
    {
      DisplayName = user.DisplayName,
      Email = user.Email,
      Token = _tokenService.CreateToken(user)
    };
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
    {
      return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address already exists" } });
    }

    var user = new AppUser
    {
      DisplayName = registerDto.DisplayName,
      UserName = registerDto.Email,
      Email = registerDto.Email
    };

    var result = await _userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      return BadRequest(new ApiResponse(400));
    }

    return new UserDto
    {
      DisplayName = user.DisplayName,
      Email = user.Email,
      Token = _tokenService.CreateToken(user)
    };
  }
}