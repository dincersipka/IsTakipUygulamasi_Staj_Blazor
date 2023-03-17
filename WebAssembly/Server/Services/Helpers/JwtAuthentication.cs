using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;
using WebAssembly.Shared.ViewModels;

namespace WebAssembly.Server.Services.Helpers
{
    public class JwtAuthentication
    {
        public const string JWT_SECURITY_KEY = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";
        private const int JWT_TOKEN_VALIDITY_MINS = 60;

        private IUnitOfWork unitOfWork;

        public JwtAuthentication(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public UserSession? GenerateJwtToken(string mail, string password)
        {
            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password)) 
            {
                return null;
            }

            var userView = (from u in unitOfWork.users.GetAll()
                            join r in unitOfWork.roles.GetAll() on u.roleId equals r.id
                            where u.mail == mail && u.password == password && !u.isDeleted
                            select new UserView 
                            {
                                id = u.id,
                                name = u.name,
                                surname = u.surname,
                                mail = mail,
                                role = r.name, 
                                password = password,
                                phone = u.phone
                            }).FirstOrDefault();

            if (userView == null || password != userView.password) 
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim> 
            {
                new Claim(ClaimTypes.Email, userView.mail),
                new Claim(ClaimTypes.Name, $"{userView.name} {userView.surname}"),
                new Claim(ClaimTypes.Sid, userView.id.ToString()),
                new Claim(ClaimTypes.Role, userView.role)
            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            var userSession = new UserSession 
            {
                name = userView.name,
                surname = userView.surname,
                mail = userView.mail,
                id = userView.id,
                role = userView.role,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };

            return userSession;
        }
    }
}
