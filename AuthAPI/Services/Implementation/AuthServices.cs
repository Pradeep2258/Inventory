using AuthAPI.Model;
using AuthAPI.Repository.Interface;
using AuthAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepo _authRepo;
        public AuthServices(IConfiguration config, IAuthRepo authRepo)
        {
            _config = config;
            _authRepo = authRepo;
        }

        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtUserDetails jwtUserDetails = new JwtUserDetails()
            {
                UserID = userInfo.LoginID,
                Username = userInfo.Username,
                UserRole = userInfo.UserRole
            };


            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userInfo.Username),
                new Claim("UserDetails",JsonConvert.SerializeObject(jwtUserDetails))
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel AuthenticateUser(UserLoginModel login)
        {
            UserModel user = null;

            //Validate the User Credentials    
            string IsValidLogin = _authRepo.GetUserDetail(login);

            if (IsValidLogin != null && IsValidLogin == "User successfully logged in")
            {
                user = _authRepo.getUserRoleByID(login);
            }

            return user;
        }
    }
}
