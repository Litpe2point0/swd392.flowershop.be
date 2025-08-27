using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.AuthDTO;
using BusinessObject.Model;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            var response = new LoginResponseDTO();
            var user = await _unitOfWork.UserRepository.GetSingleByConditionAsync(
                u => u.Email == loginRequest.Email,
                u => u.Role);

            if(user != null)
            {
                var passwordVerifyResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
                if(passwordVerifyResult == PasswordVerificationResult.Success)
                {
                    // Generate JWT token
                    string token = GenerateJwtToken(user);

                    response = new LoginResponseDTO(){
                        Code = 200,
                        Message = "Login Successfully",
                        IsSuccess = true,
                        Result = new LoginResult(){
                            UserId = user.Id,
                            Email = user.Email,
                            Username = user.Username,
                            RoleId = user.Role.Id,
                            Token = token,
                            Phone = user.Phone,
                            RoleName = user.Role.Name,
                            Address = user.Address
                        }
                    };
                    return response;
                }
            } 
            
            response = new LoginResponseDTO(){
                Code = 401,
                Message = "Invalid email or password",
                IsSuccess = false
            };
            return response;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequest)
        {
            var response = new RegisterResponseDTO();
    
            // Check if email already exists
            var existingUser = await _unitOfWork.UserRepository.GetSingleByConditionAsync(
                u => u.Email == registerRequest.Email);
        
            if (existingUser != null)
            {
                return new RegisterResponseDTO
                {
                    Code = 400,
                    Message = "Email already exists",
                    IsSuccess = false
                };
            }
    
            // Create new user
            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                Password = _passwordHasher.HashPassword(null, registerRequest.Password),
                RoleId = registerRequest.RoleId,
                Phone = registerRequest.Phone,
                CreatedDate = DateTime.Now,
                isActive = true
            };
    
            try
            {
                // Add user to database
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
        
                var createdUser = await _unitOfWork.UserRepository.GetSingleByConditionAsync(
                    u => u.Email == registerRequest.Email,
                    u => u.Role);
                // Generate token for automatic login
                string token = GenerateJwtToken(createdUser);
        
                return new RegisterResponseDTO
                {
                    Code = 201,
                    Message = "Registration successful",
                    IsSuccess = true,
                    Result = new LoginResult
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        Token = token,
                        RoleName = (await _unitOfWork.RoleRepository.GetByIdAsync(user.RoleId))?.Name,
                        Phone = user.Phone,
                        Address = user.Address
                    }
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponseDTO
                {
                    Code = 500,
                    Message = $"Registration failed: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
