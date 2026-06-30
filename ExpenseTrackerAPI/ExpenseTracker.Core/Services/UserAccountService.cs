using System.Security.Claims;
using System.Text;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Core.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _repository;
        private readonly IConfiguration _configuration;

        public UserAccountService(IUserAccountRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public List<UserAccount> GetAccountsByUserId(int userId)
        {
            return _repository.GetAccountsByUserId(userId);
        }

        // public Task<int> ValidateUserCredentialsAsync (LoginRequestDto loginData)
        // {
        //     return _repository.ValidateUserCredentialsAsync(loginData);
        // }

        public async Task<string> AuthenticateUserAsync(LoginRequestDto loginData)
        {
            var (userId, storedHash) = await _repository.GetUserAuthDetailsByEmailAsync(loginData.Email);

            if (userId == 0 || string.IsNullOrEmpty(storedHash))
            {
                return null; // Authentication failed
            }

            // Check password validity using secure hashing
            bool isPasswordValid = PasswordEncryption.VerifyPassword(loginData.Password, storedHash);
            if (!isPasswordValid)
            {
                return null;
            }

            //  Cryptographically build real JWT string
            return GenerateJwtToken(userId, loginData.Email);
        }

        private string GenerateJwtToken(int userId, string email)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(UserRegisterDto data)
        {
            // 1. Business Validation
            bool exists = await _repository.EmailExistsAsync(data.Email);
            if (exists)
            {
                return (false, "Email is already registered.");
            }

            // 2. Hash Password using BCrypt
            string passwordHash = PasswordEncryption.HashPassword(data.Password);

            // 3. Persist Data
            int result = await _repository.RegisterUserAsync(data, passwordHash);

            if (result > 0)
                return (true, "Registration successful.");

            return (false, " Error occurred during registration.");
        }

        // Call this during the login process
        public static class PasswordEncryption
        {
            // Call this when creating/registering a new user
            public static string HashPassword(string plainTextPassword)
            {
                return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
            }

            // Call this during the login process
            public static bool VerifyPassword(string plainTextPassword, string hashedPasswordFromDb)
            {
                return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPasswordFromDb);
            }
        }
    }
}
