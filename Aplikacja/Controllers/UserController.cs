using Aplikacja.DTOS.JobDtos;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.JobModel;
using Aplikacja.Entities.UserModel;
using Aplikacja.Models;
using Aplikacja.Repositories.JobRepository;
using Aplikacja.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace Aplikacja.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ActionResult<UserDto>> Profile()
        {
            try
            {
                UserDto profile = await _userRepository.GetProfile(1);
                return View(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("/profile", Name = "GetProfile")]
        //[Authorize]
        //[ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<UserDto>> GetProfile()
        //{
        //    try
        //    {
        //        UserDto profile = await _userRepository.GetProfile(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
        //        return View(profile);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("Register")]
        //[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterDto command)
        //{
        //    try
        //    {
        //        User newUser = await _userRepository.CreateUser(command);
        //        return Ok(newUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("Login")]
        //[ProducesResponseType(typeof(String), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<String>> LoginUser([FromBody] LoginDto command)
        //{
        //    try
        //    {
        //        string token = await _userRepository.LoginUser(command);
        //        return Ok(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("/profile")]
        //[Authorize]
        //[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<bool>> DeleteUser()
        //{
        //    try
        //    {
        //        bool profileDeleted = await _userRepository.DeleteUser(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
        //        return profileDeleted;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("/profile")]
        //[Authorize]
        //[ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateDto updateUser)
        //{
        //    int userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        //    try
        //    {
        //        User newUser = await _userRepository.UpdateUser(updateUser, userId);
        //        return Ok(newUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
