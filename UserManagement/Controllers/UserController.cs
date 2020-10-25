using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Entities;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {

        private UserDbContext _context;
        private readonly IRepository<User, int> _repository;
        private const bool AutoSave = true;

        public UserController(IRepository<User, int> repository, UserDbContext context)
        {
            this._repository = repository;
            _context = context;
        }

        [HttpPost("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;
                var user = await _repository.GetAsync(p => p.Email == model.Email && p.Password == model.Password);
                if (user == null) return BadRequest();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("AltamiraAltamira");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "Altamira",
                    Audience = "Altamira",
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),

                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<List<User>> GetUser()
        {

            var response = _repository.GetListAsync(null, "Address", "Address.Geo", "Company");
            return await response;
        }

        [HttpDelete]
        public async Task<User> DeleteUser(int id)
        {
            var entity = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
            var removeUser = _repository.DeleteAsync(id, AutoSave);

            return entity;
        }
        [HttpPost]

        public async Task<User> AddUser(User user)
        {
            var response = _repository.AddAsync(user, AutoSave);

            return await response;
        }
        [HttpPut]
        public async Task<User> UpdateUser(User Users)
        {
            var response = _repository.UpdateAsync(Users, AutoSave);

            return await response;
        }

    }
}
