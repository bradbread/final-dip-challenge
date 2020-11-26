using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baseball_Team_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Baseball_Team_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TestDBContext _context;

        public AuthController(TestDBContext context)
        {
            _context = context;
        }


        [HttpPost, Route("member")]
        public IActionResult Login(Login login)
        {

            if (login == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            var member = (from m in _context.Member
                              //where s.Email == login.Email
                          where m.Username == login.Username
                          select new Member
                          {
                              Username = m.Username,
                              Salt = m.Salt,
                              Password = m.Password,
                              AuthLevel = m.AuthLevel
                          }).ToList();

            //Handle invalid logins
            if (member.Count == 0)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }

            var passwordHash = SHA512.Create();

            passwordHash.ComputeHash(Encoding.UTF8.GetBytes(login.Password + member.SingleOrDefault().Salt + Environment.GetEnvironmentVariable("pepper")));
            if (passwordHash.Hash.SequenceEqual(member.SingleOrDefault().Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[] {
                    //new Claim("Role", staff.FirstOrDefault().Role.ToString()),
                    new Claim("Username", member.FirstOrDefault().Username.ToString()),
                    new Claim("AuthLevel", member.FirstOrDefault().AuthLevel.ToString())
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: Environment.GetEnvironmentVariable("applicationUrl"),
                    audience: Environment.GetEnvironmentVariable("applicationUrl"),
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }
            else
            {
                Console.WriteLine("Failed Login");
                return Unauthorized();
            }
        }

    }
}
