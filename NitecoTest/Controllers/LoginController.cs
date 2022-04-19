using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NitecoTest.Interfaces.IServices;
using NitecoTest.JWTClient;
using NitecoTest.Models.Request;

namespace NitecoTest.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IJwtClient _jwtClient;
        public LoginController(IConfiguration config, IJwtClient jwtClient, IUserService userService)
        {
            _config = config;
            _jwtClient = jwtClient;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Error");

                var validateUser = await _userService.ValidateUser(request.Email, request.Password);

                if (validateUser != null)
                {
                    var token = await _jwtClient.GetJwtToken(validateUser.Email);
                    HttpContext.Session.SetString("Token", token);
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }
    }
}
