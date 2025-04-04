using AutoMapper;
using BookStoreAppApI.Data;
using BookStoreAppApI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAppApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<AuthController> logger;
        private readonly UserManager<ApiUser> userManager;


        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser>userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;


        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);

                    }
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, "User");

                return Accepted();

            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in the{nameof(Register)}");
                return Problem($"Something went wrong in the{nameof(Register)}", statusCode:500);
            }
         
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            logger.LogInformation($"Login Attempt for{userDto.Email}");
            try
            {
                var user = await userManager.FindByEmailAsync(userDto.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, userDto.Password);

                if(user == null || passwordValid == false)
                {
                    return NotFound();
                }

                return Accepted();


            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in the{nameof(Login)}");
                return Problem($"Something went wrong in the{nameof(Login)}", statusCode: 500);

            }
        }
    }
}
